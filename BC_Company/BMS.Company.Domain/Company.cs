using System.Collections.Immutable;

namespace BMS.Company.Domain;

public class Company : Entity
{
    public string Name { get; private set; }

    private readonly IList<User> _users = new List<User>();

#pragma warning disable CS8618
    private Company() {}
#pragma warning restore CS8618

    public Company(string name)
    {
        Name = name;
    }

    public User AddUser(string email)
    {
        User user = new User(email, this);
        _users.Add(user);

        return user;
    }

    public void AddUser(User user)
    {
        if (!user.Company.Equals(this))
        {
            throw new Exception(
                "Adding user from wrong company. If this is a new user, recommended to use convenience factory method AddUser(string email)");
        }
        _users.Add(user);
    }

    public IReadOnlyCollection<User> Users => _users.ToImmutableList();
}