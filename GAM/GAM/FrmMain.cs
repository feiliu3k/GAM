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
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void TypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmEntType frmEntType = new FrmEntType();
            frmEntType.ShowDialog();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmEntCate frmEntCate = new FrmEntCate();
            frmEntCate.ShowDialog();
        }

        private void AreaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmArea frmArea = new FrmArea();
            frmArea.ShowDialog();
        }

        private void CommonManToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCommonMan frmCommonMan = new FrmCommonMan();
            frmCommonMan.ShowDialog();
        }

        private void ChargeManToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmChargeMan frmChargeMan = new FrmChargeMan();
            frmChargeMan.ShowDialog();
        }

        private void EntAddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmEntAdd frmEntAdd = new FrmEntAdd();
            frmEntAdd.ShowDialog();

        }

        private void EntQueryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmEntQuery frmEntQuery = new FrmEntQuery();
            frmEntQuery.ShowDialog();
        }

        private void randomGenerateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRandomGenerate frmRandomGenerate = new FrmRandomGenerate();
            frmRandomGenerate.ShowDialog();
        }

        private void EntLoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmEntLoad frmEntLoad = new FrmEntLoad();
            frmEntLoad.ShowDialog();
        }

        private void historyDdataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmHistoryData frmHistoryData = new FrmHistoryData();
            frmHistoryData.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmChangePassword frmChangePassword = new FrmChangePassword();
            frmChangePassword.ShowDialog();
        }
    }
}
