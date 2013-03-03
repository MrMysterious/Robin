using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Robin
{
    public abstract class Command
    {
        private static List<Command> commands = new List<Command>();

        protected Program program = Program.GetInstance();
        protected string command;
        protected int parameters;

        public string Name
        {
            get { return (command); }
            set { command = value; }
        }

        public int Parameters
        {
            get { return (parameters); }
            set { parameters = value; }
        }

        public abstract void Execute(string[] args);

        public static Command GetCommand(string s)
        {
            foreach (Command c in commands)
            {
                if (c.Name.ToUpper() != s.ToUpper())
                {
                    continue;
                }

                return (c);
            }

            return (null);
        }

        public static List<Command> GetCommands()
        {
            return (commands);
        }

        public static void Register(Command c, string s, int i)
        {
            c.Name = s;
            c.Parameters = i;

            Program.GetInstance().GetChoices().Add(s);

            commands.Add(c);
        }

        public static void Unregister(Command c)
        {
            commands.Remove(c);
        }
    }
}
