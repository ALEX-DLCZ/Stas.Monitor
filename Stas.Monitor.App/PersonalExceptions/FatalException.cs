using System;

namespace Stas.Monitor.App.PersonalExceptions;

public class FatalException : Exception
{
    public FatalException(string message) : base(message)
    {
    }

    public FatalException(string message, Exception innerException) : base(message, innerException)
    {
    }

}
