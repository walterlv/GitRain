using System.Collections.Generic;
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
        }

        public bool Contains(string dir)
        {
            return _gitRepos.Any(x => x.LocalDirectory == dir);
        }

        public void Reload(IEnumerable<GitRepoEntry> entries)
        {
            _gitRepos.Clear();
            foreach (GitRepoEntry entry in entries)
            {
                _gitRepos.Add(entry);
            }
        }

        private void VerifyAllAsync()
        {
        }
    }
}
