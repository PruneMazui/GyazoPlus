using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace gyazo_plus
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //二重起動をチェックする
            if (System.Diagnostics.Process.GetProcessesByName(
                System.Diagnostics.Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (!System.IO.File.Exists(Constant.FILE_NAME_CAPTURE))
            {
                Util.showOkDialog(Constant.FILE_NAME_CAPTURE + "が見つかりません。\nアプリケーションを終了します。", "エラー");
                return;
            }
            new FormTaskBar();
            Application.Run();
        }
    }
}
