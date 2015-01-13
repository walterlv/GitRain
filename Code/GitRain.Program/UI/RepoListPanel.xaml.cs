using System;
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
            Loaded += OnLoaded;
            GlobalCommand.AnyExecuted += GlobalCommand_AnyExecuted;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (RepoListBox.Items.Count == 0)
            {
                GlobalCommands.GitCreateOrCloneRepo.Execute(null);
            }
            else
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
            else if (e.Key.StartsWith("Back"))
            {
                if (_lastSelectedIndex >= 0)
                {
                    RepoListBox.SelectedIndex = _lastSelectedIndex;
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
                _lastSelectedIndex = RepoListBox.SelectedIndex;
            }
            if (e.AddedItems.Count > 0)
            {
                OnSelected();
            }
        }

        public void Reselect()
        {
            RepoListBox.SelectedIndex = _lastSelectedIndex;
        }

        private int _lastSelectedIndex = -1;

        public event EventHandler Selected;

        private void OnSelected()
        {
            var handler = Selected;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
