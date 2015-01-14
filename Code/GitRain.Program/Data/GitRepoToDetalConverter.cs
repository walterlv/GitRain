using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace Cvte.GitRain.Data
{
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
