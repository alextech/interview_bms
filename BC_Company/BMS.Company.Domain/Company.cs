namespace BMS.Company.Domain;

public class Company : Entity
{
    public string Name { get; private set; }

#pragma warning disable CS8618
    private Company() {}
#pragma warning restore CS8618

    public Company(Guid guid, string name)
    {
        Guid = guid;
        Name = name;
    }
}
