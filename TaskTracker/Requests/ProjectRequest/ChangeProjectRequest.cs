using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Requests.ProjectRequest
{
    public class ChangeProjectRequest
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }

        [Required]
        public string newName { get; set; }

        [Required]
        public string newDesc { get; set; }

    }
}
