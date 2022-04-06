using BMS.Company.Domain;
using MediatR;
using SharedKernel;

namespace BMS.Company.Cmd;

public class CreateCompanyWithUserCommand : ICommand<User>
{

    // these can be split into two separate DTO for company and user.
    public Guid CompanyId { get; }
    public string CompanyName { get; }
    public Guid UserId { get; }
    public string UserEmail { get; }
    public string UserPassword { get; }


    public CreateCompanyWithUserCommand(Guid companyId, string companyName, Guid userId, string userEmail, string userPassword)
    {
        CompanyId = companyId;
        CompanyName = companyName;
        UserId = userId;
        UserEmail = userEmail;
        UserPassword = userPassword;
    }

}