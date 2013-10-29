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
    public enum Status
    {
        Afleveret = 0,
        Reserveret = 1,
        Udlejet = 2,
        TilReparation = 3
    }

    public partial class SelectForm : Form
    {

        public SelectForm()
        {
            InitializeComponent();
        }

        private void addNewButton_Click(object sender, EventArgs e)
        {
            using (var form = new MemberForm((Member)null))
            {
                form.ShowDialog();
                memberComboBox.LoadData();
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            using (var form = new MemberForm( memberComboBox.SelectedMember ))
            {
                form.ShowDialog();
                memberComboBox.LoadData();
            }
        }

        private void memberDropDown1_OnMemberSelected(Member obj)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAddTool_Click(object sender, EventArgs e)
        {
            using (var form = new ToolForm(null))
            {
                form.ShowDialog();
                toolCombo.Refresh();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ToolForm myToolForm = new ToolForm(toolCombo.SelectedTool);
            myToolForm.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ToolForm MyDialog = new ToolForm(null);
            MyDialog.ShowDialog();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Reservation MyDialog = new Reservation(memberDropDown1.SelectedMember);
            if (MyDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                ShowReservations();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            //Tool SelectedTool = null;
            //if (SelectedReservation != null)
            //    SelectedTool = SelectedReservation.Tool;
            Udlejning MyDialog = new Udlejning(memberDropDown1.SelectedMember, SelectedReservation);
            if (MyDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ShowReservations();
                ShowUdlejninger();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Tool SelectedTool = null;
            if (SelectedUdlejning != null)
                SelectedTool = SelectedUdlejning.Tool;
            Retur MyDialog = new Retur(memberDropDown1.SelectedMember, SelectedTool);
            if (MyDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                ShowUdlejninger();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MemberForm MyDialog = new MemberForm(memberDropDown1.SelectedMember);
            MyDialog.ShowDialog();
        }

        private void memberDropDown1_OnMemberSelected_1(Member obj)
        {
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            ShowReservations();
            ShowUdlejninger();
        }

        List<Location> reservationList;

        private void ShowReservations()
        {

            using (var db = new HobbyKlubEntities1())
            {
                if (memberDropDown1.SelectedMember != null)
                {
                    var rv = db.Location.Where(x => x.MemberId == memberDropDown1.SelectedMember.MemberId && x.Status == (int)Status.Reserveret).OrderBy(x => x.StartDate);
                    //available.TraceQuery();
                    reservationList = rv.ToList();
                    listBox1.Items.Clear();
                    listBox1.Items.AddRange(reservationList.Select(x => x.StartDate.ToShortDateString() + " ... " + x.EndDate.ToString().Substring(0, 8) + "\t" + x.Tool.Name + " K" + x.Tool.K_number.ToString()).ToArray());
                }
                else
                {
                    var rv = db.Location.Where(x => x.MemberId > 0 && x.Status == (int)Status.Reserveret).OrderBy(x => x.StartDate);
                    //available.TraceQuery();
                    reservationList = rv.ToList();
                    listBox1.Items.Clear();
                    listBox1.Items.AddRange(reservationList.Select(x => x.StartDate.ToShortDateString() + " ... " + x.EndDate.ToString().Substring(0, 8) + "\t" + x.Tool.Name + " K" + x.Tool.K_number.ToString() + "\t" + x.Member.Name).ToArray());
                }
            }
        }

        public Location SelectedReservation
        {
            get
            {
                if (listBox1.SelectedIndex >= 0)
                    return reservationList[listBox1.SelectedIndex];
                else
                    return null;
            }
        }

        List<Location> udlejningsList;

        private void ShowUdlejninger()
        {
            using (var db = new HobbyKlubEntities1())
            {

                if (memberDropDown1.SelectedMember != null)
                {
                    var rv = db.Location.Where(x => x.MemberId == memberDropDown1.SelectedMember.MemberId && x.Status == (int)Status.Udlejet).OrderBy(x => x.StartDate);
                    //available.TraceQuery();
                    udlejningsList = rv.ToList();
                    listBox2.Items.Clear();
                    listBox2.Items.AddRange(udlejningsList.Select(x => x.StartDate.ToShortDateString() + " ... " + x.EndDate.ToString().Substring(0, 8) + "\t" + x.Tool.Name + " K" + x.Tool.K_number.ToString()).ToArray());
                }
                else
                {
                    var rv = db.Location.Where(x => x.MemberId > 0 && x.Status == (int)Status.Udlejet).OrderBy(x => x.StartDate);
                    //available.TraceQuery();
                    udlejningsList = rv.ToList();
                    listBox2.Items.Clear();
                    listBox2.Items.AddRange(udlejningsList.Select(x => x.StartDate.ToShortDateString() + " ... " + x.EndDate.ToString().Substring(0, 8) + "\t" + x.Tool.Name + " K" + x.Tool.K_number.ToString() + "\t" + x.Member.Name).ToArray());
                }
            }            
        }

        public Location SelectedUdlejning
        {
            get
            {
                if (listBox2.SelectedIndex >= 0)
                    return udlejningsList[listBox2.SelectedIndex];
                else
                    return null;
            }
        }

        private void SelectForm_Load(object sender, EventArgs e)
        {
            ShowReservations();
            ShowUdlejninger();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

    }
}
