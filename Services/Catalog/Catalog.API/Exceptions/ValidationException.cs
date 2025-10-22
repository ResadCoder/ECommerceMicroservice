namespace Catalog.API.Exceptions;

public class ValidationException : Exception
{
    public ValidationException(List<string> errors) : base()
    {
        
    }
}