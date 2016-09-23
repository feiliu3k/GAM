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
        private List<TextBox> tbEnts = new List<TextBox>();

        private List<CommonMan> commonMen = null;
        private List<ChargeMan> chargeMen = null;
        private List<Area> areas = null;
        private List<EnterpriseCategory> cates = null;
        private List<DbEntityEnterprise> ents = null;


        private List<CommonMan> choiceCommonMen = new List<CommonMan>();
        private List<ChargeMan> choiceChargeMen = new List<ChargeMan>();
        private Area choiceArea =null;
        private EnterpriseCategory choiceCate = null;
        private List<DbEntityEnterprise> choiceEnts = new List<DbEntityEnterprise>();

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
            databasename = Directory.GetCurrentDirectory() + ConfigurationManager.AppSettings["DataBasePath"] + ConfigurationManager.AppSettings["DataBase"];
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
            choiceCommonMen = new List<CommonMan>();
            choiceChargeMen = new List<ChargeMan>();
            choiceArea = null;
            choiceCate = null;
            choiceEnts = new List<DbEntityEnterprise>();

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
               
                for (int i = 0; i < Int32.Parse(nudEnterprise.Value.ToString()); i++)
                {
                    if ((ents.Count == 0)|| (tbEnts.Count==0))
                    {
                        break;
                    }
                    DbEntityEnterprise ent = ents.OrderBy(_ => Guid.NewGuid()).First();
                    tbEnts[i].Text =(i+1)+"、"+ ent.Name+"("+ent.Addr+")";
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


            DateTime dt = DateTime.Now;
            string createAt = dt.ToLongDateString();
            //打印报表
            FrmReport frmReport = new FrmReport(choiceCommonMen, choiceChargeMen, choiceArea, choiceCate, choiceEnts,createAt);
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
                SQLiteConnectionStringBuilder sb = new SQLiteConnectionStringBuilder();
                sb.DataSource = databasename;
                SQLiteConnection con = new SQLiteConnection(sb.ToString());
                con.Open();                
                con.Execute("insert into check_action(area_id,cate_id,charge_num,common_num,chkent_num) values (@area_id,@cate_id,@charge_num,@common_num,@chkent_num)", new
                { 
                    area_id = areas[0].Id,
                    cate_id = cates[0].Id,
                    common_num = int.Parse(nudCommonMan.Value.ToString()),
                    charge_num = int.Parse(nudChargeMan.Value.ToString()),
                    chkent_num = int.Parse(nudEnterprise.Value.ToString())                   
                });
                int actionid = con.Query<int>("Select max(id) from check_action").Single();
                foreach (ChargeMan chm in choiceChargeMen)
                {
                    con.Execute("insert into choice_charge_man(chargeman_id,checkaction_id) values (@chargeman_id,@checkaction_id)", new
                    {
                        chargeman_id = chm.Id,
                        checkaction_id = actionid,

                    });
                }

                foreach (CommonMan cm in choiceCommonMen)
                {
                    con.Execute("insert into choice_common_man(commonman_id,checkaction_id) values (@commonman_id,@checkaction_id)", new
                    {
                        commonman_id = cm.Id,
                        checkaction_id = actionid,

                    });
                }

                foreach (DbEntityEnterprise ee in choiceEnts)
                {
                    con.Execute("insert into choice_enterprise(enterprise_id,checkaction_id) values (@enterprise_id,@checkaction_id)", new
                    {
                        enterprise_id = ee.Id,
                        checkaction_id = actionid,

                    });
                }

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
