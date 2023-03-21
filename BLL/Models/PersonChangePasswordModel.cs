namespace BLL.Models
{
    public class PersonChangePasswordModel : IDomainModel
    {
        public string Email { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}