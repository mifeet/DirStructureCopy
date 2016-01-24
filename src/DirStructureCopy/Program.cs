using System;
using System.IO;
using System.Resources;
using System.Windows.Forms;

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
            if (args.Length == 0)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm(resources));
            }
            else
            {
                Arguments arguments = parseArguments(args);
                if (arguments == null || arguments.Help)
                {
                    printUsageMessage();
                }
                else
                {
                    runCommandLineVersion(arguments, resources);
                }
            }
        }

        private static void runCommandLineVersion(Arguments arguments, ResourceManager resources)
        {
            using (ConsoleWriter consoleWriter = new ConsoleWriter())
            using (var copier = new StructureCopier(arguments.DestinationArchive, arguments.Zip, arguments.Flatten, resources))
            {
                try
                {
                    consoleWriter.WriteLine(String.Format(resources.GetString("startedProcessing"), arguments.SourceDirectory));
                    copier.CopyDirectoryStructure(new DirectoryInfo(arguments.SourceDirectory), () => false);
                    consoleWriter.WriteLine(String.Format(resources.GetString("finishedProcessing"), arguments.DestinationArchive));
                }
                catch (Exception e)
                {
                    consoleWriter.WriteLine(String.Format(resources.GetString("commandLineError") + ":\n{0}\n", e.Message));
                }
            }
        }

        private static void printUsageMessage()
        {
            using (ConsoleWriter consoleWriter = new ConsoleWriter())
            {
                consoleWriter.WriteLine();
                consoleWriter.WriteLine("  Usage:");
                consoleWriter.WriteLine();
                consoleWriter.WriteLine(String.Format("    {0} [/flatten] [/zip] [/help] SOURCE_DIRECTORY DESTINATION_ARCHIVE", System.AppDomain.CurrentDomain.FriendlyName));
                consoleWriter.WriteLine();
                consoleWriter.WriteLine("  /flatten    Remove subdirectory structure");
                consoleWriter.WriteLine("  /zip        Include zip archives");
                consoleWriter.WriteLine("  /help       Print this message and exit");
                consoleWriter.WriteLine();
            }
        }

        private static Arguments parseArguments(string[] args)
        {
            try
            {
                return Arguments.Parse(args);
            }
            catch
            {
                return null;
            }
        }
    }
}