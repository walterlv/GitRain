using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Cvte.GitRain.Windows
{
    /// <summary>
    /// <see cref="T:Object"/> 到 <see cref="T:Visibility"/> 的通用转换器。
    /// </summary>
    public class VisibilityConverter : IValueConverter
    {
        public static readonly VisibilityConverter BooleanToVisibility = new VisibilityConverter
        {
            Visible = true,
            Collapsed = false
        };

        /// <summary>
        /// 构造一个 <see cref="T:Object"/> 到 <see cref="T:Visibility"/> 的通用转换器对象。
        /// </summary>
        public VisibilityConverter()
        {
            DefaultVisibility = Visibility.Collapsed;
        }

        /// <summary>
        /// 获取或设置 <see cref="P:Visibility.Visible"/> 所对应的值。
        /// </summary>
        public object Visible { get; set; }

        /// <summary>
        /// 获取或设置 <see cref="P:Visibility.Hidden"/> 所对应的值。
        /// </summary>
        public object Hidden { get; set; }

        /// <summary>
        /// 获取或设置 <see cref="P:Visibility.Collapsed"/> 所对应的值。
        /// </summary>
        public object Collapsed { get; set; }

        /// <summary>
        /// 设置默认的 <see cref="T:Visibility"/> 值。
        /// </summary>
        public Visibility DefaultVisibility { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Equals(value, Visible))
            {
                return Visibility.Visible;
            }
            if (Equals(value, Hidden))
            {
                return Visibility.Hidden;
            }
            if (Equals(value, Collapsed))
            {
                return Visibility.Collapsed;
            }
            return DefaultVisibility;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;
            switch (visibility)
            {
                case Visibility.Visible:
                    return Visible;
                case Visibility.Hidden:
                    return Hidden;
                case Visibility.Collapsed:
                    return Collapsed;
                default:
                    throw new ArgumentException(String.Format("不支持指定值：{0}。", value));
            }
        }
    }
}
