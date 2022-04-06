using BMS.Company.Cmd;
using BMS.Company.Data;
using BMS.Company.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Bms.Application;

public class CreateCompanyWithUserCommandHandler : IRequestHandler<CreateCompanyWithUserCommand, CommandResponse<User>>
{
    private readonly CompanyContext _companyContext;

    public CreateCompanyWithUserCommandHandler(CompanyContext companyContext)
    {
        _companyContext = companyContext;
    }

    public async Task<CommandResponse<User>> Handle(CreateCompanyWithUserCommand createCommand, CancellationToken cancellationToken)
    {
        // Alternatively, could create CompanyQuery in BMS.Comapny.Cmd, make handle for it, inject MediatR service, and query against it.
        Company? company = await (
            from c in _companyContext.Companies
                .TagWith("CreateCompanyCommand: Fetch company to add user to, if it already exists")
            where c.Name == createCommand.CompanyName
            select c
        ).SingleOrDefaultAsync(cancellationToken);

        if (company == null)
        {
            company = new Company(createCommand.CompanyName);
            _companyContext.Add(company);
        }

        bool userExists = await (
            from u in _companyContext.Users
                .TagWith("CreateCompanyCommand: Check if email already exists.")
            where u.Email == createCommand.UserEmail
            select u
        ).AnyAsync(cancellationToken);

        if (userExists)
        {
            return CommandResponse<User>.Problem(null,
                $"User with email {createCommand.UserEmail} already exists."
            );
        }

        // using convenience method
        User newUser = company.AddUser(createCommand.UserEmail);

        // could have done User user = new User(createCommand.UserEmail, company); company.addUser(User);

        // hoping one day can tag insert statements too: https://github.com/dotnet/efcore/issues/14078
        await _companyContext.SaveChangesAsync(cancellationToken);

        return CommandResponse<User>.Ok(newUser, "Successfully created user");
    }
}