namespace Backend.Core.Exceptions;

public class PostAlreadyExistsException(string message) : Exception(message)
{
}