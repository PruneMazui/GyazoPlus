namespace gyazo_plus
{
    partial class FormConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConfig));
            this.textUploadServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textUploadPath = new System.Windows.Forms.TextBox();
            this.checkUseAuth = new System.Windows.Forms.CheckBox();
            this.checkUseSsl = new System.Windows.Forms.CheckBox();
            this.checkSslCheckCert = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textAuthId = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textAuthPw = new System.Windows.Forms.TextBox();
            this.checkUpDialog = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.checkCopyUrl = new System.Windows.Forms.CheckBox();
            this.checkCopyDialog = new System.Windows.Forms.CheckBox();
            this.checkOpenBrowser = new System.Windows.Forms.CheckBox();
            this.checkEnableHotkey = new System.Windows.Forms.CheckBox();
            this.comboHotkeyMod = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.comboHotkey = new System.Windows.Forms.ComboBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.checkWriteLog = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.comboIconDoubleClick = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.buttonOpenFileDialog = new System.Windows.Forms.Button();
            this.textLogFilename = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label14 = new System.Windows.Forms.Label();
            this.textListUrl = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.fileDialogUploadLog = new System.Windows.Forms.SaveFileDialog();
            this.labelVersion = new System.Windows.Forms.Label();
            this.checkShowBalloon = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // textUploadServer
            // 
            this.textUploadServer.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textUploadServer.Location = new System.Drawing.Point(128, 2);
            this.textUploadServer.MaxLength = 1024;
            this.textUploadServer.Name = "textUploadServer";
            this.textUploadServer.Size = new System.Drawing.Size(200, 25);
            this.textUploadServer.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(6, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "アップロードホスト";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(6, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "アップロードパス";
            // 
            // textUploadPath
            // 
            this.textUploadPath.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textUploadPath.Location = new System.Drawing.Point(116, 33);
            this.textUploadPath.MaxLength = 512;
            this.textUploadPath.Name = "textUploadPath";
            this.textUploadPath.Size = new System.Drawing.Size(212, 25);
            this.textUploadPath.TabIndex = 3;
            // 
            // checkUseAuth
            // 
            this.checkUseAuth.AutoSize = true;
            this.checkUseAuth.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkUseAuth.Location = new System.Drawing.Point(9, 156);
            this.checkUseAuth.Name = "checkUseAuth";
            this.checkUseAuth.Size = new System.Drawing.Size(160, 22);
            this.checkUseAuth.TabIndex = 4;
            this.checkUseAuth.Text = "BASIC認証を有効にする";
            this.checkUseAuth.UseVisualStyleBackColor = true;
            this.checkUseAuth.CheckedChanged += new System.EventHandler(this.checkUseAuth_CheckedChanged);
            // 
            // checkUseSsl
            // 
            this.checkUseSsl.AutoSize = true;
            this.checkUseSsl.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkUseSsl.Location = new System.Drawing.Point(9, 82);
            this.checkUseSsl.Name = "checkUseSsl";
            this.checkUseSsl.Size = new System.Drawing.Size(182, 22);
            this.checkUseSsl.TabIndex = 5;
            this.checkUseSsl.Text = "SSLを使用してアップロード";
            this.checkUseSsl.UseVisualStyleBackColor = true;
            this.checkUseSsl.CheckedChanged += new System.EventHandler(this.checkUseSsl_CheckedChanged);
            // 
            // checkSslCheckCert
            // 
            this.checkSslCheckCert.AutoSize = true;
            this.checkSslCheckCert.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkSslCheckCert.Location = new System.Drawing.Point(40, 110);
            this.checkSslCheckCert.Name = "checkSslCheckCert";
            this.checkSslCheckCert.Size = new System.Drawing.Size(135, 22);
            this.checkSslCheckCert.TabIndex = 6;
            this.checkSslCheckCert.Text = "証明書の検証をする";
            this.checkSslCheckCert.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(6, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(224, 18);
            this.label3.TabIndex = 7;
            this.label3.Text = "　　　　　　　　　　　　　　　　　　";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(6, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(224, 18);
            this.label4.TabIndex = 8;
            this.label4.Text = "　　　　　　　　　　　　　　　　　　";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.Location = new System.Drawing.Point(37, 187);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 18);
            this.label5.TabIndex = 9;
            this.label5.Text = "ユーザー名";
            // 
            // textAuthId
            // 
            this.textAuthId.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textAuthId.Location = new System.Drawing.Point(110, 184);
            this.textAuthId.MaxLength = 512;
            this.textAuthId.Name = "textAuthId";
            this.textAuthId.Size = new System.Drawing.Size(218, 25);
            this.textAuthId.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.Location = new System.Drawing.Point(37, 215);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 18);
            this.label6.TabIndex = 11;
            this.label6.Text = "パスワード";
            // 
            // textAuthPw
            // 
            this.textAuthPw.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textAuthPw.Location = new System.Drawing.Point(110, 215);
            this.textAuthPw.MaxLength = 512;
            this.textAuthPw.Name = "textAuthPw";
            this.textAuthPw.Size = new System.Drawing.Size(218, 25);
            this.textAuthPw.TabIndex = 12;
            // 
            // checkUpDialog
            // 
            this.checkUpDialog.AutoSize = true;
            this.checkUpDialog.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkUpDialog.Location = new System.Drawing.Point(9, 264);
            this.checkUpDialog.Name = "checkUpDialog";
            this.checkUpDialog.Size = new System.Drawing.Size(267, 22);
            this.checkUpDialog.TabIndex = 13;
            this.checkUpDialog.Text = "アップロード前に確認ダイヤログを表示する";
            this.checkUpDialog.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.Location = new System.Drawing.Point(6, 243);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(224, 18);
            this.label7.TabIndex = 14;
            this.label7.Text = "　　　　　　　　　　　　　　　　　　";
            // 
            // checkCopyUrl
            // 
            this.checkCopyUrl.AutoSize = true;
            this.checkCopyUrl.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkCopyUrl.Location = new System.Drawing.Point(9, 292);
            this.checkCopyUrl.Name = "checkCopyUrl";
            this.checkCopyUrl.Size = new System.Drawing.Size(219, 22);
            this.checkCopyUrl.TabIndex = 15;
            this.checkCopyUrl.Text = "画像URLをクリップボードにコピー";
            this.checkCopyUrl.UseVisualStyleBackColor = true;
            // 
            // checkCopyDialog
            // 
            this.checkCopyDialog.AutoSize = true;
            this.checkCopyDialog.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkCopyDialog.Location = new System.Drawing.Point(9, 320);
            this.checkCopyDialog.Name = "checkCopyDialog";
            this.checkCopyDialog.Size = new System.Drawing.Size(303, 22);
            this.checkCopyDialog.TabIndex = 16;
            this.checkCopyDialog.Text = "クリップボードへコピー時にダイヤログを表示する";
            this.checkCopyDialog.UseVisualStyleBackColor = true;
            // 
            // checkOpenBrowser
            // 
            this.checkOpenBrowser.AutoSize = true;
            this.checkOpenBrowser.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkOpenBrowser.Location = new System.Drawing.Point(9, 348);
            this.checkOpenBrowser.Name = "checkOpenBrowser";
            this.checkOpenBrowser.Size = new System.Drawing.Size(279, 22);
            this.checkOpenBrowser.TabIndex = 17;
            this.checkOpenBrowser.Text = "アップロード後に既定のブラウザで画像を表示";
            this.checkOpenBrowser.UseVisualStyleBackColor = true;
            // 
            // checkEnableHotkey
            // 
            this.checkEnableHotkey.AutoSize = true;
            this.checkEnableHotkey.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkEnableHotkey.Location = new System.Drawing.Point(6, 6);
            this.checkEnableHotkey.Name = "checkEnableHotkey";
            this.checkEnableHotkey.Size = new System.Drawing.Size(159, 22);
            this.checkEnableHotkey.TabIndex = 19;
            this.checkEnableHotkey.Text = "ホットキーを有効にする";
            this.checkEnableHotkey.UseVisualStyleBackColor = true;
            this.checkEnableHotkey.CheckedChanged += new System.EventHandler(this.checkEnableHotkey_CheckedChanged);
            // 
            // comboHotkeyMod
            // 
            this.comboHotkeyMod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboHotkeyMod.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.comboHotkeyMod.FormattingEnabled = true;
            this.comboHotkeyMod.Location = new System.Drawing.Point(40, 34);
            this.comboHotkeyMod.Name = "comboHotkeyMod";
            this.comboHotkeyMod.Size = new System.Drawing.Size(88, 26);
            this.comboHotkeyMod.TabIndex = 20;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label9.Location = new System.Drawing.Point(134, 37);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(20, 18);
            this.label9.TabIndex = 21;
            this.label9.Text = "＋";
            // 
            // comboHotkey
            // 
            this.comboHotkey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboHotkey.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.comboHotkey.FormattingEnabled = true;
            this.comboHotkey.Location = new System.Drawing.Point(160, 34);
            this.comboHotkey.Name = "comboHotkey";
            this.comboHotkey.Size = new System.Drawing.Size(98, 26);
            this.comboHotkey.TabIndex = 22;
            // 
            // buttonOk
            // 
            this.buttonOk.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonOk.Location = new System.Drawing.Point(154, 464);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(90, 25);
            this.buttonOk.TabIndex = 23;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label10.Location = new System.Drawing.Point(6, 63);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(224, 18);
            this.label10.TabIndex = 24;
            this.label10.Text = "　　　　　　　　　　　　　　　　　　";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonCancel.Location = new System.Drawing.Point(250, 464);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(90, 25);
            this.buttonCancel.TabIndex = 25;
            this.buttonCancel.Text = "キャンセル";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // checkWriteLog
            // 
            this.checkWriteLog.AutoSize = true;
            this.checkWriteLog.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkWriteLog.Location = new System.Drawing.Point(9, 376);
            this.checkWriteLog.Name = "checkWriteLog";
            this.checkWriteLog.Size = new System.Drawing.Size(183, 22);
            this.checkWriteLog.TabIndex = 26;
            this.checkWriteLog.Text = "アップロードログを出力する";
            this.checkWriteLog.UseVisualStyleBackColor = true;
            this.checkWriteLog.CheckedChanged += new System.EventHandler(this.checkWriteLog_CheckedChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label11.Location = new System.Drawing.Point(12, 521);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(224, 18);
            this.label11.TabIndex = 27;
            this.label11.Text = "　　　　　　　　　　　　　　　　　　";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label12.Location = new System.Drawing.Point(3, 81);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(188, 18);
            this.label12.TabIndex = 28;
            this.label12.Text = "アイコンダブルクリック時の動作";
            // 
            // comboIconDoubleClick
            // 
            this.comboIconDoubleClick.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboIconDoubleClick.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.comboIconDoubleClick.FormattingEnabled = true;
            this.comboIconDoubleClick.Location = new System.Drawing.Point(197, 78);
            this.comboIconDoubleClick.Name = "comboIconDoubleClick";
            this.comboIconDoubleClick.Size = new System.Drawing.Size(131, 26);
            this.comboIconDoubleClick.TabIndex = 29;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(344, 458);
            this.tabControl1.TabIndex = 30;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.buttonOpenFileDialog);
            this.tabPage1.Controls.Add(this.textLogFilename);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.checkUseAuth);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.textUploadServer);
            this.tabPage1.Controls.Add(this.textUploadPath);
            this.tabPage1.Controls.Add(this.checkWriteLog);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.checkUseSsl);
            this.tabPage1.Controls.Add(this.checkSslCheckCert);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.textAuthId);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.textAuthPw);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.checkOpenBrowser);
            this.tabPage1.Controls.Add(this.checkCopyDialog);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.checkUpDialog);
            this.tabPage1.Controls.Add(this.checkCopyUrl);
            this.tabPage1.Location = new System.Drawing.Point(4, 27);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(336, 427);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "キャプチャ設定";
            // 
            // buttonOpenFileDialog
            // 
            this.buttonOpenFileDialog.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonOpenFileDialog.Location = new System.Drawing.Point(272, 396);
            this.buttonOpenFileDialog.Name = "buttonOpenFileDialog";
            this.buttonOpenFileDialog.Size = new System.Drawing.Size(56, 25);
            this.buttonOpenFileDialog.TabIndex = 31;
            this.buttonOpenFileDialog.Text = "参照";
            this.buttonOpenFileDialog.UseVisualStyleBackColor = true;
            this.buttonOpenFileDialog.Click += new System.EventHandler(this.buttonOpenFileDialog_Click);
            // 
            // textLogFilename
            // 
            this.textLogFilename.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textLogFilename.Location = new System.Drawing.Point(87, 396);
            this.textLogFilename.MaxLength = 512;
            this.textLogFilename.Name = "textLogFilename";
            this.textLogFilename.Size = new System.Drawing.Size(179, 25);
            this.textLogFilename.TabIndex = 28;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.Location = new System.Drawing.Point(37, 399);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 18);
            this.label8.TabIndex = 27;
            this.label8.Text = "出力先";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.label15);
            this.tabPage2.Controls.Add(this.checkShowBalloon);
            this.tabPage2.Controls.Add(this.label14);
            this.tabPage2.Controls.Add(this.textListUrl);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Controls.Add(this.checkEnableHotkey);
            this.tabPage2.Controls.Add(this.comboIconDoubleClick);
            this.tabPage2.Controls.Add(this.comboHotkeyMod);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.comboHotkey);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Location = new System.Drawing.Point(4, 27);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(336, 427);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "動作設定";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label14.Location = new System.Drawing.Point(3, 131);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(92, 18);
            this.label14.TabIndex = 32;
            this.label14.Text = "画像一覧のURL";
            // 
            // textListUrl
            // 
            this.textListUrl.Location = new System.Drawing.Point(101, 128);
            this.textListUrl.MaxLength = 1024;
            this.textListUrl.Name = "textListUrl";
            this.textListUrl.Size = new System.Drawing.Size(227, 25);
            this.textListUrl.TabIndex = 31;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label13.Location = new System.Drawing.Point(6, 107);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(224, 18);
            this.label13.TabIndex = 30;
            this.label13.Text = "　　　　　　　　　　　　　　　　　　";
            // 
            // fileDialogUploadLog
            // 
            this.fileDialogUploadLog.Filter = "ログファイル|*.log|テキストファイル|*.txt|すべてのファイル|*.*";
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelVersion.Location = new System.Drawing.Point(10, 467);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(54, 18);
            this.labelVersion.TabIndex = 32;
            this.labelVersion.Text = "Version ";
            // 
            // checkShowBalloon
            // 
            this.checkShowBalloon.AutoSize = true;
            this.checkShowBalloon.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkShowBalloon.Location = new System.Drawing.Point(6, 177);
            this.checkShowBalloon.Name = "checkShowBalloon";
            this.checkShowBalloon.Size = new System.Drawing.Size(315, 22);
            this.checkShowBalloon.TabIndex = 33;
            this.checkShowBalloon.Text = "起動時に通知を表示する（再起動後有効になります）";
            this.checkShowBalloon.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label15.Location = new System.Drawing.Point(3, 156);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(224, 18);
            this.label15.TabIndex = 34;
            this.label15.Text = "　　　　　　　　　　　　　　　　　　";
            // 
            // FormConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 501);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormConfig";
            this.Text = "Gyazo Plus 設定";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textUploadServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textUploadPath;
        private System.Windows.Forms.CheckBox checkUseAuth;
        private System.Windows.Forms.CheckBox checkUseSsl;
        private System.Windows.Forms.CheckBox checkSslCheckCert;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textAuthId;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textAuthPw;
        private System.Windows.Forms.CheckBox checkUpDialog;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox checkCopyUrl;
        private System.Windows.Forms.CheckBox checkCopyDialog;
        private System.Windows.Forms.CheckBox checkOpenBrowser;
        private System.Windows.Forms.CheckBox checkEnableHotkey;
        private System.Windows.Forms.ComboBox comboHotkeyMod;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboHotkey;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox checkWriteLog;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox comboIconDoubleClick;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button buttonOpenFileDialog;
        private System.Windows.Forms.TextBox textLogFilename;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.SaveFileDialog fileDialogUploadLog;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textListUrl;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.CheckBox checkShowBalloon;
    }
}