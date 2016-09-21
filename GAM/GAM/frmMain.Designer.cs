namespace GAM
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.systemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AreaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.CommonManToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ChargeManToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EntToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EntAddToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EntQueryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EntLoadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.randomGenerateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.historyDdataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 719);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 26, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1362, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.systemToolStripMenuItem,
            this.EntToolStripMenuItem,
            this.generateToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(11, 4, 0, 4);
            this.menuStrip1.Size = new System.Drawing.Size(1362, 33);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // systemToolStripMenuItem
            // 
            this.systemToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TypeToolStripMenuItem,
            this.CateToolStripMenuItem,
            this.AreaToolStripMenuItem,
            this.toolStripMenuItem1,
            this.CommonManToolStripMenuItem,
            this.ChargeManToolStripMenuItem,
            this.toolStripMenuItem2,
            this.ExitToolStripMenuItem});
            this.systemToolStripMenuItem.Name = "systemToolStripMenuItem";
            this.systemToolStripMenuItem.Size = new System.Drawing.Size(54, 25);
            this.systemToolStripMenuItem.Text = "系统";
            // 
            // TypeToolStripMenuItem
            // 
            this.TypeToolStripMenuItem.Name = "TypeToolStripMenuItem";
            this.TypeToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.TypeToolStripMenuItem.Text = "企业类型";
            this.TypeToolStripMenuItem.Click += new System.EventHandler(this.TypeToolStripMenuItem_Click);
            // 
            // CateToolStripMenuItem
            // 
            this.CateToolStripMenuItem.Name = "CateToolStripMenuItem";
            this.CateToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.CateToolStripMenuItem.Text = "管理类别";
            this.CateToolStripMenuItem.Click += new System.EventHandler(this.CateToolStripMenuItem_Click);
            // 
            // AreaToolStripMenuItem
            // 
            this.AreaToolStripMenuItem.Name = "AreaToolStripMenuItem";
            this.AreaToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.AreaToolStripMenuItem.Text = "区域";
            this.AreaToolStripMenuItem.Click += new System.EventHandler(this.AreaToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(141, 6);
            // 
            // CommonManToolStripMenuItem
            // 
            this.CommonManToolStripMenuItem.Name = "CommonManToolStripMenuItem";
            this.CommonManToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.CommonManToolStripMenuItem.Text = "工作人员";
            this.CommonManToolStripMenuItem.Click += new System.EventHandler(this.CommonManToolStripMenuItem_Click);
            // 
            // ChargeManToolStripMenuItem
            // 
            this.ChargeManToolStripMenuItem.Name = "ChargeManToolStripMenuItem";
            this.ChargeManToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.ChargeManToolStripMenuItem.Text = "组长";
            this.ChargeManToolStripMenuItem.Click += new System.EventHandler(this.ChargeManToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(141, 6);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.ExitToolStripMenuItem.Text = "退出";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // EntToolStripMenuItem
            // 
            this.EntToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EntAddToolStripMenuItem,
            this.EntQueryToolStripMenuItem,
            this.EntLoadToolStripMenuItem});
            this.EntToolStripMenuItem.Name = "EntToolStripMenuItem";
            this.EntToolStripMenuItem.Size = new System.Drawing.Size(54, 25);
            this.EntToolStripMenuItem.Text = "企业";
            // 
            // EntAddToolStripMenuItem
            // 
            this.EntAddToolStripMenuItem.Name = "EntAddToolStripMenuItem";
            this.EntAddToolStripMenuItem.Size = new System.Drawing.Size(112, 26);
            this.EntAddToolStripMenuItem.Text = "添加";
            this.EntAddToolStripMenuItem.Click += new System.EventHandler(this.EntAddToolStripMenuItem_Click);
            // 
            // EntQueryToolStripMenuItem
            // 
            this.EntQueryToolStripMenuItem.Name = "EntQueryToolStripMenuItem";
            this.EntQueryToolStripMenuItem.Size = new System.Drawing.Size(112, 26);
            this.EntQueryToolStripMenuItem.Text = "查询";
            this.EntQueryToolStripMenuItem.Click += new System.EventHandler(this.EntQueryToolStripMenuItem_Click);
            // 
            // EntLoadToolStripMenuItem
            // 
            this.EntLoadToolStripMenuItem.Name = "EntLoadToolStripMenuItem";
            this.EntLoadToolStripMenuItem.Size = new System.Drawing.Size(112, 26);
            this.EntLoadToolStripMenuItem.Text = "导入";
            this.EntLoadToolStripMenuItem.Click += new System.EventHandler(this.EntLoadToolStripMenuItem_Click);
            // 
            // generateToolStripMenuItem
            // 
            this.generateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.randomGenerateToolStripMenuItem,
            this.historyDdataToolStripMenuItem});
            this.generateToolStripMenuItem.Name = "generateToolStripMenuItem";
            this.generateToolStripMenuItem.Size = new System.Drawing.Size(54, 25);
            this.generateToolStripMenuItem.Text = "生成";
            // 
            // randomGenerateToolStripMenuItem
            // 
            this.randomGenerateToolStripMenuItem.Name = "randomGenerateToolStripMenuItem";
            this.randomGenerateToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.randomGenerateToolStripMenuItem.Text = "随机抽取";
            this.randomGenerateToolStripMenuItem.Click += new System.EventHandler(this.randomGenerateToolStripMenuItem_Click);
            // 
            // historyDdataToolStripMenuItem
            // 
            this.historyDdataToolStripMenuItem.Name = "historyDdataToolStripMenuItem";
            this.historyDdataToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.historyDdataToolStripMenuItem.Text = "历史数据";
            this.historyDdataToolStripMenuItem.Click += new System.EventHandler(this.historyDdataToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 33);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStrip1.Size = new System.Drawing.Size(1362, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1362, 741);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "汕头市文化广电新闻出版局文化市场监管“双随机”抽查系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripMenuItem systemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AreaToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CommonManToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ChargeManToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem EntToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EntAddToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EntQueryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem randomGenerateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem historyDdataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EntLoadToolStripMenuItem;
    }
}

