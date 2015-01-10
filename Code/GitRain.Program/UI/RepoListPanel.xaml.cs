using System.Windows;
using System.Windows.Controls;
using Cvte.GitRain.Data;

namespace Cvte.GitRain.UI
{
    public partial class RepoListPanel : UserControl
    {
        public RepoListPanel()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty SelectedRepoProperty = DependencyProperty.Register(
            "SelectedRepo", typeof (GitRepoEntry), typeof (RepoListPanel),
            new PropertyMetadata(default(GitRepoEntry)));

        public GitRepoEntry SelectedRepo
        {
            get { return (GitRepoEntry) GetValue(SelectedRepoProperty); }
            set { SetValue(SelectedRepoProperty, value); }
        }
    }
}
