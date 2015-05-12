using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace gyazo_plus
{
    class CaptureRunner
    {
        /// <summary>
        /// キャプチャプロセスの実行
        /// </summary>
        public static void run()
        {
            Process.Start(Constant.FILE_NAME_CAPTURE);
        }
    }
}
