using System;
using System.Windows.Controls;

namespace Cvte.GitRain.UI
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void RepoListPanel_Selected(object sender, EventArgs e)
        {
            GitGlobalPanel.Clear();
        }
    }
}
