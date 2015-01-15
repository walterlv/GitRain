namespace Cvte.GitRain
{
    public class CommandResult
    {
        public bool Success
        {
            get { return Code == 0; }
        }

        public int Code { get; set; }
        public string OutputText { get; set; }
    }
}
