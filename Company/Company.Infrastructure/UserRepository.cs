using Company.Domain;
using SharedKernel;

namespace Company.Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly CompanyContext _context;

    public IUnitOfWork UnitOfWork => _context;
    public UserRepository(CompanyContext companyContext)
    {
        _context = companyContext;
    }

    public User Add(User user)
    {
        if (user.IsTransient())
        {
            return _context.Users
                .Add(user)
                .Entity;
        }

        return user;
    }
}