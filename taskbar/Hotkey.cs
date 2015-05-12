using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace gyazo_plus
{
    class Hotkey
    {
        private IntPtr handle;

        private bool registored = false;

        [DllImport("user32.dll")]
        extern static int RegisterHotKey(IntPtr HWnd, int ID, int MOD_KEY, int KEY);

        [DllImport("user32.dll")]
        extern static int UnregisterHotKey(IntPtr HWnd, int ID);

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="handle"></param>
        public Hotkey(IntPtr handle)
        {
            this.handle = handle;
        }

        /// <summary>
        /// デストラクタ
        /// </summary>
        ~Hotkey()
        {
            this.unregister();
        }

        /// <summary>
        /// ホットキーの登録
        /// </summary>
        public void registor(Config config)
        {
            if (this.registored)
            {
                return;
            }

            if (!config.itemsTool.enable_hotkey)
            {
                return;
            }

            // 設定値を一応検証
            if (
                !Constant.HOTKEY_MOD_ITEMS.ContainsKey(config.itemsTool.hotkey_mod) ||
                !Constant.HOTKEY_ITEMS.ContainsKey(config.itemsTool.hotkey)
            )
            {
                return;
            }

            RegisterHotKey(this.handle, Constant.HOTKEY_ID, config.itemsTool.hotkey_mod, config.itemsTool.hotkey);
            this.registored = true;
        }

        /// <summary>
        /// ホットキー再読み込み
        /// </summary>
        public void reload(Config config)
        {
            this.unregister();
            this.registor(config);
        }

        /// <summary>
        /// ホットキーの削除
        /// </summary>
        public void unregister()
        {
            if (!this.registored)
            {
                return;
            }

            UnregisterHotKey(this.handle, Constant.HOTKEY_ID);
            this.registored = false;
        }
    }
}
