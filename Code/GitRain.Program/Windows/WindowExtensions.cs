using System;
using System.Windows;

namespace Cvte.GitRain.Windows
{
    public static class WindowExtensions
    {
        public static void Invoke(this WindowAction action, [NotNull] FrameworkElement source)
        {
            if (source == null) throw new ArgumentNullException("source");
            var window = Window.GetWindow(source);
            if (window == null) return;

            switch (action)
            {
                case WindowAction.Active:
                    window.Activate();
                    break;
                case WindowAction.Close:
                    window.Close();
                    break;
                case WindowAction.Maximize:
                    window.WindowState = WindowState.Maximized;
                    break;
                case WindowAction.Minimize:
                    window.WindowState = WindowState.Minimized;
                    break;
                case WindowAction.Normalize:
                    window.WindowState = WindowState.Normal;
                    break;
                case WindowAction.OpenSystemMenu:
                    var point = source.PointToScreen(new Point(0, source.ActualHeight));
                    SystemCommands.ShowSystemMenu(window, point);
                    break;
            }
        }
    }
}
