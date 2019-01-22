using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enkelttrådet_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            new SimpleServer(25565);
            new SimpleClient(25565);
        }
    }
}