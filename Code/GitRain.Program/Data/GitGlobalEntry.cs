using System;
using System.Linq;
using System.Windows.Input;
using Cvte.GitRain.Git;

// ReSharper disable InconsistentNaming

namespace Cvte.GitRain.Data
{
    public class GitGlobalEntry : NotificationObject
    {
        public static readonly GitGlobalEntry Instance = new GitGlobalEntry();

        private GitGlobalEntry()
        {
            _cloneOrCreateRepoCommand = new ActionCommand(x => CloneOrCreateRepo((GitCloneParameters) x));
        }

        private ICommand _cloneOrCreateRepoCommand;

        public ICommand CloneOrCreateRepoCommand
        {
            get { return _cloneOrCreateRepoCommand; }
            set { SetProperty(ref _cloneOrCreateRepoCommand, value); }
        }

        private void CloneOrCreateRepo(GitCloneParameters parameters)
        {
            string url = parameters.Url;
            string dir = parameters.LocalPath;
            string alias = parameters.Alias;

            if (String.IsNullOrEmpty(dir))
            {
                // 没有指定本地路径。
                return;
            }

            if (GitRepoCollectionEntry.Instance.Contains(dir))
            {
                GlobalCommands.BackToRepo.Execute(GitRepoCollectionEntry.Instance.Repos
                    .FirstOrDefault(x => x.LocalDirectory == dir));
                return;
            }

            bool isRemoteValid = !String.IsNullOrEmpty(url);
            bool isGitRepo = GitHelper.CheckDirectoryIsGitRepo(dir);
            bool canLocalClone = GitHelper.CheckDirectoryCanCloneGitRepo(dir);
            bool canLocalCreate = GitHelper.CheckDirectoryCanCreateGitRepo(dir);

            if (isGitRepo)
            {
                AddRepo(dir, alias);
            }
            else if (isRemoteValid)
            {
                if (canLocalClone)
                {
                    CloneRepo(url, dir, alias);
                }
            }
            else if (canLocalCreate)
            {
                CreateRepo(dir, alias);
            }
            GlobalCommands.BackToRepo.Execute(GitRepoCollectionEntry.Instance.Repos
                .FirstOrDefault(x => x.LocalDirectory == dir));
        }

        private void AddRepo(string dir, string alias)
        {
            GitRepoCollectionEntry.Instance.Repos.Add(new GitRepoEntry
            {
                Alias = alias,
                LocalDirectory = dir,
            });
        }

        private void CloneRepo(string url, string dir, string alias)
        {
            GitRepoCollectionEntry.Instance.Repos.Add(new GitRepoEntry
            {
                Alias = alias,
                LocalDirectory = dir,
            });
            // 开始执行克隆命令。
        }

        private void CreateRepo(string dir, string alias)
        {
            GitRepoCollectionEntry.Instance.Repos.Add(new GitRepoEntry
            {
                Alias = alias,
                LocalDirectory = dir,
            });
            // 开始执行创建命令。
        }
    }

    public class GitCloneParameters
    {
        public string Url { get; private set; }
        public string LocalPath { get; private set; }
        public string Alias { get; private set; }

        public GitCloneParameters(string url, string localPath, string alias)
        {
            Url = url;
            LocalPath = localPath;
            Alias = alias;
        }
    }
}
