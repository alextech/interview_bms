using SharedKernel;

namespace Company.Domain;

public class Company : Entity, IAggregateRoot
{
    public string Name { get; private set; }

#pragma warning disable CS8618
    private Company() {}
#pragma warning restore CS8618

    public Company(string name)
    {
        Name = name;
    }
}