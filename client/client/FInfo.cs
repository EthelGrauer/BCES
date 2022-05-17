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
    public partial class FInfo : Form
    {
        public FInfo(int party)
        {
            InitializeComponent();
            Party_ins party_Ins = new Party_ins(party);
            lblInfo.Text = party_Ins.info;
            lblName.Text= lblName.Text +party_Ins.name;
           linkLabel.Text = party_Ins.link;
           // linkLabel. party_Ins.link; 

        }
    }
}
