using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Cvte.GitRain.UI
{
    public partial class RepoHomePage : Page
    {
        public RepoHomePage()
        {
            InitializeComponent();
        }

        private void ConsoleTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            CommandExecutor.ConsoleOutput += OnConsoleOutput;
        }

        private void OnConsoleOutput(object sender, DataReceivedEventArgs e)
        {
            ConsoleTextBox.AppendText(e.Data);
        }
    }
}
