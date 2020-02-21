
namespace L14HW
{
    public interface ILogWriter
    {
        void LogInfo(string message);
        void LogWarning(string message);
        void LogError(string message);
    }
}
