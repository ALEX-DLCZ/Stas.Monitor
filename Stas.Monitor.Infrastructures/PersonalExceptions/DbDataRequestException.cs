namespace Stas.Monitor.Infrastructures.PersonalExceptions;

public class DbDataRequestException : Exception
{
    public DbDataRequestException(string message) : base(message)
    {
    }
}
