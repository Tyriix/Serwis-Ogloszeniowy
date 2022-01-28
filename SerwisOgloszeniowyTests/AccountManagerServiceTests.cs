using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using SerwisOgloszeniowy.Services;
using Xunit;

namespace SerwisOgloszeniowyTests
{
    
    public class AccountManagerServiceTests
    {
        AccountManagerService service = new AccountManagerService();

        [Theory]
        [InlineData("mail@mail.com")]
        [InlineData("tomek@grzanka.com")]
        public async Task CheckEmail_ForValidData_ReturnsTrue(string email)
        {
            //ACT
            var response = service.CheckMail(email);
            //ASSERT
            response.Should().BeTrue();
        }
        [Theory]
        [InlineData("test.com")]
        [InlineData("bartek.kowal")]
        public async Task CheckEmail_ForValidData_ReturnsFalse(string email)
        {
            //ACT
            var response = service.CheckMail(email);
            //ASSERT
            response.Should().BeFalse();
        }

        [Theory]
        [InlineData("Admin123!")]
        [InlineData("SilneH@slo1")]
        public async Task CheckPassword_ForValidData_returnsTrue(string password)
        {
            //ACT
            var response = service.CheckPassword(password);
            //ASSERT
            response.Should().BeTrue();
        }
        [Theory]
        [InlineData("Admin123")]
        [InlineData("silneh@slo1")]
        public async Task CheckPassword_ForValidData_returnsFalse(string password)
        {
            //ACT
            var response = service.CheckPassword(password);
            //ASSERT
            response.Should().BeFalse();
        }
    }
}
