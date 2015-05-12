using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace gyazo_plus
{
    public partial class FormTaskBar : Form
    {
        private Hotkey hotkey;

        private Config config;

        private bool isOpenedConfigForm = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormTaskBar()
        {
            InitializeComponent();

            config = new Config();
            hotkey = new Hotkey(this.Handle);
            hotkey.registor(config);
            showHotkey(config);

            if (config.itemsTool.show_ballon)
            {
                notifyTaskBar.ShowBalloonTip(2000);
            }

            // 画像一覧のパス指定なしなら、メニュー「画像一覧」を無効化
            toolStripMenuCapturedList.Enabled = config.itemsTool.list_url.Length > 0;

            // 設定ができていない場合、設定フォームを開く
            if (!config.isValid())
            {
                openConfigForm();
            }
        }

        /// <summary>
        /// 設定フォームを開く
        /// </summary>
        private void openConfigForm()
        {
            if (isOpenedConfigForm)
            {
                return;
            }

            isOpenedConfigForm = true;
            FormConfig formConfig = new FormConfig(config);
            formConfig.ShowDialog(this);
            formConfig.Dispose();
            isOpenedConfigForm = false;

            // キーバインドの再読み込み
            hotkey.reload(config);
            showHotkey(config);

            // 画像一覧のパス指定なしなら、メニュー「画像一覧」を無効化
            toolStripMenuCapturedList.Enabled = config.itemsTool.list_url.Length > 0;
        }

        /// <summary>
        /// アップロード済み一覧を標準のブラウザで開く
        /// </summary>
        private void openCapturedList()
        {
            if (config.itemsTool.list_url.Length == 0)
            {
                Util.showOkDialog("ファイル一覧URLが設定されていません。", "エラー");
                return;
            }

            string idFilename = Constant.ID_FILENAME;

            if (!File.Exists(idFilename))
            {
                string msg = "クライアントIDファイルが見つかりませんでした。\n";
                msg += "一度もアップロード処理が行われていない可能性があります。";
                Util.showOkDialog(msg, "エラー");
                return;
            }

            StreamReader reader = new StreamReader(idFilename);
            string id = reader.ReadLine();
            reader.Close();

            string url = config.itemsTool.list_url + id;
            System.Diagnostics.Process.Start(url);
        }

        /// <summary>
        /// アイコン右クリックのコンテキストメニューにホットキーコマンドを表示
        /// </summary>
        /// <param name="config"></param>
        private void showHotkey(Config config)
        {
            if (
                !config.itemsTool.enable_hotkey ||
                !Constant.HOTKEY_MOD_ITEMS.ContainsKey(config.itemsTool.hotkey_mod) ||
                !Constant.HOTKEY_ITEMS.ContainsKey(config.itemsTool.hotkey)
            )
            {
                toolStripMenuCapture.ShortcutKeyDisplayString = "";
                return;
            }

            string text = Constant.HOTKEY_MOD_ITEMS[config.itemsTool.hotkey_mod];
            text += "+" + Constant.HOTKEY_ITEMS[config.itemsTool.hotkey];

            toolStripMenuCapture.ShortcutKeyDisplayString = text;
        }

        /// <summary>
        /// メニュー 終了 クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuExit_Click(object sender, EventArgs e)
        {
            if (Util.showOkCancelDialog("アプリケーションを終了しますか？", "確認"))
            {
                notifyTaskBar.Visible = false;
                Application.Exit();
            }
        }

        /// <summary>
        /// メニュー キャプチャ クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuCapture_Click(object sender, EventArgs e)
        {
            // 設定が正しくない場合編集画面を開く
            if (!config.isValid())
            {
                openConfigForm();
                return;
            }

            CaptureRunner.run();
        }

        /// <summary>
        /// メニュー 設定 クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuConfig_Click(object sender, EventArgs e)
        {
            openConfigForm();
        }

        /// <summary>
        /// メニュー アップロード済み一覧 クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuCapturedList_Click(object sender, EventArgs e)
        {
            openCapturedList();
        }

        /// <summary>
        /// ホットキーが押されたときの動作
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == Constant.HOTKEY_WINDOWS_MESSAGE && (int)m.WParam == Constant.HOTKEY_ID)
            {
                // 設定が正しくない場合編集画面を開く
                if (!config.isValid())
                {
                    openConfigForm();
                    return;
                }

                CaptureRunner.run();
            }
        }

        /// <summary>
        /// アイコンダブルクリック時の動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyTaskBar_DoubleClick(object sender, EventArgs e)
        {
            switch (config.itemsTool.icon_double_click)
            {
                case Constant.ICON_DOUBLE_CLICK.CAPTURE:
                    // 設定が正しくない場合編集画面を開く
                    if (!config.isValid())
                    {
                        openConfigForm();
                        return;
                    }

                    CaptureRunner.run();
                    break;

                case Constant.ICON_DOUBLE_CLICK.CONFIG:
                    openConfigForm();
                    break;

                case Constant.ICON_DOUBLE_CLICK.LIST:
                    openCapturedList();
                    break;
            }
        }
    }
}
