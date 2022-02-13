using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UICommon;
using FuzzySharp;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace MPA
{
    public partial class MPAForm : Form
    {
        private Themes themes;
        private List<string> CommandHistory;
        private Dictionary<string, Command> Commands;
        private List<string> CommandsList;

        public class Command
        {
            public string selector;
            public MethodInfo methodInfo;
            public string arguments;

            public Command(string select, MethodInfo mi)
            {
                selector = select;
                methodInfo = mi;
            }

            public dynamic Run(RichTextBox ansbox)
            {
                if(methodInfo is null)
                {
                    MessageBox.Show("The command info for " + selector + " was not found!");
                    return null;
                }
                try
                {
                    List<object> allArg = new List<object>() { };

                    // add the string[] of arguments to the 
                    allArg.Add(arguments);
                    allArg.Add(ansbox);

                    if (allArg.Count == 2)
                        return methodInfo.Invoke(this, allArg.ToArray());

                    return null;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    AnswerBoxAdd(ansbox, e.Message + arguments.Length);

                    return null;
                }
            }
        }

        public MPAForm()
        {
            InitializeComponent();

            AnswerBox.HideSelection = false;

            themes = new Themes(AnswerBox, InputTx, ThemeCB, Instructions1Lab, FormBackgroundColor);

            // default theme
            themes.Pulse();

            CommandHistory = new List<string>();
            Commands = new Dictionary<string, Command>();
            CommandsList = new List<string>();

            RegisterCommand(new Command("messagebox", this.GetType().GetMethod("ShowMessageBox")));
            RegisterCommand(new Command("quit", this.GetType().GetMethod("QuitApp")));
            RegisterCommand(new Command("exit", this.GetType().GetMethod("QuitApp")));
        }

        public static void AnswerBoxAdd(RichTextBox anbox, string text)
        {
            anbox.AppendText(text + "\n");
        }

        public void RegisterCommand(Command command)
        {
            Commands.Add(command.selector, command);
            CommandsList.Add(command.selector);
        }

        public void FormBackgroundColor(Color color)
        {
            this.BackColor = color;
        }

        private void ThemeCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            themes.CBUpdateTheme();
        }

        private void InputTx_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                HandleCommandEnter();
            }
        }

        private void HandleCommandEnter()
        {
            // user entered a command
            string tx = InputTx.Text;

            InputTx.Text = "";

            CommandHistory.Add(tx);

            string[] split = tx.Split(" ");
            int argc = split.Length - 1;

            // do a fuzzy lookup of the command in the commands list
            var result = FuzzySharp.Process.ExtractOne(split[0], CommandsList);
            Command chosenCommand = Commands[result.Value];

            // either take out the arguments or leave as null
            List<string> args = new List<string>();

            if (split.Length > 1)
            {
                // leave out the first thing in split which is the command, all the rest are args
                foreach (string s in split[1..])
                {
                    args.Add(s);
                }
            }
            else
                args = null;

            if (args is null)
            {
                Debug.WriteLine("null");
            }
            else if (args is not null)
            {
                StringBuilder argstring = new StringBuilder();
                foreach (string s in args)
                {
                    argstring.Append(s);
                }
                chosenCommand.arguments = argstring.ToString();
            }

            dynamic output = chosenCommand.Run(AnswerBox);
        }

        // Command functions MUST be static!
        // Command functions must have a string as the first parameter
        // Command functions' last parameter must be a rich text box
        public static void ShowMessageBox(string param, RichTextBox ansbox)
        {
            AnswerBoxAdd(ansbox, "Displaying MessageBox with text: " + param);
            MessageBox.Show(param);
        }

        public static void QuitApp(string param, RichTextBox ansbox)
        {
            AnswerBoxAdd(ansbox, "Shutting down...");
            Application.Exit();
        }

        public static void Help(string param, RichTextBox ansbox)
        {
            AnswerBoxAdd(ansbox, "These are the active commands: ");

        }
    }
}
