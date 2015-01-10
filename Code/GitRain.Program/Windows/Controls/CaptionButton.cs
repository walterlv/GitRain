using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Cvte.GitRain.Windows.Controls
{
    public class CaptionButton : Button
    {
        static CaptionButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CaptionButton),
                new FrameworkPropertyMetadata(typeof(CaptionButton)));
        }

        public static readonly DependencyProperty WindowActionProperty = DependencyProperty.Register(
            "WindowAction", typeof (WindowAction), typeof (CaptionButton), new PropertyMetadata(default(WindowAction)));

        public WindowAction WindowAction
        {
            get { return (WindowAction) GetValue(WindowActionProperty); }
            set { SetValue(WindowActionProperty, value); }
        }

        private Window _owner;

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
#if DEBUG
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }
#endif
            _owner = Window.GetWindow(this);
            _owner.StateChanged += (sender, args) => ChangeVisibility();
            ChangeVisibility();
        }

        protected override void OnClick()
        {
            WindowAction.Invoke(this);
            base.OnClick();
        }

        private void ChangeVisibility()
        {
            switch (WindowAction)
            {
                case WindowAction.Maximize:
                    Visibility = _owner.WindowState != WindowState.Maximized ? Visibility.Visible : Visibility.Collapsed;
                    break;
                case WindowAction.Minimize:
                    Visibility = _owner.WindowState != WindowState.Minimized ? Visibility.Visible : Visibility.Collapsed;
                    break;
                case WindowAction.Normalize:
                    Visibility = _owner.WindowState != WindowState.Normal ? Visibility.Visible : Visibility.Collapsed;
                    break;
            }
        }
    }
}
