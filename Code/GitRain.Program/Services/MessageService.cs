using System.Windows;

namespace Cvte.GitRain
{
    public class MessageService : IMessageService
    {
        private static IMessageService _current;

        public static IMessageService Current
        {
            get { return _current ?? (_current = new MessageService()); }
            set { _current = value; }
        }

        public void Show(MessageContent content)
        {
            MessageBox.Show(Application.Current.MainWindow, content.Content, content.Title);
        }
    }

    public interface IMessageService
    {
        void Show(MessageContent content);
    }

    public class MessageContent
    {
        public string Title { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }
    }
}
