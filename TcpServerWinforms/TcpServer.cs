using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpServerWinforms
{
    class TcpServer
    {
        static TcpListener listener;
        static int Port = 4321;
        static IPAddress IP_ADDRESS = new IPAddress(new byte[] { 127, 0, 0, 1 });
        static string HOSTNAME = "127.0.0.1";
        static int MAX_CLIENTS = 5;

        // Sample high score table data
        static Dictionary<string, int> highScoreTable = new Dictionary<string, int>() { 
            { "john", 1001 }, 
            { "ann", 1350 }, 
            { "bob", 1200 }, 
            { "roxy", 1199 } 
        };

        /// <summary>
        /// Server receives player name requests from the client and responds with the score.
        /// </summary>
        private static void SetupTcpServer()
        {
            listener = new TcpListener(IP_ADDRESS, Port);
            listener.Start();
            Console.WriteLine("Server running, listening to port " + Port + " at " + IP_ADDRESS);
            Console.WriteLine("Hit Ctrl-C to exit");
            var tasks = new List<Task>();
            for (int i = 0; i < MAX_CLIENTS; i++)
            {
                Task task = new Task(Service, TaskCreationOptions.LongRunning);
                task.Start();
                tasks.Add(task);
            }
            Task.WaitAll(tasks.ToArray());
            listener.Stop();
        }

        private static void Service()
        {
            while (true)
            {
                Socket socket = listener.AcceptSocket();

                Console.WriteLine("Connected: {0}", socket.RemoteEndPoint);
                try
                {
                    // Open the stream
                    Stream stream = new NetworkStream(socket);
                    StreamReader sr = new StreamReader(stream);
                    StreamWriter sw = new StreamWriter(stream);
                    sw.AutoFlush = true;

                    sw.WriteLine("{0} stats available", highScoreTable.Count);
                    while (true)
                    {
                        // Read name from client
                        string name = sr.ReadLine();

                        if (name == "" || name == null) break;



                        // Write score to client
                        if (highScoreTable.ContainsKey(name))
                            sw.WriteLine(highScoreTable[name]);
                        else
                            sw.WriteLine("Player '" + name + "' was not found.");

                    }
                    stream.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                Console.WriteLine("Disconnected: {0}", socket.RemoteEndPoint);
                socket.Close();
            }
        }

    }
}
