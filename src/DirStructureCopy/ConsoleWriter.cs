using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace DirStructureCopy
{
    sealed class ConsoleWriter : IDisposable
    {
        [DllImport("kernel32.dll")]
        private static extern bool AttachConsole(int dwProcessId);
        private const int ATTACH_PARENT_PROCESS = -1;

        private StreamWriter stdOutWriter;

        public ConsoleWriter()
        {
            this.stdOutWriter = new StreamWriter(Console.OpenStandardOutput());
            stdOutWriter.AutoFlush = true;
            AttachConsole(ATTACH_PARENT_PROCESS);
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
            stdOutWriter.WriteLine(message);
        }

        public void Dispose()
        {
            stdOutWriter.Dispose();
        }
    }
}
