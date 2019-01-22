using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Enkelttrådet_Server
{
    public class SimpleServer
    {
        private int port;
        public SimpleServer(int port)
        {
            this.port = port;
            Thread t = new Thread(Run);
            t.Start();
        }

        private void Run()
        {
            try
            {
                TcpListener server = new TcpListener(IPAddress.Any, port);
                server.Start();

                using (Socket connection = server.AcceptSocket())
                {
                    Log("Client has connected!");

                    NetworkStream stream = new NetworkStream(connection);
                    using (StreamWriter writer = new StreamWriter(stream))
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string line = "";
                        while ((line = reader.ReadLine()) != "CLOSE")
                            if(line!=null)
                                Log(line);
                    }
                }
                Log("Server is closed!");
            }
            catch (Exception)
            {
                Log("Exception");
            }
        }

        private void Log(string log)
        {
            Console.WriteLine("[Server]: " + log);
        }
    }
}