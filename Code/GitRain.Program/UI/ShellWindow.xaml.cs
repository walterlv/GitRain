using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Cvte.GitRain.Configs;

namespace Cvte.GitRain.UI
{
    public partial class ShellWindow : Window
    {
        public ShellWindow()
        {
            InitializeComponent();
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
    }
}
