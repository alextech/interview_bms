using BMS.Company.Cmd;
using BMS.Company.Data;
using BMS.Company.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
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
        // validation can go to FluentValidator and even be added to pipeline processing before getting here
        if (createCommand.CompanyId == Guid.Empty)
        {
            return CommandResponse<User>.Problem(null, $"Company has empty id.");
        }
        if (createCommand.UserId == Guid.Empty)
        {
            return CommandResponse<User>.Problem(null, $"User has empty id.");
        }

        bool companyExists = await (
            from c in _companyContext.Companies
                .TagWith("CreateCompanyCommand: Checking for existing company.")
            where c.Guid == createCommand.CompanyId
            select c
        ).AnyAsync(cancellationToken);

        if (companyExists)
        {
            return CommandResponse<User>.Problem(null,
                $"Company with id {createCommand.CompanyId} already exists."
            );
        }

        bool userExists = await (
            from u in _companyContext.Users
                .TagWith("CreateCompanyCommand: Checking for existing user.")
            where u.Guid == createCommand.UserId || u.Email == createCommand.UserEmail
            select u
        ).AnyAsync(cancellationToken);

        if (userExists)
        {
            return CommandResponse<User>.Problem(null,
                $"User with email {createCommand.UserEmail} already exists."
            );
        }

        Company newCompany = new Company(createCommand.CompanyId, createCommand.CompanyName);
        User newUser = new User(createCommand.UserId, createCommand.UserEmail, newCompany);
        _setPassword(newUser, createCommand);

        await _companyContext.AddRangeAsync(newCompany, newUser);
        await _companyContext.SaveChangesAsync(cancellationToken);

        return CommandResponse<User>.Ok(newUser, "Successfully created user");
    }

    private static void _setPassword(User user, CreateCompanyWithUserCommand createCommand)
    {
        PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
        user.Password = passwordHasher.HashPassword(user, createCommand.UserPassword);
    }
}