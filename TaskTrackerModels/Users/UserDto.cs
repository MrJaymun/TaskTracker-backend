using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackerModels
{
    public class UserDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }


        [Required]
        public List<CommentDto> MyComments { get; set; } = new List<CommentDto>();

        [Required]
        public List<ProjectDto> MyProjects { get; set; } = new List<ProjectDto>();

        [Required]
        public List<TaskDto> MyTasks { get; set; } = new List<TaskDto>();


    }
}
