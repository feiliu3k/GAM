using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GAM
{
    public partial class FrmRandomGenerate : Form
    {
        List<TextBox> txtEnt = new List<TextBox>();
        public FrmRandomGenerate()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void nudEnterprise_ValueChanged(object sender, EventArgs e)
        {
            int entNum = int.Parse(nudEnterprise.Value.ToString());
            int listCount = txtEnt.Count();
            if (entNum > listCount)
            {
                //增加TextBox
                TextBox txtEntName = new TextBox();
                txtEntName.Width = 845;
                txtEntName.Height = 31;
                txtEntName.Left = 51;
                txtEntName.Top = 21;
                txtEntName.Parent = pnlEnterprise;
                txtEntName.Font.

            }
            else
            {
                //减少TextBox
            }
        }
    }
}
