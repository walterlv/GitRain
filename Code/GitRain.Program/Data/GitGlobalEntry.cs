using System;
using System.Windows;
using System.Windows.Input;

// ReSharper disable InconsistentNaming

namespace Cvte.GitRain.Data
{
    public class GitGlobalEntry : NotificationObject
    {
        public static readonly GitGlobalEntry Instance = new GitGlobalEntry();

        private GitGlobalEntry()
        {
            _cloneOrCreateRepoCommand = new ActionCommand(x => CloneOrCreateRepo((GitCloneParameters) x));
        }

        private ICommand _cloneOrCreateRepoCommand;

        public ICommand CloneOrCreateRepoCommand
        {
            get { return _cloneOrCreateRepoCommand; }
            set { SetProperty(ref _cloneOrCreateRepoCommand, value); }
        }

        private void CloneOrCreateRepo(GitCloneParameters parameters)
        {
            MessageBox.Show("从 " + parameters.Url + Environment.NewLine + "到 " + parameters.LocalPath, "假装正在创建仓库");
        }
    }

    public class GitCloneParameters
    {
        public string Url { get; private set; }
        public string LocalPath { get; private set; }

        public GitCloneParameters(string url, string localPath)
        {
            Url = url;
            LocalPath = localPath;
        }
    }
}
