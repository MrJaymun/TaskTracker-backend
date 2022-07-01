using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Requests.ProjectRequest
{
    public class DeleteProjectRequest
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
