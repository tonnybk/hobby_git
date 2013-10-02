using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HobbyKlub
{
    public partial class Tools : UserControl
    {
        public Tools()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
                 
        }

        private List<Tool> toolList;

        public Tool SelectedTool
        {
            get
            {
                return toolList[comboBox1.SelectedIndex];
            }
        }


        private void Tools_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;
           
            using (var db = new HobbyKlub_Entities())
            {
                DateTime now = DateTime.Now;
                var available = db.Tool.Where(x => !x.Location.Any(l => l.StartDate < now && l.EndDate > now));

                available.TraceQuery();

                var rv = from x in db.Tool orderby x.Name select x;

                toolList = rv.ToList();

                comboBox1.Items.AddRange(toolList.Select(x => x.Name).ToArray());
            }



        }
    }
}
