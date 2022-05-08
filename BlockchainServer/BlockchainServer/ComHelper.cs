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
    public enum ServComTypes
    {
        //server com codes
        //msgs from server
        CONNECTED = 1,
        AUTH_SUC,
        AUTH_FAIL,
        LISTEN_PORT,
        SEND_TO_PORT,
        SEND_TO_IP,
        FIRST,
        NOT_FIRST,
        //msgs to server
        SIGN_IN,
        CLIENT_CLOSED

    }
    public class ComHelper
    {
        
        public static int recvIntTypeMsg(Socket sock, int len)
        {
            byte[] msg= new byte[len];


            int num = sock.Receive(msg, 0, len, SocketFlags.None);
            //if (BitConverter.IsLittleEndian)
            //    Array.Reverse(msgLength);
            num = 0;
            for (int i = 0; i < len; i++)
            {
                msg[i] = (byte)((int)msg[i] - 48);
                num = num * 10 + (int)msg[i];
            }
            return num;
        }
        
        public static int recvmsglength(Socket sock)
        {

            return recvIntTypeMsg(sock, 4);
        }
        public static int recvmsgType(Socket sock)
        {
            byte[] msgType = new byte[1];
            int size = sock.Receive(msgType, 0, 1, SocketFlags.None);
            return (int)(msgType[0]);
        }
        public static void sendMsg(Socket sock, string msg)
        {
            sock.Send(Encoding.ASCII.GetBytes(msg));
        }
        public static string constructMsg(int type, string cntnt)
        {
            string s = type.ToString() + addZero(cntnt.Length) + cntnt;
            
            return s;
        }
        private static string addZero(int num)
        {

            int decimalLength = num.ToString("D").Length + (4 - num.ToString("D").Length);
            return num.ToString("D" + decimalLength.ToString());

        }
    }
}
