namespace Stas.Monitor.Infrastructures.PersonalExceptions;

public class DbConnectionException : Exception
{
    public DbConnectionException(string message) : base(message)
    {
    }
}
