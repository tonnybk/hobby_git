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
    public partial class SelectForm : Form
    {
        public SelectForm()
        {
            InitializeComponent();
        }

        private void addNewButton_Click(object sender, EventArgs e)
        {
            using (var form = new MemberForm(null))
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
            editButton.Enabled = true;
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
            MyDialog.ShowDialog();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Udlejning MyDialog = new Udlejning(memberDropDown1.SelectedMember);
            MyDialog.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Retur MyDialog = new Retur(memberDropDown1.SelectedMember);
            MyDialog.ShowDialog();
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
        }
    }
}
