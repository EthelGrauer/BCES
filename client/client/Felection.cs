using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using client;
namespace client
{
    public partial class Felection : Form
    {
        
        blockChain blkchn;
        User user;
        public Felection(User user)
        {
            InitializeComponent();
            this.user = user;
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
                Block choice = new Block(blkchn.getLastHash(), user.id, user.name, user.vote.ToString());
                MessageBox.Show("waiting for verification");
                //send block
                //if()
                //check verification
                //if()
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
