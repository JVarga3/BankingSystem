using BankingSystem.Api.Models;

namespace BankingSystem.Api.Core.Supervisors;

public interface IPaymentSupervisor
{
    Task Deposit(string userId, Payment request);
    Task Withdraw(string userId, Payment request);
}
