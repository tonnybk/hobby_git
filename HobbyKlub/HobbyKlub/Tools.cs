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

        private List<Tool> toolList;

        public Tool SelectedTool
        {
            get
            {
                return toolList[comboBox1.SelectedIndex];
            }
            set
            {
                if (value != null)
                    comboBox1.SelectedIndex = toolList.FindIndex(x => x.K_number == value.K_number);
            }
        }


        private void Tools_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;
           
            using (var db = new HobbyKlub.HobbyKlubEntities1())
            {
                DateTime now = DateTime.Now;
                //var available = db.Tool.Where(x => x.Location.Any(l => l.StartDate > now || l.EndDate < now));
                var available = db.Tool.Select(x => x);

                available.TraceQuery();

                var rv = from x in db.Tool orderby x.Name select x;

                toolList = rv.ToList();

                comboBox1.Items.AddRange(toolList.Select(x => x.Name + "   K" + x.K_number ).ToArray());
            }

        }
        public event Action<Tool> OnToolSelected;

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OnToolSelected != null) OnToolSelected(SelectedTool);
        }

        
    }
}
