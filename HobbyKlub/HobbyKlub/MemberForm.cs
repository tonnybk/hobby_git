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
    public partial class MemberForm : Form
    {
        public MemberForm(Member member)
        {
            this.member = member;
            InitializeComponent();
        }

        Member member;

        private void okButton_Click(object sender, EventArgs e)
        {
            using (var db = new HobbyKlubEntities1())
            {
                if (member != null)
                {
                    db.Member.Attach(member);
                }
                else
                {
                    member = new Member();
                    db.Member.AddObject(member);
                }

                member.Address = address.Text;
                member.Email1 = mail.Text;
                member.PhoneMobile = mobile.Text;
                member.Name = name.Text;
                

                db.SaveChanges();

                int xxx = 0;
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void MemberForm_Load(object sender, EventArgs e)
        {
            if (member != null)
            {
                this.name.Text = member.Name;
                this.mobile.Text = member.PhoneMobile;
                this.mail.Text = member.Email1;
                this.address.Text = member.Address;
            }
        }
    }
}
