using System.Runtime.Serialization;
using MediatR;

namespace Company.Api.Commands;

public class RegisterUserWithCompany : IRequest<bool>
{
    [DataMember]
    public string UserEmail { get; private set; }

    [DataMember]
    public string UserPassword { get; private set; }

    [DataMember]
    public string CompanyName { get; private set; }

    public RegisterUserWithCompany(string userEmail, string userPassword, string companyName)
    {
        UserEmail = userEmail;
        UserPassword = userPassword;
        CompanyName = companyName;
    }
}