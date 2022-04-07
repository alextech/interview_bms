using MediatR;

namespace Company.Domain.Events;

public class UserAddedToCompanyEvent : INotification
{
    public Company Company { get; }
    public User User { get; }

    public UserAddedToCompanyEvent(Company company, User user)
    {
        Company = company;
        User = user;
    }
}