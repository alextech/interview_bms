using BMS.Company.Cmd;
using BMS.Company.Data;
using BMS.Company.Domain;
using MediatR;
using SharedKernel;

namespace Bms.Application;

public class CreateCompanyWithUserCommandHandler : IRequestHandler<CreateCompanyWithUserCommand, CommandResponse<User>>
{
    private readonly CompanyContext _companyContext;

    public CreateCompanyWithUserCommandHandler(CompanyContext companyContext)
    {
        _companyContext = companyContext;
    }

    public async Task<CommandResponse<User>> Handle(CreateCompanyWithUserCommand request, CancellationToken cancellationToken)
    {

        User user = new User(Guid.NewGuid());

        return CommandResponse<User>.Ok(user, "Successfully created user");
    }
}