using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using client;
namespace client
{
    public enum msgCodes
    {
        //msgs between clients
        CLOSE_MSG = 11,
        INFO_BLOCK,
        CONNECTED,
        VERIFIED,
        NOT_VERIFIED
    }
    class comInfo
    {
        private string sendtoIp;
        private string recievefromIp;
        private int portsendto;
        private int portlisten;
        private string IpAddress;
        public comInfo()
        {
            
           
            sendtoIp = this.IpAddress;
            recievefromIp = this.IpAddress;
            portsendto = 0;
            portlisten = 0;
            
        }
        public comInfo(comInfo info)
        {
            this.IpAddress = info.IpAddress;
            this.portlisten = info.getListenPort();
            this.recievefromIp = info.getReceiveFromIp();
            this.sendtoIp = info.getSendToIp();
            this.portsendto = info.getSendToPort();
        }
        public string getThisIp()
        {
            return this.IpAddress;
        }
        public string getSendToIp()
        {
            return sendtoIp;
        }
        public void setSendToIp(string newIp)
        {
            sendtoIp = newIp;
        }
        public void setSendToPort(int port)
        {
            portsendto = port;
        }
        public void setlistenport(int port)
        {
            portlisten = port;
        }
        public void setReceiveFromIp(string newIp)
        {
            recievefromIp = newIp;
        }
        public string getReceiveFromIp()
        {
            return recievefromIp;
        }
        public int getListenPort()
        {
            return portlisten;
        }
        public int getSendToPort()
        {
            return portsendto;
        }
    }
}
