namespace Backend.Core.Exceptions;

public class AuthorizationException(string message) : Exception(message)
{
}