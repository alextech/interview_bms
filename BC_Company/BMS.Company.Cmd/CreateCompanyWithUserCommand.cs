using BMS.Company.Domain;
using MediatR;
using SharedKernel;

namespace BMS.Company.Cmd;

public class CreateCompanyWithUserCommand : ICommand<User>
{

    public string CompanyName { get; }
    public string UserEmail { get; }
    public string UserPassword { get; }

    public CreateCompanyWithUserCommand(string companyName, string userEmail, string userPassword)
    {
        CompanyName = companyName;
        UserEmail = userEmail;
        UserPassword = userPassword;
    }


}