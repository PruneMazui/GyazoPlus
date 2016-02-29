using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gyazo_plus
{
    public partial class FormConfig : Form
    {
        /// <summary>
        /// コンボボックスのアイテム格納オブジェクト
        /// </summary>
        class ComboboxItem
        {
            public string text { get; set; }
            public int value { get; set; }

            public ComboboxItem(int value, string text)
            {
                this.text = text;
                this.value = value;
            }

            public ComboboxItem(Constant.ICON_DOUBLE_CLICK value, string text)
            {
                this.text = text;
                this.value = (int) value;
            }
        }

        private Config config;

        private bool isValid()
        {
            string msg = "";

            if (this.textUploadServer.Text.Length == 0)
            {
                msg += "アップロードホストを入力してください。\n";
            }

            if (this.textUploadPort.Text.Length != 0)
            {
                int number;
                if (!int.TryParse(this.textUploadPort.Text, out number))
                {
                    msg += "ポート番号は数値を入力してください。\n";
                }
                else if(Constant.PORTNUMBER_MIN > number || number > Constant.PORTNUMBER_MAX)
                {
                    msg += "ポート番号は" + Constant.PORTNUMBER_MIN.ToString() + "以上" + Constant.PORTNUMBER_MAX.ToString() + "以下の数値を入力してください。\n";
                }
            }

            if (this.textUploadPath.Text.Length == 0)
            {
                msg += "アップロードパスを入力してください。\n";
            }

            if (msg.Length == 0)
            {
                return true;
            }
            Util.showOkDialog(msg);

            return false;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormConfig(Config config)
        {
            InitializeComponent();

            // バージョン情報の表示
            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            labelVersion.Text += version.Major + "." + version.Minor;

            // 設定の読み込み
            this.config = config;

            // キャプチャ設定
            textUploadServer.Text = config.itemsCapture.upload_server;
            textUploadPort.Text = config.itemsCapture.upload_port;
            textUploadPath.Text = config.itemsCapture.upload_path;

            checkUseSsl.Checked = config.itemsCapture.use_ssl;
            checkSslCheckCert.Checked = config.itemsCapture.ssl_check_cert;

            checkUseAuth.Checked = config.itemsCapture.use_auth;
            textAuthId.Text = config.itemsCapture.auth_id;
            textAuthPw.Text = config.itemsCapture.auth_pw;

            checkUpDialog.Checked = config.itemsCapture.up_dialog;
            checkCopyUrl.Checked = config.itemsCapture.copy_url;
            checkCopyDialog.Checked = config.itemsCapture.copy_dialog;
            checkOpenBrowser.Checked = config.itemsCapture.open_browser;
            checkWriteLog.Checked = config.itemsCapture.write_log;
            textLogFilename.Text = config.itemsCapture.log_filename;

            // 動作設定
            checkEnableHotkey.Checked = config.itemsTool.enable_hotkey;

            // Datasorce使うとバインドの関係で初期値を設定できないので自力でセット

            {
                int selectedIndex = 0;
                int currentIndex = 0;

                foreach (KeyValuePair<int, string> item in Constant.HOTKEY_MOD_ITEMS)
                {
                    comboHotkeyMod.Items.Add(new ComboboxItem(item.Key, item.Value));

                    if (item.Key == config.itemsTool.hotkey_mod)
                    {
                        selectedIndex = currentIndex;
                    }
                    currentIndex++;
                }
                comboHotkeyMod.DisplayMember = "text";
                comboHotkeyMod.ValueMember = "value";
                comboHotkeyMod.SelectedIndex = selectedIndex;
            }

            {
                int selectedIndex = 0;
                int currentIndex = 0;

                foreach (KeyValuePair<int, string> item in Constant.HOTKEY_ITEMS)
                {
                    comboHotkey.Items.Add(new ComboboxItem(item.Key, item.Value));

                    if (item.Key == config.itemsTool.hotkey)
                    {
                        selectedIndex = currentIndex;
                    }
                    currentIndex++;
                }
                comboHotkey.DisplayMember = "text";
                comboHotkey.ValueMember = "value";
                comboHotkey.SelectedIndex = selectedIndex;
            }

            {
                int selectedIndex = 0;
                int currentIndex = 0;

                foreach (KeyValuePair<Constant.ICON_DOUBLE_CLICK, string> item in Constant.ICON_DOUBLE_CLICK_ITEMS)
                {
                    comboIconDoubleClick.Items.Add(new ComboboxItem(item.Key, item.Value));

                    if (item.Key == config.itemsTool.icon_double_click)
                    {
                        selectedIndex = currentIndex;
                    }
                    currentIndex++;
                }
                comboIconDoubleClick.DisplayMember = "text";
                comboIconDoubleClick.ValueMember = "value";
                comboIconDoubleClick.SelectedIndex = selectedIndex;
            }
            textListUrl.Text = config.itemsTool.list_url;
            checkShowBalloon.Checked = config.itemsTool.show_ballon;

            changeEventCommon();
        }

        /// <summary>
        /// OKボタンのクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (!this.isValid())
            {
                return;
            }

            Config config = this.config;

            // キャプチャ設定
            config.itemsCapture.upload_server = textUploadServer.Text;
            config.itemsCapture.upload_port = textUploadPort.Text;
            config.itemsCapture.upload_path = textUploadPath.Text;

            config.itemsCapture.use_ssl = checkUseSsl.Checked;
            config.itemsCapture.ssl_check_cert = checkSslCheckCert.Checked;

            config.itemsCapture.use_auth = checkUseAuth.Checked;
            config.itemsCapture.auth_id = textAuthId.Text;
            config.itemsCapture.auth_pw = textAuthPw.Text;

            config.itemsCapture.up_dialog = checkUpDialog.Checked;
            config.itemsCapture.copy_url = checkCopyUrl.Checked;
            config.itemsCapture.copy_dialog = checkCopyDialog.Checked;
            config.itemsCapture.open_browser = checkOpenBrowser.Checked;
            config.itemsCapture.write_log = checkWriteLog.Checked;
            config.itemsCapture.log_filename = textLogFilename.Text;

            // 動作設定
            config.itemsTool.enable_hotkey = checkEnableHotkey.Checked;
            config.itemsTool.hotkey_mod = ((ComboboxItem)comboHotkeyMod.SelectedItem).value;
            config.itemsTool.hotkey = ((ComboboxItem)comboHotkey.SelectedItem).value;
            config.itemsTool.icon_double_click = (Constant.ICON_DOUBLE_CLICK)((ComboboxItem)comboIconDoubleClick.SelectedItem).value;
            config.itemsTool.list_url = textListUrl.Text;
            config.itemsTool.show_ballon = checkShowBalloon.Checked;

            config.save();

            this.Close();
        }

        /// <summary>
        /// キャンセルボタンのクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 値の変更による共通イベント
        /// </summary>
        private void changeEventCommon()
        {
            checkSslCheckCert.Enabled = checkUseSsl.Checked;

            textAuthId.Enabled = checkUseAuth.Checked;
            textAuthPw.Enabled = checkUseAuth.Checked;

            comboHotkey.Enabled = checkEnableHotkey.Checked;
            comboHotkeyMod.Enabled = checkEnableHotkey.Checked;

            textLogFilename.Enabled = checkWriteLog.Checked;
            buttonOpenFileDialog.Enabled = checkWriteLog.Checked;
        }

        private void checkUseSsl_CheckedChanged(object sender, EventArgs e)
        {
            changeEventCommon();
        }

        private void checkUseAuth_CheckedChanged(object sender, EventArgs e)
        {
            changeEventCommon();
        }

        private void checkEnableHotkey_CheckedChanged(object sender, EventArgs e)
        {
            changeEventCommon();
        }

        private void checkWriteLog_CheckedChanged(object sender, EventArgs e)
        {
            changeEventCommon();
        }

        private void buttonOpenFileDialog_Click(object sender, EventArgs e)
        {
            fileDialogUploadLog.FileName = textLogFilename.Text; 
            if (DialogResult.OK == fileDialogUploadLog.ShowDialog())
            {
                textLogFilename.Text = fileDialogUploadLog.FileName;
            }
        }
    }
}
