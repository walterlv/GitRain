using System.Diagnostics;

namespace Cvte.GitRain.Data
{
    [DebuggerDisplay("[{RepoName}] {LocalDirectory}")]
    public class GitRepoEntry : NotificationObject
    {
        private string _repoName;
        private string _localDirectory;

        public string RepoName
        {
            get { return _repoName; }
            set { SetProperty(ref _repoName, value); }
        }

        public string LocalDirectory
        {
            get { return _localDirectory; }
            set { SetProperty(ref _localDirectory, value); }
        }

        public GitRepoEntry()
        {
        }

        public GitRepoEntry(string directory)
        {
            _localDirectory = directory;
        }

        internal GitRepoEntry(string name, string directory)
        {
            _repoName = name;
            _localDirectory = directory;
        }
    }
}
