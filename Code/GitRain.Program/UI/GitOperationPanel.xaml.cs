using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
                    FontWeight = FontWeights.Thin,
                    Opacity = 0.33,
                    TextTrimming = TextTrimming.CharacterEllipsis,
                    Foreground = (Brush)FindResource("Theme.Brush.Accent"),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                };
            }
        }

        private void GitFrame_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (GitFrame.CanGoBack)
            {
                GitFrame.RemoveBackEntry();
            }
        }
    }
}
