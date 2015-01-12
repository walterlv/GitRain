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
            GlobalCommand.AnyExecuted += GlobalCommand_AnyExecuted;
        }

        private void GlobalCommand_AnyExecuted(object sender, GlobalCommandEventArgs e)
        {
            if (e.Key.StartsWith("Git"))
            {
                RepoListBox.SelectedIndex = -1;
            }
            else if (e.Key.StartsWith("Back"))
            {
                RepoListBox.SelectedIndex = _lastSelectedIndex;
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

        private int _lastSelectedIndex;

        public event EventHandler Selected;

        private void OnSelected()
        {
            var handler = Selected;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
