using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Enkelttrådet_Server
{
    public class SimpleClient
    {
        public SimpleClient(int port)
        {
            try
            {
                using (Socket connection = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    connection.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), port));

                    NetworkStream stream = new NetworkStream(connection);
                    using (StreamWriter writer = new StreamWriter(stream))
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        writer.WriteLine("Message 1");
                        writer.WriteLine("Message 2");
                        writer.WriteLine("Message 3");

                        writer.WriteLine("CLOSE");
                        writer.Flush();
                    }
                }
                Log("Connection to server is terminated");
            }catch(Exception)
            {
                Log("Exception");
            }
        }

        private void Log(string log)
        {
            Console.WriteLine("[Client]: "+log);
        }
    }
}