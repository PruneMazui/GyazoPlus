using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gyazo_plus
{
    /// <summary>
    /// ユーティリティ
    /// </summary>
    class Util
    {
        /// <summary>
        /// OKキャンセルダイヤログの表示
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static bool showOkCancelDialog(string message, string title)
        {
            return MessageBox.Show(message, title, MessageBoxButtons.OKCancel) == DialogResult.OK;
        }

        /// <summary>
        /// OKダイヤログの表示
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        public static void showOkDialog(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK);
        }

        /// <summary>
        /// OKダイヤログの表示
        /// </summary>
        /// <param name="message"></param>
        public static void showOkDialog(string message)
        {
            showOkDialog(message, "");
        }

        /// <summary>
        /// 未実装ダイヤログの表示
        /// </summary>
        public static void showNotImplementedDialog()
        {
            showOkDialog("未実装です", "");
        }
    }
}
