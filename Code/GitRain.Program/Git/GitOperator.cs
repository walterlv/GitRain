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

        public static void CreateRepo(string url, string localDirectory)
        {
            GitCommandExecutor git = new GitCommandExecutor(new DirectoryInfo(localDirectory).FullName);
            git.Init();
            if (!String.IsNullOrEmpty(url))
            {
                git.AddRemote("origin", url);
            }
        }

        public static void CloneRepo(string url, string localDirectory)
        {
            GitCommandExecutor git = new GitCommandExecutor(new DirectoryInfo(localDirectory).FullName);
            git.Clone(url, localDirectory);
        }
    }
}
