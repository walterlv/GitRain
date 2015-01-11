namespace Cvte.GitRain.Git
{
    public class GitCore
    {
    }

    public class GitRemote
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Fetch { get; set; }
    }

    public class GitBranch
    {
        public string Remote { get; set; }
        public string Merge { get; set; }
    }

    public class GitUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
