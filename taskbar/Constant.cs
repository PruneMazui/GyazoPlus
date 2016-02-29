using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace gyazo_plus
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class Constant
    {
        public static readonly string FILE_NAME_CAPTURE = Directory.GetParent(Application.ExecutablePath) + "\\gyazowinpp.exe";

        public static readonly string DIRECTORY_APPLICATION = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\GyazoPlus";
        public static readonly string FILE_NAME_INI = DIRECTORY_APPLICATION + "\\gyazowinpp.ini";

        public const string CONFIG_SECTION_CAPTURE = "gyazowinpp";
        public const string CONFIG_SECTION_TOOL = "gyazowin-plus";

        public const int HOTKEY_WINDOWS_MESSAGE = 0x0312;　// ホットキーのウィンドウズメッセージ
        public const int HOTKEY_ID = 0x1955; // 0x0000～0xbfff 内の適当な値

        public const int HOTKEY_MOD_ALT = 0x0001;
        public const int HOTKEY_MOD_CONTROL = 0x0002;
        public const int HOTKEY_MOD_SHIFT = 0x0004;
        public const int HOTKEY_MOD_WIN = 0x0008;

        public static readonly string ID_FILENAME = DIRECTORY_APPLICATION + "\\id.txt";

        public const int PORTNUMBER_MIN = 0;
        public const int PORTNUMBER_MAX = 65535;

        public enum ICON_DOUBLE_CLICK {
            NONE,
            CAPTURE,
            CONFIG,
            LIST
        }

        public static readonly Dictionary<ICON_DOUBLE_CLICK, string> ICON_DOUBLE_CLICK_ITEMS = new Dictionary<ICON_DOUBLE_CLICK, string> {
            { ICON_DOUBLE_CLICK.NONE,    "何もしない" },
            { ICON_DOUBLE_CLICK.CAPTURE, "画像を撮る" },
            { ICON_DOUBLE_CLICK.CONFIG,  "設定画面を開く" },
            { ICON_DOUBLE_CLICK.LIST,    "画像一覧を表示" }
        };

        public static readonly Dictionary<int, string> HOTKEY_MOD_ITEMS = new Dictionary<int, string> {
            { HOTKEY_MOD_CONTROL | HOTKEY_MOD_SHIFT, "Ctrl+Shift" },
            { HOTKEY_MOD_CONTROL | HOTKEY_MOD_ALT  , "Ctrl+Alt"   },
            { HOTKEY_MOD_CONTROL | HOTKEY_MOD_WIN  , "Ctrl+Win"   },
            { HOTKEY_MOD_CONTROL                   , "Ctrl"       },
            { HOTKEY_MOD_SHIFT | HOTKEY_MOD_ALT    , "Shift+Alt"  },
            { HOTKEY_MOD_SHIFT | HOTKEY_MOD_WIN    , "Shift+Win"  },
            { HOTKEY_MOD_SHIFT                     , "Shift"      },
            { HOTKEY_MOD_ALT | HOTKEY_MOD_WIN      , "Alt+Win"    },
            { HOTKEY_MOD_ALT                       , "Alt"        },
            { HOTKEY_MOD_WIN                       , "Win"        }
        };

        public static readonly Dictionary<int, string> HOTKEY_ITEMS = new Dictionary<int, string> {
            { (int)Keys.PrintScreen, "PrintScreen" },
            { (int)Keys.A          , "A"           },
            { (int)Keys.B          , "B"           },
            { (int)Keys.C          , "C"           },
            { (int)Keys.D          , "D"           },
            { (int)Keys.E          , "E"           },
            { (int)Keys.F          , "F"           },
            { (int)Keys.G          , "G"           },
            { (int)Keys.H          , "H"           },
            { (int)Keys.I          , "I"           },
            { (int)Keys.J          , "J"           },
            { (int)Keys.K          , "K"           },
            { (int)Keys.L          , "L"           },
            { (int)Keys.M          , "M"           },
            { (int)Keys.N          , "N"           },
            { (int)Keys.O          , "O"           },
            { (int)Keys.P          , "P"           },
            { (int)Keys.Q          , "Q"           },
            { (int)Keys.R          , "R"           },
            { (int)Keys.S          , "S"           },
            { (int)Keys.T          , "T"           },
            { (int)Keys.U          , "U"           },
            { (int)Keys.V          , "V"           },
            { (int)Keys.W          , "W"           },
            { (int)Keys.X          , "X"           },
            { (int)Keys.Y          , "Y"           },
            { (int)Keys.Z          , "Z"           },
            { (int)Keys.F1         , "F1"          },
            { (int)Keys.F2         , "F2"          },
            { (int)Keys.F3         , "F3"          },
            { (int)Keys.F4         , "F4"          },
            { (int)Keys.F5         , "F5"          },
            { (int)Keys.F6         , "F6"          },
            { (int)Keys.F7         , "F7"          },
            { (int)Keys.F8         , "F8"          },
            { (int)Keys.F9         , "F9"          },
            { (int)Keys.F10        , "F10"         },
            { (int)Keys.F11        , "F11"         },
            { (int)Keys.F12        , "F12"         }
        };
    }
}
