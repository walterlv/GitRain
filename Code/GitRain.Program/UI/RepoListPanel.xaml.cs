using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Cvte.GitRain.Data;

namespace Cvte.GitRain.UI
{
    public partial class RepoListPanel : UserControl
    {
        public RepoListPanel()
        {
            GlobalCommand.Register(GlobalCommands.BackToRepo.Key, OnBackHere);
            InitializeComponent();
            Loaded += OnLoaded;
            GlobalCommand.AnyExecuted += GlobalCommand_AnyExecuted;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (RepoListBox.Items.Count == 0)
            {
                GlobalCommands.GitCreateOrCloneRepo.Execute(null);
            }
            else if (RepoListBox.SelectedIndex < 0)
            {
                RepoListBox.SelectedIndex = 0;
            }
        }

        private void GlobalCommand_AnyExecuted(object sender, GlobalCommandEventArgs e)
        {
            if (e.Key.StartsWith("Git"))
            {
                RepoListBox.SelectedIndex = -1;
            }
        }

        private void OnBackHere(object o)
        {
            if (o == null)
            {
                if (GitRepoCollectionEntry.Instance.Index >= 0)
                {
                    RepoListBox.SelectedIndex = GitRepoCollectionEntry.Instance.Index;
                }
            }
            else if (o is Int32)
            {
                int value = (int) o;
                if (value >= -1 && value < RepoListBox.Items.Count)
                {
                    RepoListBox.SelectedIndex = value;
                }
            }
            else if (o is GitRepoEntry)
            {
                if (RepoListBox.Items.Contains(o))
                {
                    RepoListBox.SelectedItem = o;
                }
                else
                {
                    GitRepoEntry entry = (GitRepoEntry) o;
                    RepoListBox.SelectedItem = RepoListBox.Items.OfType<GitRepoEntry>()
                        .FirstOrDefault(x => x.LocalDirectory == entry.LocalDirectory);
                }
            }
        }

        public static readonly DependencyProperty SelectedRepoProperty = DependencyProperty.Register(
            "SelectedRepo", typeof (GitRepoEntry), typeof (RepoListPanel),
            new PropertyMetadata(default(GitRepoEntry)));

        public GitRepoEntry SelectedRepo
        {
            get { return (GitRepoEntry) GetValue(SelectedRepoProperty); }
            set { SetValue(SelectedRepoProperty, value); }
        }

        private void RepoListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RepoListBox.SelectedIndex >= 0)
            {
                GitRepoCollectionEntry.Instance.Index = RepoListBox.SelectedIndex;
            }
            if (e.AddedItems.Count > 0)
            {
                OnSelected();
            }
        }

        public void Reselect()
        {
            RepoListBox.SelectedIndex = GitRepoCollectionEntry.Instance.Index;
        }

        public event EventHandler Selected;

        private void OnSelected()
        {
            var handler = Selected;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
