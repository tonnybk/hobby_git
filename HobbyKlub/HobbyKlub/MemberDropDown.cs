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
    public partial class MemberDropDown : UserControl
    {
        public MemberDropDown()
        {
            InitializeComponent();
        }

        List<Member> members;

        private void MemberDropDown_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            if (DesignMode) return;

            using (var hk = new HobbyKlub_Entities())
            {
                members = hk.Member.ToList();
            }

            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(members.Select(x => x.Name).ToArray());
        }

        public Member SelectedMember
        {
            get
            {
                if (comboBox1.SelectedIndex >= 0)
                    return members[comboBox1.SelectedIndex];
                else
                    return null;
            }
        }

        public event Action<Member> OnMemberSelected;

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OnMemberSelected != null) OnMemberSelected(SelectedMember);
        }
    }
}
