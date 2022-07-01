using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Requests.ProjectRequest
{
    public class NewProjectRequest
    {
        [Required]
        public string UserLogin { get; set; }
        [Required]
        public string UserPassword { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool IsPersonal { get; set; }
    }
}
