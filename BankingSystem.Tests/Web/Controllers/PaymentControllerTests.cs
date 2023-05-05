using Xunit;
using Moq;
using BankingSystem.Api.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using BankingSystem.Api.Core.Supervisors;
using BankingSystem.Api.Models;

namespace BankingSystem.Tests.Web.Controllers;

public class PaymentControllerTests
{
    private Mock<IPaymentSupervisor> _mockSupervisor = new();

    [Fact]
    public async void PostDeposit200Ok()
    {
        _mockSupervisor.Setup(x => x.Deposit(It.IsAny<string>(), It.IsAny<Payment>()));

        var controller = new PaymentController(_mockSupervisor.Object);

        dynamic result = await controller.Deposit("1", new Payment() { AccountNumber = "1"});

        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async void PostWithdraw200Ok()
    {
        _mockSupervisor.Setup(x => x.Withdraw(It.IsAny<string>(), It.IsAny<Payment>()));

        var controller = new PaymentController(_mockSupervisor.Object);

        dynamic result = await controller.Withdraw("1", new Payment() { AccountNumber = "1" });

        Assert.IsType<OkResult>(result);
    }
}
