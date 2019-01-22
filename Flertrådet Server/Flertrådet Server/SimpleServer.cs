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
    public class SimpleServer
    {
        private int port;
        private int idCounter;
        private bool stop;
        private List<Worker> workers;

        public SimpleServer(int port)
        {
            this.port = port;
            this.stop = false;
            workers = new List<Worker>();

            Thread t = new Thread(Run);
            t.Start();
        }

        private void Run()
        {
            try
            {
                TcpListener server = new TcpListener(IPAddress.Any, port);
                server.Start();

                while (!stop)
                    if (server.Pending())
                    {
                        Socket connection = server.AcceptSocket();

                        Worker w = new Worker(this, connection, idCounter++);
                        workers.Add(w);
                    }

                Log("Server is closed!");
            }
            catch (Exception)
            {
                Log("Exception");
            }
        }

        public void Terminate()
        {
            stop = true;

            foreach (Worker w in workers)
                w.Terminate();
        }

        public void Remove(Worker worker)
        {
            workers.Remove(worker);
        }

        private void Log(string log)
        {
            Console.WriteLine("[Server]: " + log);
        }
    }
}