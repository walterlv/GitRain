using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Cvte.GitRain
{
    public class GlobalCommand : ICommand
    {
        private static readonly Dictionary<string, Action> CommandDictionary1 = new Dictionary<string, Action>();

        private static readonly Dictionary<string, Action<object>> CommandDictionary2 =
            new Dictionary<string, Action<object>>();

        public static void Register([NotNull] string key, [NotNull] Action action)
        {
            if (key == null) throw new ArgumentNullException("key");
            if (action == null) throw new ArgumentNullException("action");
            CommandDictionary1[key] += action;
        }

        public static void Register([NotNull] string key, [NotNull] Action<object> action)
        {
            if (key == null) throw new ArgumentNullException("key");
            if (action == null) throw new ArgumentNullException("action");
            CommandDictionary2[key] += action;
        }

        public static void Unregister([NotNull] string key, [NotNull] Action action)
        {
            if (key == null) throw new ArgumentNullException("key");
            if (action == null) throw new ArgumentNullException("action");
            Action localAction = CommandDictionary1[key];
            // ReSharper disable once DelegateSubtraction
            localAction -= action;
            if (localAction == null)
            {
                CommandDictionary1.Remove(key);
            }
        }

        public static void Unregister([NotNull] string key, [NotNull] Action<object> action)
        {
            if (key == null) throw new ArgumentNullException("key");
            if (action == null) throw new ArgumentNullException("action");
            Action<object> localAction = CommandDictionary2[key];
            // ReSharper disable once DelegateSubtraction
            localAction -= action;
            if (localAction == null)
            {
                CommandDictionary2.Remove(key);
            }
        }

        public string Key { get; private set; }

        public GlobalCommand([NotNull] string key)
        {
            if (key == null) throw new ArgumentNullException("key");
            Key = key;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            RaiseAnyExecuting(parameter);
            if (CommandDictionary1.ContainsKey(Key))
            {
                Action action = CommandDictionary1[Key];
                if (action != null)
                {
                    action();
                }
            }
            if (CommandDictionary2.ContainsKey(Key))
            {
                Action<object> action = CommandDictionary2[Key];
                if (action != null)
                {
                    action(parameter);
                }
            }
            RaiseAnyExecuted(parameter);
        }

        public static event EventHandler<GlobalCommandEventArgs> AnyExecuting;
        public static event EventHandler<GlobalCommandEventArgs> AnyExecuted;

        private void RaiseAnyExecuting(object parameter)
        {
            var handler = AnyExecuting;
            if (handler != null) handler(this, new GlobalCommandEventArgs(Key, parameter));
        }

        private void RaiseAnyExecuted(object parameter)
        {
            var handler = AnyExecuted;
            if (handler != null) handler(this, new GlobalCommandEventArgs(Key, parameter));
        }
    }

    public class GlobalCommandEventArgs : EventArgs
    {
        public string Key { get; private set; }
        public object Parameter { get; private set; }

        public GlobalCommandEventArgs(string key, object parameter)
        {
            Key = key;
            Parameter = parameter;
        }
    }
}
