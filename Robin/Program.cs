using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Globalization;
using System.Windows.Forms;
using System.IO;

namespace Robin
{
    public class Program
    {
        private static Program _this;

        private Thread threadIO;
        private SpeechSynthesizer speaker;
        private SpeechRecognitionEngine listener;
        private Choices choices;

        public Program()
        {
            Console.Title = ("Robin");

            threadIO = new Thread(new ThreadStart(HandlerIO));
            speaker = new SpeechSynthesizer();
            listener = new SpeechRecognitionEngine();
            choices = new Choices();

            listener.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(listener_SpeechRecognized);
        }

        private void listener_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Command command;
            Message message;

            string[] input = e.Result.Text.Split(' ', '\n');

            if ((command = Command.GetCommand(input[0])) != null)
            {
                if (command.Parameters > (input.Length - 1))
                {
                    Output(String.Format("AT LEAST {0} PARAMETERS ARE REQUIRED.", command.Parameters), ConsoleColor.Red);
                    return;
                }

                command.Execute(input);
                return;
            }

            if ((message = Message.GetMessage(e.Result.Text)) != null)
            {
                Output(message.Answer);
            }
        }

        private void HandlerIO()
        {
            try
            {
                string line;
                string[] input;

                Command command;
                Message message;

                while (true)
                {
                    if ((line = Console.ReadLine()) == null)
                    {
                        continue;
                    }

                    if ((message = Message.GetMessage(line)) != null)
                    {
                        Output(message.Answer);
                    }

                    input = line.Split(' ', '\n');

                    if((command = Command.GetCommand(input[0])) != null)
                    {
                        if (command.Parameters > (input.Length - 1))
                        {
                            Output(String.Format("AT LEAST {0} PARAMETERS ARE REQUIRED.", command.Parameters), ConsoleColor.Red);
                            continue;
                        }

                        command.Execute(input);
                        continue;
                    }
                }
            }

            catch (Exception ex)
            {
                HandleException(ex, 10000);
            }
        }

        public void HandleException(Exception e, int i)
        {
            Console.Beep();

            Clear();

            Output("--- Error ---", ConsoleColor.Red);
            Print(e.Message);

            Thread.Sleep(i);
        }

        public void Print(object o)
        {
            Console.WriteLine(o);
        }

        public void Print(object o, ConsoleColor c)
        {
            Console.ForegroundColor = c;
            Console.WriteLine(o);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void Speak(object o)
        {
            speaker.Speak(o.ToString());
        }

        public void Output(object o)
        {
            Print(o);
            Speak(o);
        }

        public void Output(object o, ConsoleColor c)
        {
            Print(o, c);
            Speak(o);
        }

        public void Clear()
        {
            Console.Clear();
        }

        public void Start()
        {
            int hour = DateTime.Now.Hour;

            if (hour < 5 && hour >= 0)
            {
                Output(String.Format("Good night, {0}.", Environment.UserName), ConsoleColor.Yellow);
            }

            if (hour >= 5 && hour < 12)
            {
                Output(String.Format("Good morning, {0}.", Environment.UserName), ConsoleColor.Yellow);
            }

            if (hour >= 12 && hour < 18)
            {
                Output(String.Format("Good afternoon, {0}.", Environment.UserName), ConsoleColor.Yellow);
            }

            if (hour >= 18 && hour <= 23)
            {
                Output(String.Format("Good evening, {0}.", Environment.UserName), ConsoleColor.Yellow);
            }

            listener.SetInputToDefaultAudioDevice();
            listener.RecognizeAsync(RecognizeMode.Multiple);
            threadIO.Start();
        }

        public void Stop()
        {
            Output(String.Format("Have a nice day, {0}.", Environment.UserName), ConsoleColor.Yellow);

            Environment.Exit(0);
        }

        public static Program GetInstance()
        {
            return (_this);
        }

        public SpeechSynthesizer GetSpeaker()
        {
            return (speaker);
        }

        public SpeechRecognitionEngine GetListener()
        {
            return (listener);
        }

        public Choices GetChoices()
        {
            return (choices);
        }

        public static void Main(string[] args)
        {
            _this = new Program();

            Command.Register(new CommandRun(),      "ROBIN RUN", 1);
            Command.Register(new CommandSay(),      "ROBIN SAY", 1);
            Command.Register(new CommandTime(),     "ROBIN TIME", 0);
            Command.Register(new CommandClear(),    "ROBIN CLEAR", 0);
            Command.Register(new CommandExit(),     "ROBIN EXIT", 0);

            Message.Register(new Message(), Message.MessageType.A, "Thanks!");
            Message.Register(new Message(), Message.MessageType.A, "Thank you!");
            Message.Register(new Message(), Message.MessageType.B, "COMP1");
            Message.Register(new Message(), Message.MessageType.B, "COMP2");
            Message.Register(new Message(), Message.MessageType.C, "JOKE1");
            Message.Register(new Message(), Message.MessageType.C, "JOKE2");

            Message.Register(new Message(), Message.MessageType.NONE, "How are you?", new string[] {

                "I'm a computer, dude...",
                "I'm a computer, so I'm fine.",
            });

            _this.Print("Available Files:", ConsoleColor.Green);

            foreach (string s in Directory.GetFiles(Directory.GetCurrentDirectory()).Select(s => Path.GetFileName(s)))
            {
                _this.Print(s);
                _this.choices.Add(String.Format("ROBIN RUN {0}", s));
            }

            GrammarBuilder grammarBuilder;

            grammarBuilder = new GrammarBuilder(_this.choices);
            grammarBuilder.Culture = new CultureInfo("en-US");

            _this.listener.LoadGrammar(new Grammar(grammarBuilder));
            _this.listener.SetInputToDefaultAudioDevice();
            _this.Start();
        }
    }
}
