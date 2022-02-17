namespace WebRunLocal
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.FoldPanelGroup = new System.Windows.Forms.Panel();
            this.btnPrinter = new System.Windows.Forms.Button();
            this.FoldPanel = new System.Windows.Forms.Panel();
            this.buttonAbout = new System.Windows.Forms.Button();
            this.buttonSetting = new System.Windows.Forms.Button();
            this.IcnTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.CmsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TsmiRestore = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.FoldPanelGroup.SuspendLayout();
            this.CmsMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // FoldPanelGroup
            // 
            this.FoldPanelGroup.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.FoldPanelGroup.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.FoldPanelGroup.Controls.Add(this.FoldPanel);
            this.FoldPanelGroup.Controls.Add(this.buttonAbout);
            this.FoldPanelGroup.Controls.Add(this.buttonSetting);
            this.FoldPanelGroup.Controls.Add(this.btnPrinter);
            this.FoldPanelGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FoldPanelGroup.Location = new System.Drawing.Point(0, 0);
            this.FoldPanelGroup.Margin = new System.Windows.Forms.Padding(2);
            this.FoldPanelGroup.Name = "FoldPanelGroup";
            this.FoldPanelGroup.Size = new System.Drawing.Size(282, 600);
            this.FoldPanelGroup.TabIndex = 1;
            // 
            // btnPrinter
            // 
            this.btnPrinter.BackColor = System.Drawing.Color.White;
            this.btnPrinter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnPrinter.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPrinter.Location = new System.Drawing.Point(0, 565);
            this.btnPrinter.Margin = new System.Windows.Forms.Padding(2);
            this.btnPrinter.Name = "btnPrinter";
            this.btnPrinter.Size = new System.Drawing.Size(278, 31);
            this.btnPrinter.TabIndex = 3;
            this.btnPrinter.Text = "打印设计";
            this.btnPrinter.UseVisualStyleBackColor = false;
            this.btnPrinter.Click += new System.EventHandler(this.FoldButtonClick);
            // 
            // FoldPanel
            // 
            this.FoldPanel.BackColor = System.Drawing.SystemColors.Window;
            this.FoldPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FoldPanel.Location = new System.Drawing.Point(0, 31);
            this.FoldPanel.Margin = new System.Windows.Forms.Padding(2);
            this.FoldPanel.Name = "FoldPanel";
            this.FoldPanel.Size = new System.Drawing.Size(278, 503);
            this.FoldPanel.TabIndex = 4;
            // 
            // buttonAbout
            // 
            this.buttonAbout.BackColor = System.Drawing.Color.White;
            this.buttonAbout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonAbout.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonAbout.Location = new System.Drawing.Point(0, 534);
            this.buttonAbout.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAbout.Name = "buttonAbout";
            this.buttonAbout.Size = new System.Drawing.Size(278, 31);
            this.buttonAbout.TabIndex = 2;
            this.buttonAbout.Text = "关于";
            this.buttonAbout.UseVisualStyleBackColor = false;
            this.buttonAbout.Click += new System.EventHandler(this.FoldButtonClick);
            // 
            // buttonSetting
            // 
            this.buttonSetting.BackColor = System.Drawing.Color.White;
            this.buttonSetting.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonSetting.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonSetting.Location = new System.Drawing.Point(0, 0);
            this.buttonSetting.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSetting.Name = "buttonSetting";
            this.buttonSetting.Size = new System.Drawing.Size(278, 31);
            this.buttonSetting.TabIndex = 1;
            this.buttonSetting.Text = "设置";
            this.buttonSetting.UseVisualStyleBackColor = false;
            this.buttonSetting.Click += new System.EventHandler(this.FoldButtonClick);
            // 
            // IcnTray
            // 
            this.IcnTray.ContextMenuStrip = this.CmsMenu;
            this.IcnTray.DoubleClick += new System.EventHandler(this.IcnTray_DoubleClick);
            // 
            // CmsMenu
            // 
            this.CmsMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.CmsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsmiRestore,
            this.TsmiSetting,
            this.TsmiLogout});
            this.CmsMenu.Name = "cmsMenu";
            this.CmsMenu.Size = new System.Drawing.Size(101, 70);
            // 
            // TsmiRestore
            // 
            this.TsmiRestore.Name = "TsmiRestore";
            this.TsmiRestore.Size = new System.Drawing.Size(100, 22);
            this.TsmiRestore.Text = "还原";
            this.TsmiRestore.Click += new System.EventHandler(this.TsmiRestore_Click);
            // 
            // TsmiSetting
            // 
            this.TsmiSetting.Name = "TsmiSetting";
            this.TsmiSetting.Size = new System.Drawing.Size(100, 22);
            this.TsmiSetting.Text = "设置";
            this.TsmiSetting.Click += new System.EventHandler(this.TsmiSetting_Click);
            // 
            // TsmiLogout
            // 
            this.TsmiLogout.Name = "TsmiLogout";
            this.TsmiLogout.Size = new System.Drawing.Size(100, 22);
            this.TsmiLogout.Text = "退出";
            this.TsmiLogout.Click += new System.EventHandler(this.TsmiLogout_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(282, 600);
            this.Controls.Add(this.FoldPanelGroup);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = global::WebRunLocal.Properties.Settings.Default.appName;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FoldPanelGroup.ResumeLayout(false);
            this.CmsMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel FoldPanelGroup;
        private System.Windows.Forms.Button buttonAbout;
        private System.Windows.Forms.Panel FoldPanel;
        private System.Windows.Forms.Button buttonSetting;
        private System.Windows.Forms.NotifyIcon IcnTray;
        private System.Windows.Forms.ContextMenuStrip CmsMenu;
        private System.Windows.Forms.ToolStripMenuItem TsmiRestore;
        private System.Windows.Forms.ToolStripMenuItem TsmiSetting;
        private System.Windows.Forms.ToolStripMenuItem TsmiLogout;
        private System.Windows.Forms.Button btnPrinter;
    }
}

