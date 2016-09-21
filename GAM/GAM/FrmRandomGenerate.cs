using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SQLite;
using Dapper;

using GAM.Models;

namespace GAM
{
    public partial class FrmRandomGenerate : Form
    {
        List<TextBox> tbEnts = new List<TextBox>();

        List<CommonMan> commonMen = null;
        List<ChargeMan> chargeMen = null;
        List<Area> areas = null;
        List<EnterpriseCategory> cates = null;
        List<DbEntityEnterprise> ents = null;


        List<CommonMan> choiceCommonMen = new List<CommonMan>();
        List<ChargeMan> choiceChargeMen = new List<ChargeMan>();
        Area choiceArea =null;
        EnterpriseCategory choiceCate = null;
        List<DbEntityEnterprise> choiceEnts = new List<DbEntityEnterprise>();

        private string databasename;
        private bool saveflag = false;

        private void initInput()
        {
            txtArea.Text = "";
            txtCate.Text = "";
            txtChargeMan.Text = "";
            txtCommonMan.Text = "";
            foreach (TextBox tbEnt in tbEnts)
            {
                tbEnt.Text = "";
            }
            btnRandomGenerate.Enabled = false;
            btnPrint.Enabled = false;
            btnSave.Enabled = false;
        }
        public FrmRandomGenerate()
        {
            InitializeComponent();
            databasename = ConfigurationManager.AppSettings["DataBasePath"] + ConfigurationManager.AppSettings["DataBase"];
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void nudEnterprise_ValueChanged(object sender, EventArgs e)
        {
            initInput();
            int entNum = int.Parse(nudEnterprise.Value.ToString());
            int listCount = tbEnts.Count();
            if (entNum > listCount)
            {
                for (int i = 0; i < (entNum - listCount); i++)
                {
                    TextBox txtEntName = new TextBox();
                    txtEntName.Width = 845;
                    txtEntName.Height = 25;
                    txtEntName.Left = 60;
                    txtEntName.Top = 3 + (tbEnts.Count()) * (25 + 10);
                    txtEntName.Parent = pnlEnterprise;
                    txtEntName.Font = new Font("宋体", txtChargeMan.Font.Size, txtChargeMan.Font.Style);
                    tbEnts.Add(txtEntName);
                }

            }
            else
            {
                for (int i = 0; i < (listCount - entNum); i++)
                {

                    TextBox txtEntName = tbEnts.Last<TextBox>();
                    tbEnts.RemoveAt(tbEnts.Count() - 1);
                    txtEntName.Dispose();
                }
            }

            btnRandomGenerate.Enabled = true;
        }

        private void btnRandomGenerate_Click(object sender, EventArgs e)
        {
            initInput();
            SQLiteConnectionStringBuilder sb = new SQLiteConnectionStringBuilder();
            sb.DataSource = databasename;
            SQLiteConnection con = new SQLiteConnection(sb.ToString());
            con.Open();
            commonMen = con.Query<CommonMan>("select * from common_man where delflag=0").ToList();
            chargeMen = con.Query<ChargeMan>("select * from charge_man where delflag=0").ToList();
            areas = con.Query<Area>("select * from areas where delflag=0").ToList();
            cates = con.Query<EnterpriseCategory>("select * from enterprise_category where delflag=0").ToList();
            
            int chargeManNum = int.Parse(nudChargeMan.Value.ToString());
            int CommonManNum = int.Parse(nudCommonMan.Value.ToString());
            int entNum = int.Parse(nudEnterprise.Value.ToString());

            if ((chargeManNum < 1) || (CommonManNum < 1) || (entNum < 0))
            {
                MessageBox.Show("输入的数据有错误，请重新输入!","错误");
            }
            else
            {
                for (int i = 0; i < Int32.Parse(nudChargeMan.Value.ToString()); i++)
                {
                    ChargeMan chargeMan = chargeMen.OrderBy(_ => Guid.NewGuid()).First();
                    txtChargeMan.Text += chargeMan.Name+"  ";
                    chargeMen.Remove(chargeMan);
                    choiceChargeMen.Add(chargeMan);
                }

                for (int i = 0; i < Int32.Parse(nudCommonMan.Value.ToString()); i++)
                {
                    CommonMan commonMan = commonMen.OrderBy(_ => Guid.NewGuid()).First();
                    txtCommonMan.Text += commonMan.Name + "  ";
                    commonMen.Remove(commonMan);
                    choiceCommonMen.Add(commonMan);
                }

                //获得类型数据
                EnterpriseCategory cate = cates.OrderBy(_ => Guid.NewGuid()).First();
                txtCate.Text = cate.Catename;
                cates.Remove(cate);
                choiceCate=cate;
                //获得区域数据
                Area area = areas.OrderBy(_ => Guid.NewGuid()).First();
                txtArea.Text = area.Areaname;
                areas.Remove(area);
                choiceArea=area;
                
                //根据类型和区域，获得企业数据
               

                            
                ents = con.Query<DbEntityEnterprise>("select * from enterprise where delflag=0 and cateid=@cateid and areaid=@areaid", new {
                    cateid=cate.Id,
                    areaid=area.Id
                }).ToList();
                con.Close();
               
                for (int i = 0; i < Int32.Parse(nudCommonMan.Value.ToString()); i++)
                {
                    if ((ents.Count == 0)|| (tbEnts.Count==0))
                    {
                        break;
                    }
                    DbEntityEnterprise ent = ents.OrderBy(_ => Guid.NewGuid()).First();
                    tbEnts[i].Text = ent.Name;
                    ents.Remove(ent);
                    choiceEnts.Add(ent);
                   
                }

            }

            btnPrint.Enabled = true;
            btnSave.Enabled = true;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (!saveflag)
            {
                btnSave_Click(sender, e);
            }

            //打印报表
            FrmReport frmReport = new FrmReport();
            frmReport.ShowDialog();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveflag)
            {
                MessageBox.Show("数据已保存!", "信息");
            }
            else
            {
                string charge_ids = "";
                foreach (ChargeMan chm in choiceChargeMen)
                {
                    charge_ids = chm.Id.ToString() + ':';
                }
                charge_ids = charge_ids.Substring(0, charge_ids.Length - 1);

                string common_ids = "";
                foreach (CommonMan cm in choiceCommonMen)
                {
                    common_ids = cm.Id.ToString() + ':';
                }
                common_ids = common_ids.Substring(0, common_ids.Length - 1);

                string chkent_ids = "";
                foreach (DbEntityEnterprise ent in choiceEnts)
                {
                    chkent_ids = ent.Id.ToString() + ':';
                }
                chkent_ids = chkent_ids.Substring(0, chkent_ids.Length - 1);

                SQLiteConnectionStringBuilder sb = new SQLiteConnectionStringBuilder();
                sb.DataSource = databasename;
                SQLiteConnection con = new SQLiteConnection(sb.ToString());
                con.Open();
                con.Execute("insert into check_action(charge_num,charge_ids,common_num,common_ids, area_id,cate_id,chkent_num,chkent_ids) values (@charge_num,@charge_ids,@common_num,@common_ids,@area_id,@cate_id,@chkent_num,@chkent_ids)", new
                {
                    charge_num = int.Parse(nudChargeMan.Value.ToString()),
                    charge_ids = charge_ids,
                    common_num = int.Parse(nudCommonMan.Value.ToString()),
                    common_ids = common_ids,
                    area_id = areas[0].Id,
                    cate_id = cates[0].Id,
                    chkent_num = int.Parse(nudEnterprise.Value.ToString()),
                    chkent_ids = chkent_ids,
                });
                saveflag = true;
                con.Close();
                btnSave.Enabled = false;
            }
        }

        private void FrmRandomGenerate_Load(object sender, EventArgs e)
        {
            initInput(); 
        }

        private void nudChargeMan_ValueChanged(object sender, EventArgs e)
        {
            initInput();
            btnRandomGenerate.Enabled = true;
        }
    }
}
