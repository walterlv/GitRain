using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Cvte.GitRain.Configs;
using Cvte.GitRain.UI;

namespace Cvte.GitRain.Git
{
    public class GitCommandExecutor
    {
        private readonly DirectoryInfo _directory;

        public GitCommandExecutor(string localDirectory)
        {
            if (!Directory.Exists(localDirectory))
            {
                Directory.CreateDirectory(localDirectory);
            }
            _directory = new DirectoryInfo(localDirectory);
        }

        private CommandResult Execute(params string[] arguments)
        {
            return CommandExecutor.Start(UserConfig.Instance.Executable.Msysgit,
                _directory.FullName, arguments);
        }

        private async Task<CommandResult> ExecuteAsync(params string[] arguments)
        {
            MessageService.Current.Show(new ConsoleControl
            {
                ConsoleText = arguments.Aggregate("git", (sum, add) => sum + " " + add)
            });
            Task<CommandResult> task = new Task<CommandResult>(() => Execute(arguments));
            task.Start();
            CommandResult result = await task;
            MessageService.Current.Show(new ConsoleControl
            {
                ConsoleText = String.IsNullOrEmpty(result.OutputText) ? "Exit" : result.OutputText
            });
            return result;
        }

        public async Task<CommandResult> InitAsync()
        {
            return await ExecuteAsync("init");
        }

        public async Task<CommandResult> AddRemoteAsync(string name, string url)
        {
            return await ExecuteAsync("remote", "add", name, url);
        }

        public async Task<CommandResult> CommitAsync(string message)
        {
            return await ExecuteAsync("commit", "-m", message);
        }

        public async Task<CommandResult> PushAsync(string remoteName, string branchName)
        {
            return await ExecuteAsync("push", "-u", remoteName, branchName);
        }

        public async Task<CommandResult> CloneAsync(string url, string dir)
        {
            return await ExecuteAsync("clone", url, dir);
        }
    }
}
