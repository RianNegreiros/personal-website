namespace Backend.Core.Exceptions;

public class PostAlreadyExistsException : Exception
{
    public PostAlreadyExistsException(string message) : base(message)
    {
    }
}
