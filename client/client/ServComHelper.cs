using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;


namespace client
{
    public enum ServComTypes
    {
        //server com codes
        //msgs from server
        CONNECTED = 1,
        AUTH_SUC ,
        AUTH_FAIL,
        LISTEN_PORT,
        SEND_TO_PORT,
        SEND_TO_IP,
        FIRST ,
        NOT_FIRST,
        //msgs to server
        SIGN_IN,
        CLIENT_CLOSED
    
    }
public class Serv_helper
    {
        
        
        //servport
        private const int SERV_PORT = 8002;
        
        static Socket Serv_sock;
        
        private static bool auth_flag;
        private static bool first_flag;

        private static comInfo com;
        
        public Serv_helper()
        {
            com = new comInfo();
            first_flag = false;
            auth_flag = false;
            Serv_sock= new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
            Serv_sock.Connect(IPAddress.Loopback, SERV_PORT);//connection to server

            Serv_sock.Send(Encoding.ASCII.GetBytes(comHelper.GetIpAdDress()));//sending server current ip address
            
            Thread t = new Thread(getMsgFromSrvr);
            t.IsBackground = true;
            t.Start();
            
            
            
        }
        

        public bool getFirstFlag()
        {
            return first_flag;
        }
        public static void getMsgFromSrvr()
        {
            while (true)
            {

                try
                {
                    
                    int type=comHelper.recvmsgType(Serv_sock);
                    int len = comHelper.recvmsglength(Serv_sock);
                    byte[] msg = new byte[len];
                    
                    switch (type-48)
                    {
                        case (int)ServComTypes.CONNECTED:
                            MessageBox.Show("Client Connected");
                            break;
                        case (int)ServComTypes.AUTH_SUC:
                            auth_flag = true;
                            break;
                        case (int)ServComTypes.AUTH_FAIL:
                            auth_flag = false;
                            break;
                        case (int)ServComTypes.LISTEN_PORT:
                            
                            com.setlistenport(comHelper.recvIntTypeMsg(Serv_sock, len));
                            break;
                        case (int)ServComTypes.SEND_TO_PORT:
                            com.setSendToPort(comHelper.recvIntTypeMsg(Serv_sock, len));
                            break;
                        case (int)ServComTypes.SEND_TO_IP:
                            com.setSendToIp( comHelper.recvStringTypeMsg(Serv_sock, len));
                            //CliComHelper cliCom = new CliComHelper();
                            //CliComHelper cliCom = new CliComHelper();
                            break;
                        case (int)ServComTypes.FIRST:
                            first_flag = true;
                            break;
                        case (int)ServComTypes.NOT_FIRST:
                            first_flag = false;
                            
                            break;
                    }
                }
                catch { }
            }
            


        }
        public bool send_LoginToSrvr( string id, string name)
        {
            comHelper.sendMsg(Serv_sock, comHelper.constructMsg((int)ServComTypes.SIGN_IN, id + name)) ;
            Thread.Sleep(2);//instead of mutex
            return auth_flag;
        }

        
       
    }
}
