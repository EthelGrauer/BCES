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
    public partial class Felection : Form
    {
        
        //blockChain blkchn;
        //NetworkFuncClient cli;
        //NetworkFuncServer server;
        User user;
        public Felection(User user)
        {
            InitializeComponent();
            this.user = user;
            //blkchn = b;
            //cli = client;
            //server = serv;
        }
        
        private void choice(object sender, EventArgs e)
        {
            //MessageBox.Show("Something went wrong.\ntry voting again");
            Button btn = (Button)sender;
            user.vote = int.Parse(btn.Tag.ToString());//check
            Party_ins party = new Party_ins(user.vote);
            string message = "You've chosen " + party.name + " party.\nAre you sure?";
            string title = "confirmation";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                Block choice;
                if (Global.blkchn.isEmpty())
                {
                    choice = new Block("", user);
                }
                else 
                {
                   choice = new Block(Global.blkchn.getLastHash(), user);
                }
                if (Global.FuncClient ==null && Global.FuncServer ==null)
                    Global.blkchn.addBlock(choice);
                else
                {
                    MessageBox.Show("waiting for verification");
                    Global.FuncClient.sendBlock(choice);
                    do
                    {
                        Thread.Sleep(2000);
                    } while ((Global.FuncServer.getFinished() == false));
                    Global.FuncServer.setFinished(false);
                    Global.blkchn.addBlock(choice);
                }
                
                //server.getMsgFromCliChain();
                
                this.Close();

            }
           //MessageBox.Show("Thank you for your vote.\nYou will be disconnected in a few seconds.");

        }

        private void info(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            
            
            FInfo info= new FInfo(int.Parse(btn.Tag.ToString()));
            this.Hide();
            info.ShowDialog();
            this.Show();

        }
    }
}
