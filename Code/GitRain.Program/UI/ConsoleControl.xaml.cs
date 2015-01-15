using System.Windows.Controls;

namespace Cvte.GitRain.UI
{
    public partial class ConsoleControl : UserControl
    {
        public ConsoleControl()
        {
            InitializeComponent();
        }

        public string ConsoleText
        {
            get { return ConsoleTextBox.Text; }
            set { ConsoleTextBox.Text = value; }
        }
    }
}
