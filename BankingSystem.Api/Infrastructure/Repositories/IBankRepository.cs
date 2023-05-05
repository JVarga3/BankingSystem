using BankingSystem.Api.Models;

namespace BankingSystem.Api.Infrastructure.Repositories;

public interface IBankRepository
{
    Task<List<Account>> GetAccounts(string userId);
    Task<Account> GetAccount(string userId, string accountNumber);
    Task<Account> CreateAccount(string userId, Account type);
    Task DeleteAccount(string userId, string accountNumber);
    Task Deposit(string userId, Payment request);
    Task Withdraw(string userId, Payment request);
}