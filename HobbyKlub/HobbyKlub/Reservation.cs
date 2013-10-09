using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Objects;
using System.Diagnostics;

namespace HobbyKlub
{
    public partial class Reservation : Form
    {
        public Reservation(Member ActualMember)
        {
            InitializeComponent();
            this.MyMember = ActualMember;
            textBox1.Text = MyMember.Name;
        }

        Member MyMember;


        private void button1_Click(object sender, EventArgs e)
        {
            using (var hobbyklub = new HobbyKlubEntities1())
            {
                hobbyklub.Location.AddObject(new Location() { MemberId = MyMember.MemberId, StartDate = dateTimePicker1.Value, EndDate = dateTimePicker1.Value.AddDays(7), Status = (int)Status.Reserveret, ToolId = tools1.SelectedTool.ToolId });
                hobbyklub.SaveChanges();
            }
        }
    }
}
