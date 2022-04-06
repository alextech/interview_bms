using System.Linq;
using NUnit.Framework;

namespace BMS.Company.Domain.Test;

public class CompanyTests
{

    [Test]
    public void CanAddUserToCompanyTest()
    {
        Company company = new Company( "test company");
        User user1 = new User("user_1@test.local", company);
        User user2 = new User("user_2@test.local", company);

        company.AddUser(user1);
        company.AddUser(user2);

        // not concerned with inefficiencies of creating new ROlist for every assertion in a simple test in favor of readability
        Assert.That(company.Users, Has.Exactly(2).Items);
        Assert.That(company.Users, Contains.Item(user1));
        Assert.That(company.Users, Contains.Item(user2));

        Assert.NotNull(user1);
        Assert.AreEqual(user1.Company, company);
    }

    [Test]
    public void CompanyCanCreateUserTest()
    {
        Company company = new Company( "test company");
        const string email = "new_user@test.local";

        company.AddUser(email);

        Assert.AreEqual(email, company.Users.First().Email);
    }
}