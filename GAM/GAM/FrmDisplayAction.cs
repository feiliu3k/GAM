﻿using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    public partial class FrmDisplayAction : Form
    {
        private string databasename;       
        private int actionID;

        private List<TextBox> tbEnts = new List<TextBox>();

        private List<CommonMan> commonMen = null;
        private List<ChargeMan> chargeMen = null;
        private Area area = null;
        private EnterpriseCategory cate = null;
        private List<DbEntityEnterprise> ents = null;
        string createAt = "";

        public FrmDisplayAction(int actionID)
        {
            InitializeComponent();
            this.actionID = actionID;
            databasename = Directory.GetCurrentDirectory() + ConfigurationManager.AppSettings["DataBasePath"] + ConfigurationManager.AppSettings["DataBase"];
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmDisplayAction_Load(object sender, EventArgs e)
        {
            SQLiteConnectionStringBuilder sb = new SQLiteConnectionStringBuilder();
            sb.DataSource = databasename;
            SQLiteConnection con = new SQLiteConnection(sb.ToString());
            con.Open();
            chargeMen = con.Query<ChargeMan>("select chm.id,chm.name, chm.birth, chm.sex, chm.addr,chm.phone,chm.remark,chm.delflag from charge_man chm join choice_charge_man cchm on chm.id=cchm.chargeman_id and cchm.checkaction_id=@actionID", new { actionID = this.actionID }).ToList();
            foreach (ChargeMan chm in chargeMen)
            {
                txtChargeMan.Text += chm.Name + "  ";
            }
            commonMen = con.Query<CommonMan>("select cm.id,cm.name, cm.birth, cm.sex, cm.addr,cm.phone,cm.remark,cm.delflag from common_man cm join choice_common_man ccm on cm.id=ccm.commonman_id and ccm.checkaction_id=@actionID",new{ actionID=this.actionID }).ToList();
            foreach (CommonMan cm in commonMen)
            {
                txtCommonMan.Text += cm.Name + "  ";
            }
            area = con.Query<Area>("select ar.* from areas ar join check_action ca on ca.area_id=ar.id where ca.id=@actionID", new { actionID = this.actionID }).Single();
            txtArea.Text = area.Areaname;
            cate = con.Query<EnterpriseCategory>("select ec.* from enterprise_category ec join check_action ca on ca.cate_id=ec.id where ca.id=@actionID", new { actionID = this.actionID }).Single();
            txtCate.Text = cate.Catename;
            ents = con.Query<DbEntityEnterprise>("select ent.* from enterprise ent join choice_enterprise cent on ent.id=cent.enterprise_id where cent.checkaction_id=@actionID", new
            {
                actionID = this.actionID,
                
            }).ToList();


            CheckAction ca=con.Query<CheckAction>("select id,create_at from check_action where id=@actionID", new
            {
                actionID = this.actionID
            }).Single();

            txtCreatAt.Text = ca.Create_at.ToLongDateString();
            this.createAt = ca.Create_at.ToLongDateString();

            con.Close();

            int entNum = ents.Count;
           
            if (entNum > 0)
            {
                for (int i = 0; i < entNum; i++)
                {
                    TextBox txtEntName = new TextBox();
                    txtEntName.Width = 845;
                    txtEntName.Height = 25;
                    txtEntName.Left = 60;
                    txtEntName.Top = 3 + i * (25 + 10);
                    txtEntName.Parent = pnlEnterprise;
                    txtEntName.Font = new Font("宋体", txtChargeMan.Font.Size, txtChargeMan.Font.Style);
                    txtEntName.Text=(i+1)+"、"+ents[i].Name+"("+ents[i].Addr+")";
                   
                }

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SQLiteConnectionStringBuilder sb = new SQLiteConnectionStringBuilder();
            sb.DataSource = databasename;
            SQLiteConnection con = new SQLiteConnection(sb.ToString());
            con.Open();

            int result = con.Execute("update check_action set delflag=@delflag where id=@actionID", new
            {
                delflag = true,
                actionID = this.actionID,
            });
            if (result > 0)
            {
                MessageBox.Show("数据已删除!", "信息");
                this.Close();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //打印报表
            
            FrmReport frmReport = new FrmReport(commonMen, chargeMen, area, cate, ents,createAt);
            frmReport.ShowDialog();
        }
    }
}
