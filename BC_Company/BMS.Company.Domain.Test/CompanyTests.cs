using System;
using NUnit.Framework;

namespace BMS.Company.Domain.Test;

public class CompanyTests
{

    [Test]
    public void CanAddUserToCompanyTest()
    {
        Company company = new Company( Guid.NewGuid(),"test company");
        User user1 = new User(Guid.NewGuid(), "user_1@test.local", company);

        Assert.NotNull(user1.Company);
        Assert.AreEqual(user1.Company, company);
    }
}
