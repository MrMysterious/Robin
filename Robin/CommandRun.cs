using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace Robin
{
    public class CommandRun : Command
    {
        public override void Execute(string[] args)
        {
            string parameters = "";

            if (args.Length > 2)
            {
                parameters = "/C ";

                foreach (string s in args)
                {
                    parameters += String.Format("{0} ", s);
                }
            }

            try
            {
                Process process = new Process();

                process.StartInfo.FileName = args[1];
                process.StartInfo.Arguments = parameters;

                process.Start();
            }

            catch (Exception ex)
            {
                program.HandleException(ex, 0);
            }
        }
    }
}
