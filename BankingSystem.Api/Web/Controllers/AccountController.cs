using BankingSystem.Api.Core.Supervisors;
using BankingSystem.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.Api.Web.Controllers;

[ApiController]
[Route("api/v1/[controller]/{userId}")]
public class AccountController : ControllerBase
{
    private readonly IAccountSupervisor _supervisor;

    public AccountController(IAccountSupervisor supervisor)
    {
        _supervisor = supervisor;
    }

    [HttpGet]
    public async Task<IActionResult> GetAccounts(string userId)
    {
        var response = await _supervisor.GetAccounts(userId);
        return Ok(response);
    }

    [HttpGet]
    [Route("{accountNumber}")]
    public async Task<IActionResult> GetAccount(string userId, string accountNumber)
    {
        var response = await _supervisor.GetAccount(userId, accountNumber);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> PostAccount(string userId, Account request)
    {
        var response = await _supervisor.CreateAccount(userId, request);
        return CreatedAtAction(nameof(GetAccount), new { userId = userId, accountNumber = response.AccountNumber }, response);
    }

    [Route("{accountNumber}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteAccount(string userId, string accountNumber)
    {
        await _supervisor.DeleteAccount(userId, accountNumber);
        return NoContent();
    }
}