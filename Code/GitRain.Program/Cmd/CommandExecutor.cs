using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Threading;

namespace Cvte.GitRain
{
    public static class CommandExecutor
    {
        public static event DataReceivedEventHandler ConsoleOutput;

        private static void RaiseConsoleOutput(DataReceivedEventArgs e)
        {
            Action action = () =>
            {
                var handler = ConsoleOutput;
                if (handler != null) handler(null, e);
            };
            if (Dispatcher.CurrentDispatcher == Application.Current.Dispatcher)
            {
                action();
            }
            else
            {
                Application.Current.Dispatcher.BeginInvoke(action);
            }
        }

        public static CommandResult Start(string command, params string[] arguments)
        {
            return Start(command, null, arguments);
        }

        public static CommandResult Start(string command, string workingDirectory, params string[] arguments)
        {
            CommandResult result = new CommandResult();
            using (Process cmd = new Process
            {
                StartInfo =
                {
                    FileName = command,
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                }
            })
            {
                if (!String.IsNullOrEmpty(workingDirectory))
                {
                    cmd.StartInfo.WorkingDirectory = workingDirectory;
                }

                arguments = arguments.Select(x => x.Contains(' ')
                    ? String.Format("\"{0}\"", x)
                    : x.Replace("\"", "")).ToArray();
                string parameter = arguments.Aggregate(String.Empty, (sum, add) => sum + " " + add).Trim();
                if (!String.IsNullOrEmpty(parameter))
                {
                    cmd.StartInfo.Arguments = parameter;
                }

                StringBuilder output = new StringBuilder();
                cmd.OutputDataReceived += (sender, args) =>
                {
                    if (args.Data == null)
                    {
                        return;
                    }
                    Debug.WriteLine(args.Data);
                    RaiseConsoleOutput(args);
                    output.Append(args.Data);
                };
                cmd.Start();
                cmd.BeginOutputReadLine();
                cmd.WaitForExit();
                result.Code = cmd.ExitCode;
                result.OutputText = output.ToString();
            }
            return result;
        }
    }
}
