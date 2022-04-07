using Company.Api.Commands;
using FluentValidation;

namespace Company.Api.Validations;

public class RegisterUserWithCompanyValidator : AbstractValidator<RegisterUserWithCompany>
{
    public RegisterUserWithCompanyValidator()
    {
        RuleFor(register => register.CompanyName).NotEmpty()
            .WithMessage("Company name should not be empty.");
        RuleFor(register => register.UserEmail).NotEmpty()
            .WithMessage("User email should not be empty");
        RuleFor(register => register.UserPassword).NotEmpty()
            .WithMessage("Password should not be empty");
    }
}