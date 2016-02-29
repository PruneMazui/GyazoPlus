using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sgry.Ini;
using System.IO;
using System.Windows.Forms;

namespace gyazo_plus
{
    public class Config
    {
        /// <summary>
        /// キャプチャの動作に関連する設定項目
        /// </summary>
        public class ItemsCapture
        {
            public string upload_server = "";
            public string upload_port = "";
            public string upload_path = "";

            public bool use_ssl = false;
            public bool ssl_check_cert = true;

            public bool use_auth = false;
            public string auth_id = "";
            public string auth_pw = "";

            public bool up_dialog = false;
            public bool copy_url = true;
            public bool copy_dialog = false;
            public bool open_browser = true;
            public bool write_log = false;
            public string log_filename = "";
        }

        /// <summary>
        /// ツールの動作に関連する設定項目
        /// </summary>
        public class ItemsTool
        {
            public bool enable_hotkey = false;
            public int hotkey_mod = Constant.HOTKEY_MOD_CONTROL | Constant.HOTKEY_MOD_WIN;
            public int hotkey = (int)System.Windows.Forms.Keys.T;

            public Constant.ICON_DOUBLE_CLICK icon_double_click = Constant.ICON_DOUBLE_CLICK.CAPTURE;

            public string list_url = "";

            public bool show_ballon = true;
        }

