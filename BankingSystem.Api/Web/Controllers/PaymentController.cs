using BankingSystem.Api.Core.Supervisors;
using BankingSystem.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.Api.Web.Controllers;

[ApiController]
[Route("api/v1/[controller]/{userId}")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentSupervisor _paymentsSupervisor;

    public PaymentController(IPaymentSupervisor paymentsSupervisor)
    {
        _paymentsSupervisor = paymentsSupervisor;
    }

    [HttpPost]
    [Route("deposit")]
    public async Task<ActionResult> Deposit(string userId, Payment request)
    {
        await _paymentsSupervisor.Deposit(userId, request);
        return Ok();
    }

    [HttpPost]
    [Route("withdraw")]
    public async Task<ActionResult> Withdraw(string userId, Payment request)
    {
        await _paymentsSupervisor.Withdraw(userId, request);
        return Ok();
    }
}