using NUnit.Framework;

namespace Company.Domain.Test;

public class Tests
{
    [Test]
    public void CanAddUserToCompanyTest()
    {
        Company company = new Company( "test company");
        User user = new User("user_1@test.local", company);

        Assert.NotNull(user.Company);
        Assert.AreEqual(user.Company, company);
    }
}