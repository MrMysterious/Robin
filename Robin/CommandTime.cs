using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Robin
{
    public class CommandTime : Command
    {
        public override void Execute(string[] args)
        {
            program.Output(String.Format("The time is: {0:HH:mm:ss}", DateTime.Now), ConsoleColor.Green);
        }
    }
}
