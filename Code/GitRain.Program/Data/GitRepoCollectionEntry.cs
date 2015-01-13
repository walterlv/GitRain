using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace Cvte.GitRain.Data
{
    [DebuggerDisplay("{Repos}")]
    public class GitRepoCollectionEntry : NotificationObject
    {
        public static GitRepoCollectionEntry Instance = new GitRepoCollectionEntry();

        private readonly ObservableCollection<GitRepoEntry> _gitRepos;
        public ReadOnlyObservableCollection<GitRepoEntry> Repos { get; private set; }

        private GitRepoCollectionEntry()
        {
            _gitRepos = new ObservableCollection<GitRepoEntry>();
            Repos = new ReadOnlyObservableCollection<GitRepoEntry>(_gitRepos);
            LoadAllFromUserFile();
        }

        public bool Contains(string dir)
        {
            return _gitRepos.Any(x => x.LocalDirectory == dir);
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
