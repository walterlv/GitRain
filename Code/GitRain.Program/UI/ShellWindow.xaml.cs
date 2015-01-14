using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Effects;
using Cvte.GitRain.Configs;

namespace Cvte.GitRain.UI
{
    public partial class ShellWindow : Window
    {
        public ShellWindow()
        {
            InitializeComponent();
        }

        private bool IsMessageDisplaying
        {
            get { return MainFrame.Effect != null; }
            set
            {
                if (value)
                {
                    MainFrame.Effect = (Effect) FindResource("BlurEffect");
                    MessageFrame.Visibility = Visibility.Visible;
                }
                else
                {
                    MainFrame.Effect = null;
                    MessageFrame.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            UserConfig.Instance.Load();
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            Hide();
            UserConfig.Instance.Save();
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            IsMessageDisplaying = !IsMessageDisplaying;
        }
    }
}
