using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Robin
{
    public class CommandClear : Command
    {
        public override void Execute(string[] args)
        {
            program.Clear();
            program.Speak("Console cleared.");
        }
    }
}
