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
    public partial class Fsignin : Form
    {
        private Serv_helper communicator;
        //private blockChain BlockChain;
        public Fsignin()
        {
            InitializeComponent();
            communicator = null;
            
                Global.blkchn = new blockChain();
            
            
            connectToServer();

        }
        
        private void connectToServer()
        {
            

            while (communicator == null)
            {
                try
                {
                    communicator = new Serv_helper();
                }
                catch (Exception)
                {
                    MessageBox.Show("couldn't connect to server");
                }
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

           
            
            if (checkID(txtId.Text) == false)
            {
                MessageBox.Show("invalid ID\nYou may try again");
                return;
            }
            else if (!this.communicator.send_LoginToSrvr(txtId.Text, txtName.Text))
            {
                MessageBox.Show("Not found\nYou may try again");
                return;
            }
            else //logged in 
            {
                User user = new User();
                user.id = int.Parse(txtId.Text);
                user.name = txtName.Text;
                Felection election = new Felection(user);
                this.Hide();
                election.ShowDialog();
                this.Show();
                

                //Felection election = new Felection(user);
                //this.Hide();
                //election.ShowDialog();
                //this.Show();
            }

        }
        

        private void btnBC_Click(object sender, EventArgs e)
        {
            if (!communicator.getFirstFlag())
            {
                
                FblockChain blockchain = new FblockChain(communicator.GetComInfo());
                this.Hide();
                blockchain.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("no network to show.\nonly one computer connected");
            }
            
        }
        private bool checkID(string id)
        {
            int[] id_12_digits = { 1, 2, 1, 2, 1, 2, 1, 2, 1 };
            int count = 0;

            if (id == null)
                return false;

            id = id.PadLeft(9, '0');

            for (int i = 0; i < 9; i++)
            {
                int num = Int32.Parse(id.Substring(i, 1)) * id_12_digits[i];

                if (num > 9)
                    num = (num / 10) + (num % 10);

                count += num;
            }

            return (count % 10 == 0);
        }
        private void OnClosing() // זוהי פונקציה שמחסלת תהליכים היא סוגרת את הthread ואת המאזין
        {
            //if (tcpLsn != null)
            //{ tcpLsn.Stop(); }

            //foreach (ClientConnect cd in allClients)
            //{
            //    if (cd.clientSocket.Connected) cd.clientSocket.Close();
            //    if (cd.clientThread.IsAlive) cd.clientThread.Abort();
            //}


            //if (tcpThd.IsAlive) tcpThd.Abort();



        }

    }
}
