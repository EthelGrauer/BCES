using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;


namespace BlockchainServer
{
    
    public partial class Form1 : Form
    {
        static public int portDis = 10000;
        private string IpAddress; // משתנה המחזיק את הכתובת של המחשב
        private TcpListener tcpLsn; // מאזין להתחברות של סוקט לשרת 
        private Thread tcpThd; // הגדרת אובייקט של תהליך
        ClientConnect c1;
        List<ClientConnect> allClients = new List<ClientConnect>();
        private static int connectId = 0; // משתנה ששומר את מספר הלקוחות שהתחברו ואת מספר הלקוח 
        //string NameOfUsers;


        private void GetIpAdDress() // את כתובת המחשב IpAddress פונקציה שתשמור במשתנה IpAdress את הכתובת
        {
            IPAddress[] localIP = Dns.GetHostAddresses(Dns.GetHostName());
            IpAddress = Convert.ToString(localIP[localIP.Length - 1]);
            // IpAddress ="127.0.0.1";        
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetIpAdDress();
            tcpLsn = new TcpListener(IPAddress.Loopback, 8002);//  יצירת מאזין- 
            tcpLsn.Start();// מפעילה את המאזין
            lblInfo.Text = "Listen at: " + tcpLsn.LocalEndpoint.ToString();// מציג את הכתובת והפורט
            c1 = new ClientConnect();
            tcpThd = new Thread(new ThreadStart(NewClientConnected));// יצירת תהליך שפועל ברקע   
            tcpThd.Start();

        }

