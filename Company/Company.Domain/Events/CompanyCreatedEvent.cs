using MediatR;

namespace Company.Domain.Events;

public class CompanyCreatedEvent : INotification
{
    public Company Company { get; }

    public CompanyCreatedEvent(Company company)
    {
        Company = company;
    }
}