using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fakeData
{
    class CMDOptions
    {

        [Option('n', "num", HelpText = "Number of entries to generate")]
        public string numEntries { get; set; }

        [Option('d', "domain", HelpText = "Domain to append to generated emails")]
        public string emailDomain { get; set; }

        //[HelpOption]
        public string GetUsage()
        {
            var usage = new StringBuilder();
            usage.AppendLine("");
            usage.AppendLine("Commandline Options: ");
            usage.AppendLine("-n, --num     Number of entries to generate");
            usage.AppendLine("-d, --domain     Domain to append to generated emails");
            usage.AppendLine("");
            usage.AppendLine("Usage: ");
            usage.AppendLine("createData.exe -n 10000 -d test.com");
            return usage.ToString();
        }
    }
}
