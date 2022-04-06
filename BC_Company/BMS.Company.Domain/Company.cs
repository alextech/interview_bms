using System.Collections.Immutable;

namespace BMS.Company.Domain;

public class Company
{
    public Guid Guid { get; }

    public string Name { get; }

    private readonly IList<User> _users = new List<User>();

    public Company(Guid guid, string name)
    {
        Guid = guid;
        Name = name;
    }

    public void AddUser(User user)
    {
        _users.Add(user);
    }

    public IReadOnlyCollection<User> Users => _users.ToImmutableList();
}