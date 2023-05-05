using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Api.Models;
public class Payment
{
    [Required]
    public string AccountNumber { get; set; }
    public decimal Amount { get; set; }
}
