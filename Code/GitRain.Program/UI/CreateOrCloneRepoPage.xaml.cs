using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;
using Cvte.GitRain.Data;
using Cvte.GitRain.Git;

namespace Cvte.GitRain.UI
{
    public partial class CreateOrCloneRepoPage : Page
    {
        private readonly DispatcherTimer _timer;
        private string _clone;
        private string _create;
        private string _add;
        private string _exist;

        public CreateOrCloneRepoPage()
        {
            _timer = new DispatcherTimer {Interval = TimeSpan.FromMilliseconds(500)};
            _timer.Tick += Timer_Tick;
            InitializeComponent();
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            _clone = (string)FindResource("Clone");
            _create = (string)FindResource("Create");
            _add = (string)FindResource("Add");
            _exist = (string)FindResource("Exist");
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            _timer.IsEnabled = false;
        }

        private void OnAnyTextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsLoaded)
            {
                _timer.Stop();
                _timer.Start();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _timer.IsEnabled = false;

            string remoteText = UrlTextBox.Text.Trim();
            string localText = LocalPathTextBox.Text.Trim();

            if (String.IsNullOrEmpty(localText))
            {
                CloneButton.Content = _clone;
                CloneTipTextBlock.Text = String.Empty;
                return;
            }

            if (GitRepoCollectionEntry.Instance.Contains(localText))
            {
                CloneButton.Content = _exist;
                CloneTipTextBlock.Text = (string)FindResource("ExistTip");
                return;
            }

            bool isRemoteValid = !String.IsNullOrEmpty(remoteText);
            bool isGitRepo = GitHelper.CheckDirectoryIsGitRepo(localText);
            bool canLocalClone = GitHelper.CheckDirectoryCanCloneGitRepo(localText);
            bool canLocalCreate = GitHelper.CheckDirectoryCanCreateGitRepo(localText);

            if (isGitRepo)
            {
                CloneButton.Content = _add;
                CloneTipTextBlock.Text = (string) FindResource("AddTip");
            }
            else if (isRemoteValid)
            {
                CloneButton.Content = _clone;
                if (canLocalClone)
                {
                    CloneTipTextBlock.Text = String.Empty;
                }
                else
                {
                    CloneTipTextBlock.Text = (string)FindResource("CannotCloneTip");
                }
            }
            else if (canLocalCreate)
            {
                CloneButton.Content = _create;
                CloneTipTextBlock.Text = String.Empty;
            }
        }
    }

    internal class TextBoxTextToGitCloneParameterConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return new GitCloneParameters((string) values[0], (string) values[1]);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
