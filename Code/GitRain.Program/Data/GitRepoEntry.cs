namespace Cvte.GitRain.Data
{
    public class GitRepoEntry : NotificationObject
    {
        private string _repoName;
        private string _rootFolderName;

        public string RepoName
        {
            get { return _repoName; }
            set { SetProperty(ref _repoName, value); }
        }

        public string RootFolderName
        {
            get { return _rootFolderName; }
            set { SetProperty(ref _rootFolderName, value); }
        }
    }
}
