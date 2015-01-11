namespace Cvte.GitRain.Data
{
    public class GitRepoDetailEntry : GitRepoEntry
    {
        public GitRepoDetailEntry()
        {
        }

        public GitRepoDetailEntry(GitRepoEntry entry)
        {
            RepoName = entry.RepoName;
            LocalDirectory = entry.LocalDirectory;
            HaveContentToSync = true;
        }

        public bool HaveContentToSync { get; set; }
    }
}
