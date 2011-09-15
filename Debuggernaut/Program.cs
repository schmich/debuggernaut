using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Debuggernaut
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            DynamicAssemblyResolver.AddAssembly(
                "Microsoft.VisualStudio.VirtualTreeGrid, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                Resources.VirtualTreeGridAssembly
            );

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
