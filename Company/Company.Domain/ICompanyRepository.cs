using SharedKernel;

namespace Company.Domain;

public interface ICompanyRepository : IRepository<Company>
{
    public Company Add(Company company);
    Task<Company?> FindByNameAsync(string name, CancellationToken cancellationToken = default);
}