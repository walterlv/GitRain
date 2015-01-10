using System.ComponentModel;
using System.Runtime.CompilerServices;

// ReSharper disable ExplicitCallerInfoArgument

namespace Cvte.GitRain
{
    public class NotificationObject : INotifyPropertyChanged
    {
        /// <summary>
        /// 当某个属性的值改变时发生。
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 引发 <see cref="PropertyChanged"/> 事件。
        /// </summary>
        /// <param name="propertyName">指示引发属性改变事件的属性名。</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 设置属性 <paramref name="propertyName"/> 的值，并提供值变更通知。
        /// </summary>
        /// <typeparam name="T">类型参数将自动识别，是所设置属性的类型。</typeparam>
        /// <param name="storage">属性对应的私有字段，这个参数是引用的，用于比较和更改。</param>
        /// <param name="value">属性即将被修改的新值。</param>
        /// <param name="propertyName">属性名，如果未显式指定，将自动使用调用方法所在的属性中。</param>
        /// <returns>如果值被修改，则返回 true，否则返回 false。</returns>
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value)) return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
