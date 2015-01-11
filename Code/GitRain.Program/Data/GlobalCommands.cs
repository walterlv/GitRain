namespace Cvte.GitRain.Data
{
    public static class GlobalCommands
    {
        public static readonly GlobalCommand GitCloneRepo = new GlobalCommand("GitCloneRepo");
        public static readonly GlobalCommand GitOpenOrCreateRepo = new GlobalCommand("GitOpenOrCreateRepo");
        public static readonly GlobalCommand GitConfig = new GlobalCommand("GitConfig");
    }
}
