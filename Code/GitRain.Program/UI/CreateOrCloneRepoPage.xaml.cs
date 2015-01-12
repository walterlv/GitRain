using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;
using Cvte.GitRain.Data;

namespace Cvte.GitRain.UI
{
    public partial class CreateOrCloneRepoPage : Page
    {
        private readonly DispatcherTimer _timer;
        private string _clone;
        private string _create;
        private string _add;

        public CreateOrCloneRepoPage()
        {
            _timer = new DispatcherTimer {Interval = TimeSpan.FromMilliseconds(500)};
            _timer.Tick += Timer_Tick;
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            _clone = (string)FindResource("Clone");
            _create = (string)FindResource("Create");
            _add = (string)FindResource("Add");
        }

        private void OnAnyTextChanged(object sender, TextChangedEventArgs e)
        {
            _timer.Stop();
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _timer.IsEnabled = false;

            if (CheckLocalDirectoryIsGitRepo(LocalPathTextBox.Text.Trim()))
            {
                CloneButton.Content = _add;
                return;
            }

            if (!String.IsNullOrEmpty(UrlTextBox.Text.Trim()))
            {
                CloneButton.Content = _clone;
                return;
            }

            CloneButton.Content = _create;
        }

        private static bool CheckLocalDirectoryIsGitRepo(string path)
        {
            return false;
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
