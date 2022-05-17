using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainServer
{
    class DBhelper
    {
        public DBhelper()
        {
            sqlcon = "blockChainUsers.mdf";
        }
        public bool does_exist(string name, int id)
        {
            string query = "SELECT * FROM [dbo].[users] where id=" + id + " and name='" + name + "'";
            return IsExist(sqlcon, query);
        }
        public void change_signed_in(string name, int id)
        {
            string query = "UPDATE [dbo].[users] SET isVote=1 WHERE id=" + id + " and name='" + name + "'";
            DoQuery(sqlcon, query);
        }
        public bool was_signed_in(string name, int id)
        {
            string query= "SELECT isVote FROM [dbo].[users] where id="+id+" and name='" +name+"'";
            DataTable dt = ExecuteDataTable(sqlcon, query);
            if (dt.Rows[0][0].ToString()==0.ToString())
            {
                return false;
            }
            return true;
        }
        private SqlConnection ConnectToDb(string fileName)
        {
            // string path = HttpContext.Current.Server.MapPath("App_Data/");//מיקום מסד בפורוייקט
            string path = Directory.GetCurrentDirectory() + "\\";
            path = path.Substring(0, path.IndexOf("bin"));
            path += fileName;

            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='" + path + "';Integrated Security=True";
            //Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="C:\Users\משתמש\Desktop\תשפב\school project\BlockchainServer\BlockchainServer\blockChainUsers.mdf";Integrated Security=True
            SqlConnection conn = new SqlConnection(connString);
            return conn;

        }
        private void DoQuery(string fileName, string sql)
        {

            SqlConnection conn = ConnectToDb(fileName);
            conn.Open();
            SqlCommand com = new SqlCommand(sql, conn);
            com.ExecuteNonQuery();
            com.Dispose();
            conn.Close();

        }
        private DataTable ExecuteDataTable(string fileName, string sql)
        {
            SqlConnection conn = ConnectToDb(fileName);
            conn.Open();
            SqlDataAdapter tableAdapter = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            tableAdapter.Fill(dt);
            return dt;
        }

        private bool IsExist(string fileName, string sql)//הפעולה מקבלת שם קובץ ומשפט בחירת נתון ומחזירה אמת אם הנתונים קיימים ושקר אחרת
        {

            SqlConnection conn = ConnectToDb(fileName);
            try
            {
                conn.Open();
            }
            catch(Exception e)
            { }
            SqlCommand com = new SqlCommand(sql, conn);
            SqlDataReader data = com.ExecuteReader();
            bool found;
            found = (bool)data.Read();// אם יש נתונים לקריאה יושם אמת אחרת שקר - הערך קיים במסד הנתונים
            conn.Close();
            return found;

        }
        private string sqlcon;
    }
}
