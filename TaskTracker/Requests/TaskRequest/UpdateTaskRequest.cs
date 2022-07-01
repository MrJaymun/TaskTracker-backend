using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Requests.TaskRequest
{
    public class UpdateTaskRequest
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
        public Guid ProjectId { get; set; }

        [Required]
        public Guid TaskId { get; set; }


    }
}
