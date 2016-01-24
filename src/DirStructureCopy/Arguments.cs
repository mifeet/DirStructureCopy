using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DirStructureCopy
{
    class Arguments
    {
        public bool Flatten
        {
            get;
            private set;
        }

        public bool Zip
        {
            get;
            private set;
        }

        public bool Help
        {
            get;
            private set;
        }

        public string SourceDirectory
        {
            get;
            private set;
        }

        public string DestinationArchive
        {
            get;
            private set;
        }

        private Arguments()
        {
        }

        public static Arguments Parse(string[] args)
        {
            var arguments = new Arguments();
            HashSet<String> argumentSet = new HashSet<String>(args.Where(isOption));
            IList<String> positionalArgs = args.Where(a => !isOption(a)).ToList<String>();

            if (positionalArgs.Count != 2)
            {
                throw new ArgumentException("Invalid number of positional arguments");
            }

            arguments.SourceDirectory = positionalArgs[0];
            arguments.DestinationArchive = positionalArgs[1];

            arguments.Help = argumentSet.Contains("/help");
            arguments.Flatten = argumentSet.Contains("/flatten");
            arguments.Zip = argumentSet.Contains("/zip");

            return arguments;
        }

        private static bool isOption(string arg)
        {
            return arg.StartsWith("/");
        }
    }
}