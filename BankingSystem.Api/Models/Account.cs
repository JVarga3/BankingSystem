using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace BankingSystem.Api.Models;

public class Account
{
    [Required]
    public string AccountNumber { get; set; }
    [Range(typeof(decimal), "100.00", "10000.00", ErrorMessage = "The Balance must be greater than or equal to 100 and less than or equal to 10,000")]
    public decimal Balance { get; set; }
    public AccountType Type { get; set; }
}