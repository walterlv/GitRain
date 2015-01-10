using System;
using System.Collections;
using System.ComponentModel;
using System.Windows;

namespace Cvte.GitRain.Designing
{
    public class Designer
    {
        public static readonly DependencyProperty DataContextProperty =
            DependencyProperty.RegisterAttached(
                "DataContext", typeof (object), typeof (Designer),
                new PropertyMetadata(default(object), DataContextChanged));

        public static void SetDataContext(DependencyObject element, ArrayList value)
        {
            element.SetValue(DataContextProperty, value);
        }

        public static ArrayList GetDataContext(DependencyObject element)
        {
            return (ArrayList) element.GetValue(DataContextProperty);
        }

        private static void DataContextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
#if DEBUG
            if (!DesignerProperties.GetIsInDesignMode(sender))
            {
                return;
            }
            FrameworkElement element = sender as FrameworkElement;
            if (element == null)
            {
                throw new ArgumentException("DataContext 必须附加到类型为 FrameworkElement 的元素上。");
            }
            element.DataContext = e.NewValue;
#endif
        }
    }
}
