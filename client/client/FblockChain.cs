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
        public FblockChain(comInfo com)
        {
            InitializeComponent();
            
            lblipcur.Text += " " + com.getThisIp() + "\n";
            lbl2.Text += " " + com.getReceiveFromIp() + "\n";
            lbl2.Text += "PORT: " + com.getListenPort().ToString();
            
            lblipsend.Text+= " " + com.getSendToIp() + "\n";
            lblipsend.Text += "PORT: " + com.getSendToPort().ToString();
            Party_ins p;
            if (!Global.blkchn.isEmpty())
            {
                lbln1.Text += Global.blkchn.LastOne().getUs().name;
                p = new Party_ins(Global.blkchn.LastOne().getUs().vote);
                lblv1.Text += p.name;
                lblm1.Text += Global.blkchn.LastOne().get_magic_num();
                if (Global.blkchn.PrevtoLast() != null)

                {
                    lbln2.Text += Global.blkchn.PrevtoLast().getUs().name;
                    p = new Party_ins(Global.blkchn.PrevtoLast().getUs().vote);
                    lblv2.Text += p.name;
                    lblm2.Text += Global.blkchn.PrevtoLast().get_magic_num();
                    if (Global.blkchn.twoBeforeEnd() != null)
                    {
                        lbln3.Text += Global.blkchn.twoBeforeEnd().getUs().name;
                        p = new Party_ins(Global.blkchn.twoBeforeEnd().getUs().vote);
                        lblv3.Text += p.name;
                        lblm3.Text += Global.blkchn.twoBeforeEnd().get_magic_num();
                    }

                }
                
            }
            
            
            
            
        }
    }
}
