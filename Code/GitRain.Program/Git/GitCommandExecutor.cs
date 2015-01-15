using System.IO;
using Cvte.GitRain.Configs;

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
            return CommandExecutor.Start(UserConfig.Instance.Executable.Msysgit, _directory.FullName, arguments);
        }

        public CommandResult Init()
        {
            return Execute("init");
        }

        public CommandResult AddRemote(string name, string url)
        {
            return Execute("remote", "add", name, url);
        }

        public CommandResult Commit(string message)
        {
            return Execute("commit", "-m", message);
        }

        public CommandResult Push(string remoteName, string branchName)
        {
            return Execute("push", "-u", remoteName, branchName);
        }

        public CommandResult Clone(string url, string dir)
        {
            return Execute("clone", url, dir);
        }
    }
}
