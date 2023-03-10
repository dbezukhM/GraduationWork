using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class LoginRequest : IRequest
    {
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}