        public void NewClientConnected()
        {
            Socket s;
            string strmess;

            while (true)
            {
                try
                {
                    s = tcpLsn.AcceptSocket();
                    c1 = new ClientConnect();
                    c1.clientSocket = s;
                    c1.clientThread = new Thread(new ThreadStart(ReadSocket)); //שומרת את הטרד 
                    int ret = 0;
                    Byte[] receive = new Byte[15];// ברשת נתונים עוברים רק בביטים
                    ret = s.Receive(receive, receive.Length, 0);// מחזיק את הכמות הבייטים שיתקבלו
                    strmess = System.Text.Encoding.UTF8.GetString(receive);// שורה זו ממירה את המערך שהוא מטיפוס בייט למחרוזת שאותה הוא שומר ב strmess
                    c1.name = strmess.Substring(0, ret);// שורה זו מחלצת את מה שכתוב עד לרווח הראשון

                    Interlocked.Increment(ref connectId);
                    c1.clientnum = connectId;
                    UpDateDataGrid(connectId + " : " + strmess.Substring(0, ret) + "\n");
                    lock (this)
                    {
                        allClients.Add(c1);// מעדכנות את הטבלה ומוסיפות לתקסטבוקס את שעת ההיתחברות
                        UpDateDataGrid("Connected > " + connectId + " " + DateTime.Now.ToLongTimeString());//
                        ComHelper.sendMsg(c1.clientSocket, ComHelper.constructMsg((int)ServComTypes.CONNECTED, ""));
                        c1.clientThread.Start();
                        byte[] port = new byte[5];
                        if (allClients.Count == 1)
                        {
                            ComHelper.sendMsg(c1.clientSocket, ComHelper.constructMsg((int)ServComTypes.LISTEN_PORT, (portDis+c1.clientnum).ToString()));
                            ComHelper.sendMsg(c1.clientSocket, ComHelper.constructMsg((int)ServComTypes.FIRST,""));
                            //c1.clientSocket.Send(Encoding.ASCII.GetBytes("1"));
                            //c1.clientSocket.Send(Encoding.ASCII.GetBytes((allClients[0].name)));
                            
                        }
                        else
                        {
                            ComHelper.sendMsg(c1.clientSocket, ComHelper.constructMsg((int)ServComTypes.LISTEN_PORT, (c1.clientnum+portDis).ToString()));
                            ComHelper.sendMsg(c1.clientSocket, ComHelper.constructMsg((int)ServComTypes.NOT_FIRST, ""));
                            ComHelper.sendMsg(c1.clientSocket, ComHelper.constructMsg((int)ServComTypes.SEND_TO_PORT,(allClients[0].clientnum+portDis).ToString()));
                            ComHelper.sendMsg(c1.clientSocket, ComHelper.constructMsg((int)ServComTypes.SEND_TO_IP, allClients[0].name));
                            
                            if (allClients.Count==2)
                            {
                                
                                ComHelper.sendMsg(allClients[0].clientSocket, ComHelper.constructMsg((int)ServComTypes.NOT_FIRST, null));
                            }
                            ComHelper.sendMsg(allClients[0].clientSocket, ComHelper.constructMsg((int)ServComTypes.SEND_TO_PORT, null));
                            ComHelper.sendMsg(allClients[0].clientSocket, ComHelper.constructMsg((int)ServComTypes.SEND_TO_IP, null));
                            //c1.clientSocket.Send(Encoding.ASCII.GetBytes("2" + (allClients[allClients.Count - 2].name)));
                            //c1.clientSocket.Send(Encoding.ASCII.GetBytes((allClients[0].name)));
                            //allClients[0].clientSocket.Send(Encoding.ASCII.GetBytes((c1.name)));
                            //allClients[allClients.Count - 2].clientSocket.Send(Encoding.ASCII.GetBytes((c1.name)));
                        }
                       


                    }
                    
                }
                catch (Exception e)
                {
                    break;
                }
            }
        }
        public void UpDateDataGrid(string displayString)//אם תוך כדי תהליכים נצטרך לעדכן תיבת טקסט או כפתורים על הטופס נעשה אינבואוק
        {
            if (txtData.InvokeRequired)
                txtData.Invoke(new MethodInvoker(() => txtData.AppendText(displayString + "\n")));

        }
        public void ReadSocket()// תהליך של כול לקוח
        {

            long realId = c1.clientnum; // The realId saves the real number of the client that sends the info
            Socket s = c1.clientSocket;
            int ret = 0; // This object will contain the number of characters that are passed in the message
            Byte[] receive; // In this array I'll save the info from the client
            receive = new Byte[2000]; // If the client is connected we reboot the array;
            while (true) // מנהל המשחק: כרגע מקבלת ממל מי כול לקוח ומוסרת אותו לכול הלקוחות
            {
                try
                {
                    if (s.Connected)
                    {
                        ret = s.Receive(receive, receive.Length, 0);//s.Receive is a command that gets the info from the client and put it in the array receive
                        if (ret > 0) // If a message is rececived
                        {
                            foreach (ClientConnect c in allClients)
                            {
                                if (c.clientSocket.Connected)
                                    c.clientSocket.Send(receive, receive.Length, SocketFlags.None);
                            }
                        }
                        else
                        {
                            break;
                        }
                    }// If a message was received and its' characters length is 0 we get out of the loop
                }
                catch (Exception e) // If an error occured we want to stop the thread
                {
                    UpDateDataGrid(e.ToString()); // Show on the screen in the txtbox the error that occured
                    if (!s.Connected) break;// If the client is not connected we get out of the loop
                }
            }


            CloseTheThread(realId);// We'll get to this line only if an error occured, because we only get out of the loop if there was an error
                                   //that's why this function will only be summaned if an error occured
        }
        private void CloseTheThread(long realId)
        {
            //This function closes the thread - the process of the client that caused the error
            try
            {
                for (int i = 0; i < allClients.Count; i++)
                {
                    if (allClients[i].clientnum == realId)
                        allClients[i].clientThread.Abort();
                }

            }

            catch (Exception e)
            {
                lock (this)
                {
                    for (int i = 0; i < allClients.Count; i++)
                    {
                        if (allClients[i].clientnum == realId)
                            allClients.Remove(allClients[i]);
                    }

                    UpDateDataGrid("Disconnected>" + realId + " " + DateTime.Now.ToLongTimeString());
                }
            }
        }
        private void OnClosing() // זוהי פונקציה שמחסלת תהליכים היא סוגרת את הthread ואת המאזין
        {
            if (tcpLsn != null)
            { tcpLsn.Stop(); }

            foreach (ClientConnect cd in allClients)
            {
                if (cd.clientSocket.Connected) cd.clientSocket.Close();
                if (cd.clientThread.IsAlive) cd.clientThread.Abort();
            }


            if (tcpThd.IsAlive) tcpThd.Abort();

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            OnClosing();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            OnClosing();
        }

        private void transfer_data()
        {
            
        }
    }
}


        

 

