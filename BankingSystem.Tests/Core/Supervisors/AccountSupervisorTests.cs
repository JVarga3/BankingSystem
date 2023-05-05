using Xunit;
using Moq;
using BankingSystem.Api.Infrastructure.Repositories;
using BankingSystem.Api.Models;
using BankingSystem.Api.Core.Supervisors;

namespace BankingSystem.Tests.Core.Supervisors;

public class AccountSupervisorTests
{
    private Mock<IBankRepository> _mockRepository = new();

    [Fact]
    public async void GetAccountsReturnsSuccess()
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

        _mockRepository.Setup(x => x.GetAccounts(It.IsAny<string>())).ReturnsAsync(accounts);

        var supervisor = new AccountSupervisor(_mockRepository.Object);

        var result = await supervisor.GetAccounts("1");

        Assert.Equal(accounts, result);
    }

    [Fact]
    public async void GetAccountReturnsSuccess()
    {
        var account = new Account()
        {
            AccountNumber = "1",
            Balance = 100,
            Type = AccountType.Checkings
        };

        _mockRepository.Setup(x => x.GetAccount(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(account);

        var supervisor = new AccountSupervisor(_mockRepository.Object);

        var result = await supervisor.GetAccount("1", "1");

        Assert.Equal(account, result);
    }

    [Fact]
    public async void PostAccountReturnsSuccess()
    {
        var account = new Account()
        {
            AccountNumber = "1",
            Balance = 100,
            Type = AccountType.Checkings
        };

        _mockRepository.Setup(x => x.CreateAccount(It.IsAny<string>(), It.IsAny<Account>())).ReturnsAsync(account);

        var supervisor = new AccountSupervisor(_mockRepository.Object);

        var result = await supervisor.CreateAccount("1", account);

        Assert.Equal(account, result);
    }
}
