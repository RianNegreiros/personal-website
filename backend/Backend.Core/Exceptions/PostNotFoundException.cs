namespace Backend.Core.Exceptions;

public class PostNotFoundException(string message) : Exception(message)
{
}