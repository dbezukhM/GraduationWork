namespace WebApi.Models
{
    public class PersonChangePasswordRequest : IRequest
    {
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}