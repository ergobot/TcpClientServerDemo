using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TcpServerWinforms
{
    public partial class Form1 : Form
    {
        private AsyncService service;
        public Form1()
        {
            
            InitializeComponent();
            try
            {
                service = new AsyncService(5000);
                service.OnUpdateStatus += service_OnUpdateStatus;
                service.Run();
            }
            catch (Exception ex) {
                Console.WriteLine(ex);
            }
        }

        void service_OnUpdateStatus(object sender, ProgressEventArgs e)
        {
            SetStatus(e.Status);
        }

        private void UpdateStatus(object sender, ProgressEventArgs e)
        {
            SetStatus(e.Status);
        }

        private void SetStatus(string status)
        {
            txtReceive.Text = status;
            //label1.Text = status;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        

        
    }
}
