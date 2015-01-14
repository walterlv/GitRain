using System;
using System.Windows.Controls;

namespace Cvte.GitRain.UI
{
    public partial class MainFramePanel : UserControl
    {
        public MainFramePanel()
        {
            InitializeComponent();
        }

        private void RepoListPanel_Selected(object sender, EventArgs e)
        {
            GitGlobalPanel.Clear();
        }
    }
}
