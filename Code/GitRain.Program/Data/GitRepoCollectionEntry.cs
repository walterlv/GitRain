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
        private int _index;

        public int Index
        {
            get { return _index; }
            set { SetProperty(ref _index, value); }
        }

        public ObservableCollection<GitRepoEntry> Repos
        {
            get { return _gitRepos; }
        }

        private GitRepoCollectionEntry()
        {
            _gitRepos = new ObservableCollection<GitRepoEntry>();
            _index = -1;
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
