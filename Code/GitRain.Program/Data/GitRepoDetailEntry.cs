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
        }
    }
}
