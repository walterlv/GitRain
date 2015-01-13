using System.Diagnostics;

namespace Cvte.GitRain.Data
{
    [DebuggerDisplay("[{RepoName}] {LocalDirectory}")]
    public class GitRepoEntry : NotificationObject
    {
        private string _repoName;
        private string _localDirectory;
        private bool _isStared;
        private string _alias;

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

        public bool IsStared
        {
            get { return _isStared; }
            set { SetProperty(ref _isStared, value); }
        }

        public string Alias
        {
            get { return _alias; }
            set { SetProperty(ref _alias, value); }
        }
    }
}
