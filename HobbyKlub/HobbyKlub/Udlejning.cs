using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HobbyKlub
{
    public partial class Udlejning : Form
    {
        public Udlejning(Member ActualMember)
        {
            InitializeComponent();
            this.MyMember = ActualMember;
            textBox1.Text = MyMember.Name;
        }

        Member MyMember;
    }
}
