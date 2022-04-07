using SharedKernel;

namespace Company.Domain;

public interface IUserRepository : IRepository<User>
{
    public User Add(User user);
}