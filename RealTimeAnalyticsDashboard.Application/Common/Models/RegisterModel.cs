using System.ComponentModel.DataAnnotations;

namespace RealTimeAnalyticsDashboard.Application.Common.Models;

public class RegisterModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required]
    public string FullName { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
