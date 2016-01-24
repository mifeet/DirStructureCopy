using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel;
using System.Resources;
using System.IO;
using System.Runtime.InteropServices;

namespace DirStructureCopy
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            var resources = new ResourceManager("DirStructureCopy.UIStrings", typeof(MainForm).Assembly);
            if (args.Length == 2)
            {
                runCommandLineVersion(args[0], args[1], resources);
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm(resources));
            }
        }

        private static void runCommandLineVersion(string sourcePath, string destinationPath, ResourceManager resources)
        {
            using (ConsoleWriter consoleWriter = new ConsoleWriter())
            using (var copier = new StructureCopier(destinationPath, false, false, resources))
            {
                try
                {
                    consoleWriter.WriteLine(String.Format(resources.GetString("startedProcessing"), sourcePath));
                    copier.CopyDirectoryStructure(new DirectoryInfo(sourcePath), () => false);
                    consoleWriter.WriteLine(String.Format(resources.GetString("finishedProcessing"), destinationPath));
                }
                catch (Exception e)
                {
                    consoleWriter.WriteLine(String.Format(resources.GetString("commandLineError") + ":\n{0}\n", e.Message));
                }
            }
        }
    }
}