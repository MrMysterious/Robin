using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Robin
{
    public class CommandHelp : Command
    {
        public override void Execute(string[] args)
        {
            program.Output("Commands:", ConsoleColor.Green);

            foreach (string s in Command.GetCommands().Select(s => s.Name))
            {
                program.Output(s);
            }
        }
    }
}
