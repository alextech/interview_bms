using System.Collections.Immutable;

namespace BMS.Company.Domain;

public class Company : Entity
{
    public string Name { get; }

    private readonly IList<User> _users = new List<User>();

#pragma warning disable CS8618
    private Company() {}
#pragma warning restore CS8618

    public Company(Guid guid, string name)
    {
        Guid = guid;
        Name = name;
    }

    public void AddUser(User user)
    {
        _users.Add(user);
        user.Company = this;
    }

    public IReadOnlyCollection<User> Users => _users.ToImmutableList();
}