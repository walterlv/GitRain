using System.Windows;
using System.Windows.Controls;
using Cvte.GitRain.Data;

namespace Cvte.GitRain.UI
{
    public partial class GitOperationPanel : UserControl
    {
        public GitOperationPanel()
        {
            InitializeComponent();
            GlobalCommand.AnyExecuted += GlobalCommand_AnyExecuted;
        }

        private void GlobalCommand_AnyExecuted(object sender, GlobalCommandEventArgs e)
        {
            if (e.Key == GlobalCommands.GitCreateOrCloneRepo.Key)
            {
                GitFrame.Content = new CreateOrCloneRepoPage();
            }
            else if (e.Key == GlobalCommands.GitConfig.Key)
            {
                GitFrame.Content = new GitConfigPage();
            }
            else
            {
                GitFrame.Content = new TextBlock
                {
                    Text = ":( 努力开发中...",
                    FontSize = 48,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                };
            }
        }
    }
}
