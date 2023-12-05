namespace Stas.Monitor.Infrastructures.PersonalExceptions;

public class UnknownArgumentException : Exception
{
    public UnknownArgumentException(string message) : base(message)
    {
    }

}
