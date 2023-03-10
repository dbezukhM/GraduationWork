namespace BLL.Models
{
    public class LoginModel : IDomainModel
    {
        public string? Email { get; set; }

        public string? Password { get; set; }
    }
}