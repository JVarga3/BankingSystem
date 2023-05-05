using BankingSystem.Api.Models;
using Xunit;
using BankingSystem.Api.Infrastructure.Repositories;

namespace BankingSystem.Tests.Infrastructure.Repositories;

public class BankRepositoryTests
{
    [Fact]
    public async void GetAccountsSuccess()
    {
        var user1Accounts = new List<Account>()
        {
            new Account() { AccountNumber = "1", Balance = 100m, Type = AccountType.Checkings },
            new Account() { AccountNumber = "2", Balance = 100m, Type = AccountType.Checkings }
        };
        var user2Accounts = new List<Account>()
        {
            new Account() { AccountNumber = "3", Balance = 100m, Type = AccountType.Checkings },
            new Account() { AccountNumber = "4", Balance = 100m, Type = AccountType.Checkings }
        };
        var mockDictionary = new Dictionary<string, List<Account>>();
        mockDictionary.Add("1", user1Accounts);
        mockDictionary.Add("2", user2Accounts);

        var repository = new BankRepository(mockDictionary);

        var result = await repository.GetAccounts("1");

        Assert.Equal(user1Accounts, result);
    }

    [Fact]
    public async void GetAccountSuccess()
    {
        var user1Accounts = new List<Account>()
        {
            new Account() { AccountNumber = "1", Balance = 100m, Type = AccountType.Checkings },
            new Account() { AccountNumber = "2", Balance = 100m, Type = AccountType.Checkings }
        };
        var user2Accounts = new List<Account>()
        {
            new Account() { AccountNumber = "3", Balance = 100m, Type = AccountType.Checkings },
            new Account() { AccountNumber = "4", Balance = 100m, Type = AccountType.Checkings }
        };
        var mockDictionary = new Dictionary<string, List<Account>>();
        mockDictionary.Add("1", user1Accounts);
        mockDictionary.Add("2", user2Accounts);

        var repository = new BankRepository(mockDictionary);

        var result = await repository.GetAccount("1", "2");

        Assert.Equal(user1Accounts.First(x => x.AccountNumber == "2"), result);
    }
}
