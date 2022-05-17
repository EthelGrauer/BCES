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
    public class NetworkFuncClient//all the functions as a block chain client 
    {
        Socket Send_to_sock;
        comInfo com;
        public List<int> magic_num;
        public NetworkFuncClient(comInfo cominfo)
        {
            com = cominfo;
            Send_to_sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            magic_num = new List<int>();
            //Send_to_sock.Connect(sendtoIp, portsendto);
            bool flag = false;
            do
            {
                try
                {
                    Send_to_sock.Connect(IPAddress.Loopback, com.getSendToPort());
                    flag = true;
                }
                catch { }
            } while (!flag);
            
                

            comHelper.sendMsg(Send_to_sock, comHelper.constructMsg((int)msgCodes.CONNECTED, ""));
        }
        public void getMsgFromSrvrChain()
        {
            int type = comHelper.recvmsgType(Send_to_sock);
            int len = comHelper.recvmsglength(Send_to_sock);
            byte[] msg = new byte[len];
            
            switch (type)
            {
                case (int)msgCodes.CLOSE_MSG:

                    break;
                case (int)msgCodes.VERIFIED:
                    
                    break;
                case (int)msgCodes.NOT_VERIFIED:
                    //throw out block
                    break;

            }
        }
        public void SendMsgToSrvr()
        {

        }
        public bool sendBlock(client.Block block)
        {
            if (magic_num.Contains(block.get_magic_num()))
            {
                magic_num.Remove(block.get_magic_num());
                return true;
            }
                
   

                magic_num.Add(block.get_magic_num());
                comHelper.sendMsg(Send_to_sock, comHelper.constructMsg((int)msgCodes.INFO_BLOCK, block.tostring()));
            return false;
        }

    }
}
