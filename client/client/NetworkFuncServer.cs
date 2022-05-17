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

namespace client
{
    
    public class NetworkFuncServer
    {
       
         
        private TcpListener tcpLsn; // מאזין להתחברות של סוקט לשרת 
        private Thread tcpThd; // הגדרת אובייקט של תהליך
        ClientConnect c1;
        List<ClientConnect> allClients = new List<ClientConnect>();
        private static int connectId = 0;
        comInfo com;
        //NetworkFuncClient cli;
        bool finished;
        public bool getFinished()
        {
            return finished;
        }
        public void setFinished(bool f)
        {
             finished=f;
        }
        public NetworkFuncServer(comInfo info)
        {
            com = info;
            tcpLsn = new TcpListener(IPAddress.Loopback, com.getListenPort());//  יצירת מאזין- 
            tcpLsn.Start();// מפעילה את המאזין
            
            c1 = new ClientConnect();
            tcpThd = new Thread(new ThreadStart(NewClientConnected));// יצירת תהליך שפועל ברקע   
            tcpThd.Start();
            finished = false;
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
                    //int ret = 0;
                    //Byte[] receive = new Byte[15];// ברשת נתונים עוברים רק בביטים
                    //ret = s.Receive(receive, receive.Length, 0);// מחזיק את הכמות הבייטים שיתקבלו
                    //strmess = System.Text.Encoding.UTF8.GetString(receive);// שורה זו ממירה את המערך שהוא מטיפוס בייט למחרוזת שאותה הוא שומר ב strmess
                    //c1.name = strmess.Substring(0, ret);// שורה זו מחלצת את מה שכתוב עד לרווח הראשון

                    //Interlocked.Increment(ref connectId);
                    c1.clientnum = 1;
                    
                    lock (this)
                    {
                        allClients.Add(c1);// מעדכנות את הטבלה ומוסיפות לתקסטבוקס את שעת ההיתחברות
                        c1.clientThread.Start();
                        
                        



                    }

                }
                catch (Exception e)
                {
                    break;
                }
            }
        }


        public void ReadSocket()// תהליך של כול לקוח
        {

            long realId = c1.clientnum; // The realId saves the real number of the client that sends the info
            Socket s = c1.clientSocket;
            int ret = 0; // This object will contain the number of characters that are passed in the message
            //com.setReceiveFromIp(s.RemoteEndPoint);
            while (true) // מנהל המשחק: כרגע מקבלת ממל מי כול לקוח ומוסרת אותו לכול הלקוחות
            {
                try
                {
                    if (s.Connected)
                    {
                        getMsgFromCliChain(s);
                    }// If a message was received and its' characters length is 0 we get out of the loop
                }
                catch (Exception e) // If an error occured we want to stop the thread
                {
                    
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

                   
                }
            }
        }
        private Block ParseBlock(string info)
        {
            User us = new User();
            us.id = int.Parse(info.Substring(6, 9));
            us.name = info.Substring(16,info.IndexOf("@", 17)-16);
            us.vote = int.Parse(info.Substring(info.IndexOf("@", 17)+1, 1));

            Block block = new Block(info, us);
            //Block block = new Block(info.Split("@".ToCharArray()[0])[0],User(123,"a") );
            block.set_magic_num(int.Parse(info.Substring(0,5)));
            return block;
        }

        public void getMsgFromCliChain(Socket s)
        {
            int type = comHelper.recvmsgType(s);
            int len = comHelper.recvmsglength(s);
            byte[] msg = new byte[len];
            //int size;
            switch (type-48)
            {
                case (int)msgCodes.CLOSE_MSG:

                    break;
                case (int)msgCodes.INFO_BLOCK://parsing into block class
                    Block b = ParseBlock(comHelper.recvStringTypeMsg(s, len));
                   if(b.verifyMagic_num(b.get_magic_num()) && b.verify_block())
                    {
                        if (Global.FuncClient.sendBlock(b))
                        {
                            lock(this)
                            {
                                finished = true;
                            }
                        }
                        else
                        {
                            Global.blkchn.addBlock(b);
                        }
                        


                    }
                   else
                    {

                        Global.FuncClient.SendMsgToSrvr();//
                    }
                    break;
                case (int)msgCodes.VERIFIED:
                    break;
                case (int)msgCodes.NOT_VERIFIED:
                    break;
                case (int)msgCodes.CONNECTED:
                    MessageBox.Show("connected p2p");
                    //size = Listen_sock.Receive(msg, 0, len, SocketFlags.None);
                    //recievefromIp = msg.ToString();
                    break;

            }
        }
    }
}
