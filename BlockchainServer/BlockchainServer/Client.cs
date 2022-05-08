using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;


namespace BlockchainServer
{
    class ClientConnect
    {
        public int clientnum;
        public string name;  //שם הלקוח שיתחבר לשרת 
        public Socket clientSocket; // הסוקט שדרכו הלקוח התחבר לשרת 
        public Thread clientThread;//
    }
}
