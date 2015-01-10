using System.Windows;
using System.Windows.Controls;

namespace Cvte.GitRain.Windows.Controls
{
    public class SystemButtons : Control
    {
        static SystemButtons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SystemButtons),
                new FrameworkPropertyMetadata(typeof(SystemButtons)));
        }
    }
}
