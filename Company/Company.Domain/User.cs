using SharedKernel;

namespace Company.Domain;

/*
 * User is treated as aggregate root because in task
 * there was no requirement for Company to hold list of clients.
 * Therefore, company is unable to manage child users in its context
 * and users need their own independent DB context to be saved.
 */
public class User : Entity, IAggregateRoot
{
    public string Email { get; private set; }

    public string Password { get; set; }

    public Company Company { get; private set; }


#pragma warning disable CS8618
    private User() {}
#pragma warning restore CS8618

    public User(string email, Company company)
    {
        Email = email;
        Company = company;
    }
}