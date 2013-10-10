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
            bool conflict = false;
            for (int i = 0; i < monthCalendar1.BoldedDates.Count(); i++)
            {
                if (monthCalendar1.BoldedDates[i] >= monthCalendar1.SelectionStart && monthCalendar1.BoldedDates[i] <= monthCalendar1.SelectionEnd)
                {
                    conflict = true;
                    break;
                }
            }
            if (conflict)
            {
                MessageBox.Show("The tool K" + tools1.SelectedTool.K_number.ToString() + " is not available in this period.", "Error", MessageBoxButtons.OK);
                this.DialogResult = System.Windows.Forms.DialogResult.None;
            }
            else
            {
                using (var hobbyklub = new HobbyKlubEntities1())
                {
                    hobbyklub.Location.AddObject(new Location() { MemberId = MyMember.MemberId, StartDate = monthCalendar1.SelectionStart, EndDate = monthCalendar1.SelectionEnd, Status = (int)Status.Reserveret, ToolId = tools1.SelectedTool.ToolId });
                    hobbyklub.SaveChanges();
                }
            }
        }

        private void Reservation_Load(object sender, EventArgs e)
        {
            monthCalendar1.MinDate = DateTime.Now;
            monthCalendar1.ShowTodayCircle = true;
        }

        List<Location> locationListOfSelectedTool;

        private void tools1_OnToolSelected(Tool obj)
        {
            using (var db = new HobbyKlubEntities1())
            {
                var rv = db.Location.Where(x => x.ToolId == tools1.SelectedTool.ToolId && x.Status > (int)Status.Afleveret && x.EndDate >= DateTime.Now);
                foreach (var loc in rv) // mark all days where the tool is not available
                {
                    DateTime start = (DateTime)loc.StartDate;
                    DateTime end = (DateTime)loc.EndDate;
                    for (DateTime l = start; l <= end; l = l.AddDays(1))
                    {
                        monthCalendar1.AddBoldedDate(l);
                    }
                }
                monthCalendar1.UpdateBoldedDates();

                locationListOfSelectedTool = rv.ToList();
                listBox1.Items.Clear();
                listBox1.Items.AddRange(locationListOfSelectedTool.Select(x => x.StartDate.ToShortDateString() + " ... " + x.EndDate.ToString().Substring(0, 8) + "\t" + x.Member.Name + "\t" + ((Status)x.Status).ToString()).ToArray());
            }

        }
    }
}
