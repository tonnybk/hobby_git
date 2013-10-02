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
    public partial class ToolForm : Form
    {
        public ToolForm(Tool tool)
        {
            this.tool = tool;
            InitializeComponent();
        }

        Tool tool;

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ToolForm_Load(object sender, EventArgs e)
        {
            tbxName.Text = tool.Name;
            checkBox1.Checked = tool.Active;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
