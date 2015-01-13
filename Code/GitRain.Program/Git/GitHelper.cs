using System.IO;
using System.Linq;

namespace Cvte.GitRain.Git
{
    public class GitHelper
    {
        public static bool CheckDirectoryIsGitRepo(string dir)
        {
            while (true)
            {
                DirectoryInfo info = new DirectoryInfo(dir);
                if (!info.Exists)
                {
                    return false;
                }
                if (File.Exists(Path.Combine(info.FullName, ".git", "config")))
                {
                    return true;
                }
                DirectoryInfo parent = info.Parent;
                if (parent == null)
                {
                    return false;
                }
                dir = parent.FullName;
            }
        }

        public static bool CheckDirectoryCanCloneGitRepo(string dir)
        {
            if (CheckDirectoryIsGitRepo(dir))
            {
                return false;
            }
            DirectoryInfo info = new DirectoryInfo(dir);
            if (!info.Exists)
            {
                return true;
            }
            if (!info.EnumerateFileSystemInfos().Any())
            {
                return true;
            }
            return false;
        }

        public static bool CheckDirectoryCanCreateGitRepo(string dir)
        {
            return !CheckDirectoryIsGitRepo(dir);
        }
    }
}
