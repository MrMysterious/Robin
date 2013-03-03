using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Robin
{
    public class CommandExit : Command
    {
        public override void Execute(string[] args)
        {
            program.Stop();
        }
    }
}
