using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlockchainServer
{
    class DBhelper
    {
        public DBhelper()
        {
            sqlcon = "";
        }
        public bool does_exist(string name, int id)
        {
            return false;
        }
        public void change_signed_in(string name, int id)
        {


        }
        public bool was_signed_in(string name, int id)
        {
            return false;
        }
        private string sqlcon;
    }
}
