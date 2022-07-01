using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Requests.UserRequest
{
    public class NewPasswordRequest
    {
        [Required]
        public string userLogin { get; set; }
        [Required]
        public string userPassword { get; set; }
        [Required]
        public string userNewPassword { get; set; }
    }
}
