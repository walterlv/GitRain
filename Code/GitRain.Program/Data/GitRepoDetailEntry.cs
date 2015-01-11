using System.Windows.Input;
using Cvte.GitRain.Git;

namespace Cvte.GitRain.Data
{
    public class GitRepoDetailEntry : GitRepoEntry
    {
        public GitRepoDetailEntry()
        {
            _syncCommand = new ActionCommand(Sync);
        }

        public GitRepoDetailEntry(GitRepoEntry entry) : this()
        {
            RepoName = entry.RepoName;
            LocalDirectory = entry.LocalDirectory;

            // 以下句子临时使用。
            HaveContentToSync = true;
        }

        private bool _haveContentToSync;
        private ICommand _syncCommand;

        public bool HaveContentToSync
        {
            get { return _haveContentToSync; }
            set { SetProperty(ref _haveContentToSync, value); }
        }

        public ICommand SyncCommand
        {
            get { return _syncCommand; }
            set { SetProperty(ref _syncCommand, value); }
        }

        private void Sync()
        {
            GitOperator.SyncWholeRepo();
        }
    }
}
