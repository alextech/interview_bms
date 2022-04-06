using System;
using NUnit.Framework;

namespace BMS.Company.Domain.Test;

public class CompanyTests
{

    [Test]
    public void CanAddUserToCompanyTest()
    {
        Company company = new Company(Guid.NewGuid(), "test company");
        User user1 = new User(Guid.NewGuid())
        {
            Email = "user_1@test.local"
        };
        User user2 = new User(Guid.NewGuid())
        {
            Email = "user_2@test.local"
        };

        company.AddUser(user1);
        company.AddUser(user2);

        // not concerned with inefficiencies of creating new ROlist for every assertion in a simple test in favor of readability
        Assert.That(company.Users, Has.Exactly(2).Items);
        Assert.That(company.Users, Contains.Item(user1));
        Assert.That(company.Users, Contains.Item(user2));

        Assert.NotNull(user1.Company);
        Assert.AreEqual(user1.Company, company);
    }
}