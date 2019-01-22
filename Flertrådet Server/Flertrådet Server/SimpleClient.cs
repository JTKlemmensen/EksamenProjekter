using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flertrådet_Server
{
    public class SimpleClient
    {
        private int port;
        public SimpleClient(int port)
        {
            this.port = port;
            new Thread(Run).Start();
        }

        private void Run()
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
                        writer.WriteLine("LOWER HeJSDFPOSDFKPOFSDPOKFSDOPKSDFOPK");
                        writer.WriteLine("Message 3");

                        writer.WriteLine("CLOSEALL");
                        writer.Flush();

                        string line = reader.ReadLine();
                        Log("Received message: '" + line + "'");
                    }
                }
                Log("Connection to server is terminated");
            }
            catch (Exception)
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