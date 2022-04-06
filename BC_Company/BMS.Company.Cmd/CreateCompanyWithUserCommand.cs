namespace BMS.Company.Cmd;

public class CreateCompanyWithUserCommand
{
    public string CompanyName { get; init; }
    public string UserEmail { get; init; }
    public string UserPassword { get; init; }
}