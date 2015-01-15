using System;
using System.IO;
using System.Windows;

namespace Cvte.GitRain.Git
{
    public static class GitOperator
    {
        /// <summary>
        /// 同步一整个 Git 仓库，这包括所有的分支和子模块。
        /// </summary>
        public static void SyncWholeRepo(string directory)
        {
            MessageBox.Show("假装正在同步……");
        }

        public async static void CreateRepo(string url, string localDirectory)
        {
            GitCommandExecutor git = new GitCommandExecutor(new DirectoryInfo(localDirectory).FullName);
            await git.InitAsync();
            if (!String.IsNullOrEmpty(url))
            {
                await git.AddRemoteAsync("origin", url);
            }
        }

        public static async void CloneRepo(string url, string localDirectory)
        {
            GitCommandExecutor git = new GitCommandExecutor(new DirectoryInfo(localDirectory).FullName);
            CommandResult result = await git.CloneAsync(url, localDirectory);
            MessageBox.Show(result.OutputText);
        }
    }
}
