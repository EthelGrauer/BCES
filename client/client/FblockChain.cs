using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client
{
    public partial class FblockChain : Form
    {
        public FblockChain(comInfo com, blockChain blkChn)
        {
            InitializeComponent();
            
            lblipcur.Text += " " + com.getReceiveFromIp() + "\n";
            lblipcur.Text += "PORT: " + com.getListenPort().ToString();
            blkChn.LastOne();
            blkChn.PrevtoLast();
            blkChn.twoBeforeEnd();
            
            
        }
    }
}
