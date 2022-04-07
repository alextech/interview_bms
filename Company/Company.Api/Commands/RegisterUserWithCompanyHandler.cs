using Company.Domain;
using MediatR;

namespace Company.Api.Commands;

public class RegisterUserWithCompanyHandler : IRequestHandler<RegisterUserWithCompany, bool>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IUserRepository _userRepository;

    public RegisterUserWithCompanyHandler(ICompanyRepository companyRepository, IUserRepository userRepository)
    {
        _companyRepository = companyRepository;
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(RegisterUserWithCompany registerCommand, CancellationToken cancellationToken)
    {
        Domain.Company? company = await _companyRepository.FindByNameAsync(registerCommand.CompanyName, cancellationToken);

        if (company == null)
        {
            company = new Domain.Company(registerCommand.CompanyName);
            _companyRepository.Add(company);
            try
            {
                await _companyRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            }
            catch
            {
                // send to error logging
                return false;
            }
        }

        User user = new User(registerCommand.UserEmail, company);
        _userRepository.Add(user);

        try
        {
            await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
        catch
        {
            // send to error logging
            return false;
        }

        return true;
    }
}

public record UserDTO
{
    public string UserEmail { get; init; }
    public string Company { get; init; }

    public static UserDTO FromUser(User user)
    {
        return new UserDTO()
        {
            UserEmail = user.Email,
            Company = user.Company.Name
        };
    }
}
