using Xunit;
using Moq;
using BankingSystem.Api.Infrastructure.Repositories;
using BankingSystem.Api.Models;
using BankingSystem.Api.Core.Supervisors;
using System.Web.Http;
using Microsoft.Extensions.Logging;

namespace BankingSystem.Tests.Core.Supervisors;

public class PaymentSupervisorTests
{
    private Mock<IBankRepository> _mockRepository = new();
    private Mock<ILogger<IPaymentSupervisor>> _mockLogger = new();

    [Fact]
    public async void DepositReturnsExceptionOnDepositMoreThanLimit()
    {
        var supervisor = new PaymentSupervisor(_mockRepository.Object, _mockLogger.Object);

        var exception = await Assert.ThrowsAsync<HttpResponseException>(() =>
            supervisor.Deposit("1", new Payment() { AccountNumber = "1", Amount = 10000.01m }));

        Assert.Equal("Deposit amount cannot be more than $10,000 in a single transaction.", exception.Response.ReasonPhrase);
    }

    [Fact]
    public async void WithdrawReturnsExceptionOnAccountBalanceLimit()
    {
        var account = new Account()
        {
            AccountNumber = "1",
            Balance = 100m
        };

        _mockRepository.Setup(x => x.GetAccount(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(account);

        var supervisor = new PaymentSupervisor(_mockRepository.Object, _mockLogger.Object);

        var exception = await Assert.ThrowsAsync<HttpResponseException>(() =>
            supervisor.Withdraw("1", new Payment() { AccountNumber = "1", Amount = 100.01m }));

        Assert.Equal("Withdraw amount cannot leave a balance less than $100.", exception.Response.ReasonPhrase);
    }

    [Fact]
    public async void WithdrawReturnsExceptionOn90PercentTransactionLimit()
    {
        var account = new Account()
        {
            AccountNumber = "1",
            Balance = 10000m
        };

        _mockRepository.Setup(x => x.GetAccount(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(account);

        var supervisor = new PaymentSupervisor(_mockRepository.Object, _mockLogger.Object);

        var exception = await Assert.ThrowsAsync<HttpResponseException>(() =>
            supervisor.Withdraw("1", new Payment() { AccountNumber = "1", Amount = 9000.01m }));

        Assert.Equal("Withdraw amount cannot be more than 90% of current balance.", exception.Response.ReasonPhrase);
    }
}