        public ItemsCapture itemsCapture = new ItemsCapture();
        public ItemsTool itemsTool = new ItemsTool();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Config()
        {
            IniDocument ini = new IniDocument();

            if (!Directory.Exists(Constant.DIRECTORY_APPLICATION))
            {
                Directory.CreateDirectory(Constant.DIRECTORY_APPLICATION);
            }

            if (File.Exists(Constant.FILE_NAME_INI))
            {
                using (var file = new StreamReader(Constant.FILE_NAME_INI, Encoding.UTF8))
                {
                    ini.Load(file);

                    // キャプチャ設定
                    itemsCapture.upload_server = ini.Get(Constant.CONFIG_SECTION_CAPTURE, "upload_server", "");
                    itemsCapture.upload_port = ini.Get(Constant.CONFIG_SECTION_CAPTURE, "upload_port", "");
                    itemsCapture.upload_path = ini.Get(Constant.CONFIG_SECTION_CAPTURE, "upload_path", "");
                    itemsCapture.use_ssl = ini.Get(Constant.CONFIG_SECTION_CAPTURE, "use_ssl", "") == "yes" ? true : false;
                    itemsCapture.ssl_check_cert = ini.Get(Constant.CONFIG_SECTION_CAPTURE, "ssl_check_cert", "") == "yes" ? true : false;
                    itemsCapture.use_auth = ini.Get(Constant.CONFIG_SECTION_CAPTURE, "use_auth", "") == "yes" ? true : false;
                    itemsCapture.auth_id = ini.Get(Constant.CONFIG_SECTION_CAPTURE, "auth_id", "");
                    itemsCapture.auth_pw = ini.Get(Constant.CONFIG_SECTION_CAPTURE, "auth_pw", "");
                    itemsCapture.up_dialog = ini.Get(Constant.CONFIG_SECTION_CAPTURE, "up_dialog", "") == "yes" ? true : false;
                    itemsCapture.copy_url = ini.Get(Constant.CONFIG_SECTION_CAPTURE, "copy_url", "") == "yes" ? true : false;
                    itemsCapture.copy_dialog = ini.Get(Constant.CONFIG_SECTION_CAPTURE, "copy_dialog", "") == "yes" ? true : false;
                    itemsCapture.open_browser = ini.Get(Constant.CONFIG_SECTION_CAPTURE, "open_browser", "") == "yes" ? true : false;
                    itemsCapture.write_log = ini.Get(Constant.CONFIG_SECTION_CAPTURE, "write_log", "") == "yes" ? true : false;
                    itemsCapture.log_filename = ini.Get(Constant.CONFIG_SECTION_CAPTURE, "log_filename", "");

                    // 動作設定
                    itemsTool.enable_hotkey = ini.Get(Constant.CONFIG_SECTION_TOOL, "enable_hotkey", "") == "yes" ? true : false;
                    itemsTool.hotkey_mod = ini.GetInt(Constant.CONFIG_SECTION_TOOL, "hotkey_mod", 0, 0xFFFF, 0);
                    itemsTool.hotkey = ini.GetInt(Constant.CONFIG_SECTION_TOOL, "hotkey", 0, 0xFFFF, 0);

                    Constant.ICON_DOUBLE_CLICK icon_double_click;
                    if(Enum.TryParse<Constant.ICON_DOUBLE_CLICK>(
                        ini.Get(Constant.CONFIG_SECTION_TOOL, "icon_double_click", ""),
                        out icon_double_click
                    ))
                    {
                        itemsTool.icon_double_click = icon_double_click;
                    }

                    itemsTool.list_url = ini.Get(Constant.CONFIG_SECTION_TOOL, "list_url", "http://");
                    itemsTool.show_ballon = ini.Get(Constant.CONFIG_SECTION_TOOL, "show_ballon", "yes") == "yes" ? true : false;

                    file.Close();
                }
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        public void save()
        {
            try
            {
                IniDocument ini = new IniDocument();

                // キャプチャ設定
                ini.Set(Constant.CONFIG_SECTION_CAPTURE, "upload_server", itemsCapture.upload_server);
                ini.Set(Constant.CONFIG_SECTION_CAPTURE, "upload_port", itemsCapture.upload_port);
                ini.Set(Constant.CONFIG_SECTION_CAPTURE, "upload_path", itemsCapture.upload_path);
                ini.Set(Constant.CONFIG_SECTION_CAPTURE, "use_ssl", itemsCapture.use_ssl ? "yes" : "no");
                ini.Set(Constant.CONFIG_SECTION_CAPTURE, "ssl_check_cert", itemsCapture.ssl_check_cert ? "yes" : "no");
                ini.Set(Constant.CONFIG_SECTION_CAPTURE, "use_auth", itemsCapture.use_auth ? "yes" : "no");
                ini.Set(Constant.CONFIG_SECTION_CAPTURE, "auth_id", itemsCapture.auth_id);
                ini.Set(Constant.CONFIG_SECTION_CAPTURE, "auth_pw", itemsCapture.auth_pw);
                ini.Set(Constant.CONFIG_SECTION_CAPTURE, "up_dialog", itemsCapture.up_dialog ? "yes" : "no");
                ini.Set(Constant.CONFIG_SECTION_CAPTURE, "copy_url", itemsCapture.copy_url ? "yes" : "no");
                ini.Set(Constant.CONFIG_SECTION_CAPTURE, "copy_dialog", itemsCapture.copy_dialog ? "yes" : "no");
                ini.Set(Constant.CONFIG_SECTION_CAPTURE, "open_browser", itemsCapture.open_browser ? "yes" : "no");
                ini.Set(Constant.CONFIG_SECTION_CAPTURE, "write_log", itemsCapture.write_log ? "yes" : "no");
                ini.Set(Constant.CONFIG_SECTION_CAPTURE, "log_filename", itemsCapture.log_filename);

                // 動作設定
                ini.Set(Constant.CONFIG_SECTION_TOOL, "enable_hotkey", itemsTool.enable_hotkey ? "yes" : "no");
                ini.Set(Constant.CONFIG_SECTION_TOOL, "hotkey_mod", itemsTool.hotkey_mod);
                ini.Set(Constant.CONFIG_SECTION_TOOL, "hotkey", itemsTool.hotkey);
                ini.Set(Constant.CONFIG_SECTION_TOOL, "icon_double_click", itemsTool.icon_double_click);
                ini.Set(Constant.CONFIG_SECTION_TOOL, "list_url", itemsTool.list_url);
                ini.Set(Constant.CONFIG_SECTION_TOOL, "show_ballon", itemsTool.show_ballon ? "yes" : "no");

                var file = new StreamWriter(Constant.FILE_NAME_INI, false, Encoding.Default);

                file.NewLine = "\r\n";
                ini.Save(file);
                file.Close();
            } catch
            {
                Util.showOkDialog("設定ファイルの保存に失敗しました。", "エラー");
            }
        }

        /// <summary>
        /// 設定内容が正しいか返す
        /// </summary>
        /// <returns></returns>
        public bool isValid()
        {
            return
                this.itemsCapture.upload_server.Length > 0 && // アップロードホストが設定されているか
                this.itemsCapture.upload_path.Length > 0;     // アップロードパスが設定されているか
        }
    }
}
