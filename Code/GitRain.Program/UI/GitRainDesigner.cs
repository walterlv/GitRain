using System.ComponentModel;
using System.Windows;
using Cvte.GitRain.Configs;

namespace Cvte.GitRain.UI
{
    internal static class GitRainDesigner
    {
        public static readonly DependencyProperty EnableConfigProperty = DependencyProperty.RegisterAttached(
            "EnableConfig", typeof (bool), typeof (GitRainDesigner),
            new PropertyMetadata(default(bool), EnableConfigPropertyChangedCallback));

        public static void SetEnableConfig(DependencyObject element, bool value)
        {
            element.SetValue(EnableConfigProperty, value);
        }

        public static bool GetEnableConfig(DependencyObject element)
        {
            return (bool) element.GetValue(EnableConfigProperty);
        }

        private static void EnableConfigPropertyChangedCallback(DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            if (!DesignerProperties.GetIsInDesignMode(sender))
            {
                return;
            }
            if (Equals(e.NewValue, true))
            {
                UserConfig.Instance.Load();
            }
        }
    }
}
