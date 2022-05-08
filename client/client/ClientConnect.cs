using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;
using System.Data;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

using System.Threading;

namespace client
{
    class ClientConnect
    {
        public int clientnum;
        public string name;  //שם הלקוח שיתחבר לשרת 
        public Socket clientSocket; // הסוקט שדרכו הלקוח התחבר לשרת 
        public Thread clientThread;//
    }
}
