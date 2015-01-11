using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Cvte.GitRain.UI
{
    public partial class GitGlobalPanel : UserControl
    {
        public GitGlobalPanel()
        {
            InitializeComponent();
        }

        private IEnumerable<RadioButton> FuncButtons
        {
            get { return FuncPanel.Children.OfType<RadioButton>(); }
        }

        public void Clear()
        {
            if (FuncButtons.Any(x => x.IsChecked == true))
            {
                FuncButtons.ForEach(x => x.IsChecked = false);
            }
        }

        private void OnCheckChanged(object sender, RoutedEventArgs e)
        {
            RadioButton button = FuncButtons.FirstOrDefault(x => x.IsChecked == true);
            if (button != null)
            {
                TabButton.Content = FindResource(((GlobalCommand)button.Command).Key);
                TabButton.IsChecked = true;
            }
            else
            {
                TabButton.Content = null;
                TabButton.IsChecked = false;
            }
        }

        public static readonly GlobalCommand EmptyCommand = new GlobalCommand("Empty");
    }
}
