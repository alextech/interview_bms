namespace Company.Domain.Exceptions;

public class CompanyDomainException : Exception
{
    public CompanyDomainException() { }

    public CompanyDomainException(string message) : base(message)
    {

    }

    public CompanyDomainException(string message, Exception innerException)
        : base(message, innerException)
    {

    }
}