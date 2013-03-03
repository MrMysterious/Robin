using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Robin
{
    public class CommandSay : Command
    {
        public override void Execute(string[] args)
        {
            string message = "";

            for (int i = 1; i < args.Length; i++)
            {
                message += String.Format("{0} ", args[i]);
            }

            program.Output(message, ConsoleColor.Green);
        }
    }
}
