using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flertrådet_Server
{
    public class Worker
    {
        private SimpleServer server;
        private Socket connection;
        private int id;
        private bool stop;
        private StreamWriter writer;

        public Worker(SimpleServer server, Socket connection, int id)
        {
            this.server = server;
            this.connection = connection;
            this.id = id;
            this.stop = false;

            Thread t = new Thread(Run);
            t.Start();
        }

        private void Run()
        {
            NetworkStream stream = new NetworkStream(connection);
            using (writer = new StreamWriter(stream))
            using (StreamReader reader = new StreamReader(stream))
            {
                string line = "";
                while (!stop)
                {
                    line = reader.ReadLine();
                    if (line != null)
                        Command(line);
                }
            }
            connection.Close();
        }

        private void Command(string input)
        {
            string[] tokens = input.Split();
            string command = tokens[0].ToUpper();

            switch (command)
            {
                case "CLOSE":
                    Terminate();
                    break;

                case "UPPER":
                    if (tokens[1] != null)
                        SendMessage(tokens[1].ToUpper());
                    break;

                case "LOWER":
                    if (tokens[1] != null)
                        SendMessage(tokens[1].ToLower());
                    break;

                case "CLOSEALL":
                    server.Terminate();
                    break;

                default:
                    Log(command);
                    break;
            }
        }

        private void SendMessage(string message)
        {
            Log("Sending message: " + message);
            writer.WriteLine(message);
            writer.Flush();
        }

        private void Log(string log)
        {
            Console.WriteLine("[Worker "+id+"]: " + log);
        }

        public void Terminate()
        {
            stop = true;
            Log("Closing");
        }
    }
}