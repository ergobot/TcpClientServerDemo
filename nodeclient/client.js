var net = require('net');

//var HOST = '192.168.1.124';//'127.0.0.1';
var HOST = '127.0.0.1';
var PORT = 5000;

var client = new net.Socket();
client.connect(PORT, HOST, function() {
	//client.write('Transformation is the goal.  End of line');
    console.log('CONNECTED TO: ' + HOST + ':' + PORT);
    // Write a message to the socket as soon as the client is connected, the server will receive it as message from the client 
    //client.write('I am Chuck Norris!');

});

// Add a 'data' event handler for the client socket
// data is what the server sent to this socket
client.on('data', function(data) {
    
    console.log('DATA: ' + data);
    // Close the client socket completely
    //client.destroy();
    
});

// Add a 'close' event handler for the client socket
client.on('close', function() {
    console.log('Connection closed');
});

 var readline = require('readline');
 var rl = readline.createInterface({
  input: process.stdin,
  output: process.stdout
});

rl.on('line', function (cmd) {
  //console.log('You just typed: '+cmd);
  //jar.stdin.write(cmd + '\r\n');
  client.write(cmd + '\r\n');
});