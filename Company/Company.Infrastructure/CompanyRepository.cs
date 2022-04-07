using Company.Domain;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Company.Infrastructure;

public class CompanyRepository : ICompanyRepository
{
    private readonly CompanyContext _context;
    public IUnitOfWork UnitOfWork => _context;

    public CompanyRepository(CompanyContext companyContext)
    {
        _context = companyContext;
    }
    public Domain.Company Add(Domain.Company company)
    {
        if (company.IsTransient())
        {
            return _context.Companies
                .Add(company)
                .Entity;
        }

        return company;
    }

    public async Task<Domain.Company?> FindByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await (
            from c in _context.Companies
            where c.Name == name
            select c
        ).SingleOrDefaultAsync(cancellationToken);
    }
}