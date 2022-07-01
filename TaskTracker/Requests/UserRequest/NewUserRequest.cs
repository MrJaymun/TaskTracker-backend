using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Requests
{
    public class NewUserRequest
    {
        [Required]
        public string NewUserLogin { get; set; }
        [Required]
        public string NewUserPassword { get; set; }
       
    }
}
