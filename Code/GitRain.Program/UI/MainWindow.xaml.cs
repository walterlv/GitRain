using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Cvte.GitRain.Configs;
using Cvte.GitRain.Data;

namespace Cvte.GitRain.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RepoListPanel_Selected(object sender, EventArgs e)
        {
            GitGlobalPanel.Clear();
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

    internal class GitRepoToDetalConverter : IValueConverter
    {
        private static readonly Dictionary<GitRepoEntry, GitRepoDetailEntry> RepoDetailDictionary =
            new Dictionary<GitRepoEntry, GitRepoDetailEntry>();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is GitRepoDetailEntry)
            {
                return value;
            }
            GitRepoEntry entry = value as GitRepoEntry;
            if (entry == null)
            {
                return null;
            }
            if (RepoDetailDictionary.ContainsKey(entry))
            {
                return RepoDetailDictionary[entry];
            }
            else
            {
                GitRepoDetailEntry detailEntry = new GitRepoDetailEntry(entry);
                RepoDetailDictionary[entry] = detailEntry;
                return detailEntry;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
