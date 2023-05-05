using BankingSystem.Api.Infrastructure.Repositories;
using BankingSystem.Api.Models;

namespace BankingSystem.Api.Core.Supervisors;

public class AccountSupervisor : IAccountSupervisor
{
    private readonly IBankRepository _repository;

    public AccountSupervisor(IBankRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Account>> GetAccounts(string userId) =>
        await _repository.GetAccounts(userId);

    public async Task<Account> GetAccount(string userId, string accountNumber) =>
        await _repository.GetAccount(userId, accountNumber);

    public async Task<Account> CreateAccount(string userId, Account request) =>
        await _repository.CreateAccount(userId, request);
    
    public Task DeleteAccount(string userId, string accountNumber) => 
        _repository.DeleteAccount(userId, accountNumber);
}