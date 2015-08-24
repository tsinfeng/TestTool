using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.InteropServices;

namespace ScriptRun
{
    class HelperApi
    {
        //start console
        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();
        //free console
        [DllImport("kernel32.dll")]
        private static extern bool FreeConsole();

        public class ConsoleShow
        {
            public static bool consoleState = false;
            public bool CsAllocConsole()
            {
                consoleState = true;
                return AllocConsole();
            }
            public bool CsFreeConsole()
            {
                consoleState = false;
                return FreeConsole();
            }
     
        }






    }
}
