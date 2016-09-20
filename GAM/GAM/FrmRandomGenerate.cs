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
                for (int i = 0; i < (entNum - listCount); i++)
                {
                    TextBox txtEntName = new TextBox();
                    txtEntName.Width = 845;
                    txtEntName.Height = 25;
                    txtEntName.Left = 60;
                    txtEntName.Top = 3 + (txtEnt.Count()) * (25 + 10);
                    txtEntName.Parent = pnlEnterprise;
                    txtEntName.Font = new Font("宋体", txtChargeMan.Font.Size, txtChargeMan.Font.Style);
                    txtEnt.Add(txtEntName);
                }

            }
            else
            {
                for (int i = 0; i < (listCount - entNum); i++)
                {

                    TextBox txtEntName = txtEnt.Last<TextBox>();
                    txtEnt.RemoveAt(txtEnt.Count() - 1);
                    txtEntName.Dispose();
                }
            }
        }

        private void btnRandomGenerate_Click(object sender, EventArgs e)
        {
            int chargeManNum = int.Parse(nudChargeMan.Value.ToString());
            int CommonManNum = int.Parse(nudCommonMan.Value.ToString());
            int entNum = int.Parse(nudEnterprise.Value.ToString());

            if ((chargeManNum < 1) || (CommonManNum < 1) || (entNum < 1))
            {
                MessageBox.Show("输入的数据有错误，请重新输入!","错误");
            }
            else
            {
                //获得组长数据，一般工作人员数据

                //获得类型数据
                //获得区域数据
                //根据类型和区域，获得企业数据

            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
