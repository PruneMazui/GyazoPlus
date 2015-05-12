namespace gyazo_plus
{
    partial class FormTaskBar
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTaskBar));
            this.notifyTaskBar = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuCapturedList = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyTaskBar
            // 
            this.notifyTaskBar.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyTaskBar.BalloonTipText = "アプリケーションを開始しました。";
            this.notifyTaskBar.BalloonTipTitle = "Gyazo Plus";
            this.notifyTaskBar.ContextMenuStrip = this.contextMenuStrip;
            this.notifyTaskBar.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyTaskBar.Icon")));
            this.notifyTaskBar.Text = "Gyazo Plus";
            this.notifyTaskBar.Visible = true;
            this.notifyTaskBar.DoubleClick += new System.EventHandler(this.notifyTaskBar_DoubleClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuCapture,
            this.toolStripMenuConfig,
            this.toolStripSeparator2,
            this.toolStripMenuCapturedList,
            this.toolStripSeparator1,
            this.toolStripMenuExit});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(137, 104);
            // 
            // toolStripMenuCapture
            // 
            this.toolStripMenuCapture.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.toolStripMenuCapture.Name = "toolStripMenuCapture";
            this.toolStripMenuCapture.ShortcutKeyDisplayString = "";
            this.toolStripMenuCapture.Size = new System.Drawing.Size(136, 22);
            this.toolStripMenuCapture.Text = "画像を撮る";
            this.toolStripMenuCapture.Click += new System.EventHandler(this.toolStripMenuCapture_Click);
            // 
            // toolStripMenuConfig
            // 
            this.toolStripMenuConfig.Name = "toolStripMenuConfig";
            this.toolStripMenuConfig.Size = new System.Drawing.Size(136, 22);
            this.toolStripMenuConfig.Text = "設定";
            this.toolStripMenuConfig.Click += new System.EventHandler(this.toolStripMenuConfig_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(133, 6);
            // 
            // toolStripMenuCapturedList
            // 
            this.toolStripMenuCapturedList.Name = "toolStripMenuCapturedList";
            this.toolStripMenuCapturedList.Size = new System.Drawing.Size(136, 22);
            this.toolStripMenuCapturedList.Text = "画像一覧";
            this.toolStripMenuCapturedList.Click += new System.EventHandler(this.toolStripMenuCapturedList_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(133, 6);
            // 
            // toolStripMenuExit
            // 
            this.toolStripMenuExit.Name = "toolStripMenuExit";
            this.toolStripMenuExit.Size = new System.Drawing.Size(136, 22);
            this.toolStripMenuExit.Text = "終了";
            this.toolStripMenuExit.Click += new System.EventHandler(this.toolStripMenuExit_Click);
            // 
            // FormTaskBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(235, 246);
            this.Name = "FormTaskBar";
            this.Text = "formTaskBar";
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyTaskBar;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuExit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuCapture;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuConfig;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuCapturedList;
    }
}

