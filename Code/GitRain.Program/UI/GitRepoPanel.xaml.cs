using System;
using System.Windows;
using System.Windows.Controls;

namespace Cvte.GitRain.UI
{
    public partial class GitRepoPanel : UserControl
    {
        public GitRepoPanel()
        {
            InitializeComponent();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            string pageTypeName = String.Format("{0}.Repo{1}Page",
                GetType().Namespace, (string) (((FrameworkElement) e.Source).Tag));
            Type pageType = Type.GetType(pageTypeName);
            Page page = (Page)Activator.CreateInstance(pageType);
            Frame.Content = page;
        }
    }
}
