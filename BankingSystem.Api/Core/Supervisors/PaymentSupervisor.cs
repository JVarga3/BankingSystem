using BankingSystem.Api.Infrastructure.Repositories;
using BankingSystem.Api.Models;
using System.Net;
using System.Web.Http;

namespace BankingSystem.Api.Core.Supervisors;

public class PaymentSupervisor : IPaymentSupervisor
{
    private readonly IBankRepository _repository;
    private readonly ILogger<IPaymentSupervisor> _logger;

    public PaymentSupervisor(IBankRepository repository, ILogger<IPaymentSupervisor> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task Deposit(string userId, Payment request)
    {
        if (request.Amount > 10000m)
        {
            _logger.LogError("Deposit amount cannot be more than $10,000 in a single transaction.");
            throw new HttpResponseException(
               new HttpResponseMessage(HttpStatusCode.BadRequest)
               {
                   ReasonPhrase = "Deposit amount cannot be more than $10,000 in a single transaction."
               });
        }

        await _repository.Deposit(userId, request);
    }

    public async Task Withdraw(string userId, Payment request)
    {
        var account = await _repository.GetAccount(userId, request.AccountNumber);

        if (account.Balance - request.Amount < 100m)
        {
            _logger.LogError("Withdraw amount cannot leave a balance less than $100.");
            throw new HttpResponseException(
                new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    ReasonPhrase = "Withdraw amount cannot leave a balance less than $100."
                });
        }

        if (request.Amount > account.Balance * 0.9m)
        {
            _logger.LogError("Withdraw amount cannot be more than 90% of current balance.");
            throw new HttpResponseException(
                new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    ReasonPhrase = "Withdraw amount cannot be more than 90% of current balance."
                });
        }

        await _repository.Withdraw(userId, request);
    }
}