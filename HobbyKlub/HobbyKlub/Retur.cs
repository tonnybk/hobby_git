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
    public partial class Retur : Form
    {
        public Retur(Member ActualMember, Tool ActualTool)
        {
            InitializeComponent();
            this.MyMember = ActualMember;
            textBox1.Text = MyMember.Name;
            this.MyTool = ActualTool;
        }

        private void Retur_Load(object sender, EventArgs e)
        {
            tools1.SelectedTool = MyTool;
        }

        Member MyMember;
        Tool MyTool;

        private void button1_Click(object sender, EventArgs e)
        {
            using (var db = new HobbyKlubEntities1())
            {
                var rv = db.Location.Where(x => x.MemberId == MyMember.MemberId && x.ToolId == MyTool.ToolId && x.Status == (int)Status.Udlejet);
                if (rv.Count() > 0)
                {
                    //foreach (var loc in rv)
                    //{
                    var loc = rv.First();  // convert only first reservation to udlejninger, normally there should only be one
                    db.Location.Attach(loc);
                    loc.Status = (int)Status.Afleveret;
                    loc.EndDate = dateTimePicker1.Value;
                    //    break;   
                    //}
                    db.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Member " + MyMember.Name + " does not have tool " + MyTool.Name + " K" + MyTool.K_number.ToString());
                }
            }

        }

        private void tools1_OnToolSelected(Tool obj)
        {
            MyTool = tools1.SelectedTool;
        }


    }
}
