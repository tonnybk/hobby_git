using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HobbyklubModel1;

namespace HobbyklubConverter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var oldHobbyklub = new Hobbyklub2Entities1())
            {
                using (var hobbyKlub = new HobbyKlubEntities1())
                {
                    //foreach (var m in hobbyKlub.Member) hobbyKlub.Member.DeleteObject(m);
                    foreach (var t in hobbyKlub.Tool) hobbyKlub.Tool.DeleteObject(t);
                    foreach (var p in hobbyKlub.Part) hobbyKlub.Part.DeleteObject(p);
                    //foreach (var p in hobbyKlub.Location) hobbyKlub.Location.DeleteObject(p);
                    hobbyKlub.SaveChanges();

                    //foreach (var m in oldHobbyklub.)    MEDLEM existerer ikke i Entity, selvom den existerer i databasen? Desuden ligger der ikke nogen Medlemsdata i denne database, kun tools.
                    //{
                    //    hobbyKlub.Member.AddObject(new Member() { Name = m.NAVN, Address = m.ADRESSE, Zip = m.POSTNUMMER, City = m.BY, LoenNumber = m.LON_NUMMER });
                    //}

                    int toolid = 1;
                    foreach (var t in oldHobbyklub.VAERKTOJ)
                    {
                        int n;
                        if (t.K_NUMMER[0] != 'K')  //throw new Exception("error in K_NUMMER");
                            n = int.Parse(t.K_NUMMER);  // try without K
                        else
                            n = int.Parse(t.K_NUMMER.Substring(1));

                        int partno = 1;
                        Action<string> a = s =>
                            {
                                if (!string.IsNullOrEmpty(s))
                                {
                                    hobbyKlub.Part.AddObject(new Part() { Description = s, PartNo = partno++, ToolId = toolid });
                                }
                            };

                        if (t.DEL1 != null)
                            if (t.DEL1.Length > 0)
                                a(t.DEL1);
                        if (t.DEL2 != null)
                            if (t.DEL2.Length > 0)
                                a(t.DEL2);
                        if (t.DEL3 != null)
                            if (t.DEL3.Length > 0)
                                a(t.DEL3);
                        if (t.DEL4 != null)
                            if (t.DEL4.Length > 0)
                                a(t.DEL4);
                        if (t.DEL5 != null)
                            if (t.DEL5.Length > 0)
                                a(t.DEL5);
                        if (t.DEL6 != null)
                            if (t.DEL6.Length > 0)
                                a(t.DEL6);
                        if (t.DEL7 != null)
                            if (t.DEL7.Length > 0)
                                a(t.DEL7);
                        string toolName = t.BESKRIV;
                        int pos = t.BESKRIV.IndexOf(' ', 0);
                        if (pos >= 0)
                            toolName = t.BESKRIV.Substring(0, pos);

                        hobbyKlub.Tool.AddObject(new Tool() { ToolId = toolid++, Name = toolName, Description = t.BESKRIV, K_number = n });

                        textBox1.Text = "ToolIDs = " + toolid.ToString();
                    }

                    hobbyKlub.SaveChanges();
                }
            }
        }
    }
}
