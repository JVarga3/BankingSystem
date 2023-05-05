using BankingSystem.Api.Models;

namespace BankingSystem.Api.Infrastructure.Repositories;

public class BankRepository : IBankRepository
{
    private readonly Dictionary<string, List<Account>> _userDictionary;

    public BankRepository(Dictionary<string, List<Account>> userDictionary)
    {
        _userDictionary = userDictionary;
    }

    public async Task<List<Account>> GetAccounts(string userId) =>
        _userDictionary[userId];

    public async Task<Account> GetAccount(string userId, string accountNumber)
    {
        var account = _userDictionary[userId].FirstOrDefault(x => x.AccountNumber == accountNumber);
        return account;
    }

    public async Task<Account> CreateAccount(string userId, Account request)
    {
        if (!_userDictionary.ContainsKey(userId))
            _userDictionary.Add(userId, new List<Account>());

        _userDictionary[userId].Add(request);
        return request;
    }

    public Task DeleteAccount(string userId, string accountNumber)
    {
        var account = _userDictionary[userId].FirstOrDefault(x => x.AccountNumber == accountNumber);
        _userDictionary[userId].Remove(account);

        return Task.CompletedTask;
    }

    public Task Withdraw(string userId, Payment request)
    {
        var userAccounts = _userDictionary[userId];
        var account = userAccounts.First(x => x.AccountNumber == request.AccountNumber);
        account.Balance -= request.Amount;

        return Task.CompletedTask;
    }

    public Task Deposit(string userId, Payment request)
    {
        var userAccounts = _userDictionary[userId];
        var account = userAccounts.First(x => x.AccountNumber == request.AccountNumber);
        account.Balance += request.Amount;

        return Task.CompletedTask;
    }
}
