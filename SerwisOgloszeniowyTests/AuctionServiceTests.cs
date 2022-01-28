using FluentAssertions;
using SerwisOgloszeniowy.Models;
using SerwisOgloszeniowy.Models.AuctionModels;
using SerwisOgloszeniowy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SerwisOgloszeniowyTests
{
    public class AuctionServiceTests
    {
        AuctionService service = new AuctionService();

        [Theory]
        [InlineData("981")]
        [InlineData("532")]
        public async Task CheckPrice_ForValidData_ReturnsTrue(string price)
        {
            //ACT
            var response = service.CheckPrice(price);
            //ASSERT
            response.Should().BeTrue();
        }
        [Theory]
        [InlineData("86c")]
        [InlineData("97!")]
        public async Task CheckPrice_ForValidData_ReturnsFalse(string price)
        {
            //ACT
            var response = service.CheckPrice(price);
            //ASSERT
            response.Should().BeFalse();
        }
        [Theory]
        [InlineData("999111222")]
        [InlineData("673112321")]
        public async Task CheckPhonenumber_ForValidData_ReturnsTrue(string phonenumber)
        {
            //ACT
            var response = service.CheckPhoneNumber(phonenumber);
            //ASSERT
            response.Should().BeTrue();
        }
        [Theory]
        [InlineData("14291112225351")]
        [InlineData("171521")]
        public async Task CheckPhonenumber_ForValidData_ReturnsFalse(string phonenumber)
        {
            //ACT
            var response = service.CheckPhoneNumber(phonenumber);
            //ASSERT
            response.Should().BeFalse();
        }
        [Theory]
        [InlineData(new byte[] {1, 2, 3})]
        [InlineData(new byte[] { 1, 1, 5, 6, 7, 8 , 1, 2, 3, 5 })]
        public async Task CheckImage_ForValidData_ReturnsTrue(byte[] image)
        {
            //ACT
            var response = service.CheckImage(image);
            //ASSERT
            response.Should().BeTrue();
        }
    }
}
