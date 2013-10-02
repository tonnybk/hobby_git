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
    }
}
