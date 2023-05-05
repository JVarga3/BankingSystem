using BankingSystem.Api.Models;

namespace BankingSystem.Api.Core.Supervisors;

public interface IAccountSupervisor
{
    Task<IEnumerable<Account>> GetAccounts(string userId);
    Task<Account> GetAccount(string userId, string accountNumber);
    Task<Account> CreateAccount(string userId, Account request);
    Task DeleteAccount(string userId, string accountNumber);    
}