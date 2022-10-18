namespace InterviewTask.Services
{
    public interface ILoggingService
    {
        void LogError(string message);
        void LogInformation(string message);
        void LogWarning(string message);
    }
}