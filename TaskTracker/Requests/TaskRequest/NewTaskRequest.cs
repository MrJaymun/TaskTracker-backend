using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Requests.TaskRequest
{
    public class NewTaskRequest
    {
        [Required]
        public string UserLogin { get; set; }
        [Required]
        public string UserPassword { get; set; }
        [Required]
        public string PerformerLogin { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Guid Project { get; set; }


    }
}
