namespace BMS.Company.Domain;

public class User
{
    public Guid Guid { get; }

    public string Email { get; set; }

    public User(Guid guid)
    {
        Guid = guid;
    }
}