using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Cvte.GitRain.Data
{
    [DebuggerDisplay("{Repos}")]
    public class GitRepoCollectionEntry : NotificationObject
    {
        private readonly ObservableCollection<GitRepoEntry> _gitRepos;
        public ReadOnlyObservableCollection<GitRepoEntry> Repos { get; private set; }

        public GitRepoCollectionEntry()
        {
            _gitRepos = new ObservableCollection<GitRepoEntry>();
            Repos = new ReadOnlyObservableCollection<GitRepoEntry>(_gitRepos);
            LoadAllFromUserFile();
        }

        private void LoadAllFromUserFile()
        {
            _gitRepos.Add(new GitRepoEntry("GitRain", ""));
            _gitRepos.Add(new GitRepoEntry("MetroRadiance", ""));
            _gitRepos.Add(new GitRepoEntry("miracle", ""));
            _gitRepos.Add(new GitRepoEntry("sheet-data", ""));
            _gitRepos.Add(new GitRepoEntry("vs-command-executor", ""));
        }

        private void VerifyAllAsync()
        {
        }
    }
}
