using System.Windows;
using System.Windows.Controls;

namespace Cvte.GitRain.UI
{
    public class MessageFrame : ContentControl
    {
        static MessageFrame()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MessageFrame),
                new FrameworkPropertyMetadata(typeof(MessageFrame)));
        }
    }
}
