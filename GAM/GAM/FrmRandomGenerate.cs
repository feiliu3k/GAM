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
                for(int i = 0; i< (entNum - listCount); i++) { 
                    TextBox txtEntName = new TextBox();
                    txtEntName.Width = 845;
                    txtEntName.Height = 25;
                    txtEntName.Left = 51;
                    txtEntName.Top = 3+ (txtEnt.Count()) * (25+10);
                    txtEntName.Parent = pnlEnterprise;
                    txtEntName.Font= new Font("宋体", txtChargeMan.Font.Size, txtChargeMan.Font.Style);
                    txtEnt.Add(txtEntName);
                }

            }
            else
            {
                //减少TextBox
            }
        }
    }
}
