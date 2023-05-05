using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using BankingSystem.Api.Models;
using BankingSystem.Api.Core.Supervisors;
using BankingSystem.Api.Web.Controllers;

namespace BankingSystem.Tests.Web.Controllers;

public class AccountControllerTests
{
    private Mock<IAccountSupervisor> _mockSupervisor = new();

    [Fact]
    public async void GetAccounts200Success()
    {
        var accounts = new List<Account>()
        {
            new Account()
            {
                AccountNumber = "1",
                Balance = 100,
                Type = AccountType.Checkings
            },
            new Account()
            {
                AccountNumber = "2",
                Balance = 100,
                Type = AccountType.Checkings
            }
        };

        _mockSupervisor.Setup(x => x.GetAccounts(It.IsAny<string>())).ReturnsAsync(accounts);

        var controller = new AccountController(_mockSupervisor.Object);

        dynamic result = await controller.GetAccounts("1");

        Assert.IsType<OkObjectResult>(result);
        Assert.Equal(accounts, result.Value);
    }

    [Fact]
    public async void GetAccount200Success()
    {
        var account = new Account()
        {
            AccountNumber = "1",
            Balance = 100,
            Type = AccountType.Checkings
        };

        _mockSupervisor.Setup(x => x.GetAccount(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(account);

        var controller = new AccountController(_mockSupervisor.Object);

        dynamic result = await controller.GetAccount("1", "1");

        Assert.IsType<OkObjectResult>(result);
        Assert.Equal(account, result.Value);
    }

    [Fact]
    public async void PostAccount201Created()
    {
        var account = new Account()
        {
            AccountNumber = "1",
            Balance = 100,
            Type = AccountType.Checkings
        };

        _mockSupervisor.Setup(x => x.CreateAccount(It.IsAny<string>(), account)).ReturnsAsync(account);

        var controller = new AccountController(_mockSupervisor.Object);

        dynamic result = await controller.PostAccount("1", account);

        Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(account, result.Value);
    }

    [Fact]
    public async void DeleteAccount200Ok()
    {
        _mockSupervisor.Setup(x => x.DeleteAccount(It.IsAny<string>(), It.IsAny<string>()));

        var controller = new AccountController(_mockSupervisor.Object);

        var result = await controller.DeleteAccount("1", "1");

        Assert.IsType<NoContentResult>(result);
    }
}
