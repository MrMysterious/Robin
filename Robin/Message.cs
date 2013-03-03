using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Speech.Recognition;

namespace Robin
{
    public class Message
    {
        public enum MessageType
        {
            NONE = -1,

            A = 0,
            B = 1,
            C = 2,
        }

        private static List<Message> list = new List<Message>();

        private string message;
        private List<string> answers;

        private MessageType type;

        public MessageType Type
        {
            get { return (type); }
            set { type = value; }
        }

        public string Value
        {
            get { return (message); }
            set { message = value; }
        }

        public string Answer
        {
            get { return (Answers[new Random().Next(Answers.Count)]); }
        }

        public List<string> Answers
        {
            get { return (answers); }
            set { answers = value; }
        }

        public static Message GetMessage(string s)
        {
            foreach (Message m in list)
            {
                if (m.Value.ToUpper() != s.ToUpper())
                {
                    continue;
                }

                return (m);
            }

            return (null);
        }

        public static List<Message> GetMessages()
        {
            return (list);
        }

        public static void Register(Message m, MessageType t, string s)
        {
            m.Type = t;
            m.Value = s;
            m.Answers = new List<string>();

            if (m.Type == MessageType.A)
            {
                m.Answers.AddRange(new string[]
                {
                    "No problem.",
                    "No problem, %user%.".Replace("%user%", Environment.UserName),

                    "You're welcome.",
                    "You're welcome, %user%.".Replace("%user%", Environment.UserName),
                });
            }

            if (m.Type == MessageType.B)
            {
                m.Answers.AddRange(new string[]
                {
                    "Thanks!",
                    "Thanks, %user%!".Replace("%user%", Environment.UserName),

                    "Haha, thanks!",
                    "Haha, thanks %user%!".Replace("%user%", Environment.UserName),
                });
            }

            if (m.Type == MessageType.C)
            {
                m.Answers.AddRange(new string[]
                {
                    "Haha!",
                    "Haha, lol!",
                    "Haha, funny!",
                    "Haha, %user%!".Replace("%user%", Environment.UserName),
                });
            }

            list.Add(m);

            Program.GetInstance().GetChoices().Add(s);
        }

        public static void Register(Message m, MessageType t, string s, string[] a)
        {
            m.Type = t;
            m.Value = s;
            m.Answers = a.ToList<string>();

            if (m.Type == MessageType.A)
            {
                m.Answers.AddRange(new string[]
                {
                    "No problem.",
                    "No problem, %user%.".Replace("%user%", Environment.UserName),

                    "You're welcome.",
                    "You're welcome, %user%.".Replace("%user%", Environment.UserName),
                });
            }

            if (m.Type == MessageType.B)
            {
                m.Answers.AddRange(new string[]
                {
                    "Thanks!",
                    "Thanks, %user%!".Replace("%user%", Environment.UserName),

                    "Haha, thanks!",
                    "Haha, thanks %user%!".Replace("%user%", Environment.UserName),
                });
            }

            if (m.Type == MessageType.C)
            {
                m.Answers.AddRange(new string[]
                {
                    "Haha!",
                    "Haha, lol!",
                    "Haha, funny!",
                    "Haha, %user%!".Replace("%user%", Environment.UserName),
                });
            }

            list.Add(m);

            Program.GetInstance().GetChoices().Add(s);
        }
    }
}
