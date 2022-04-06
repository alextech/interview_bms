namespace BMS.Company.Domain;

public class User : Entity
{
    // allow to change email
    public string Email { get; set; }
    public string Password { get; set; }

    // Do not allow to change company object to avoid data inconsistency, since Company also has a list of these users.
    // Alternatively, could make a setter body that navigates to current company and removes the user from its internal list.
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
