namespace AdventureWorksProject.Utils
{
    public static class Logger
    {
        private static readonly string LogFile = "app.log";

        public static void Log(string message)
        {
            var logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";
            File.AppendAllText(LogFile, logEntry + Environment.NewLine);
        }

        public static void LogError(Exception ex)
        {
            var logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - ERROR: {ex.Message}\n{ex.StackTrace}";
            File.AppendAllText(LogFile, logEntry + Environment.NewLine);
        }
    }
}
