using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackerModels
{
    public class TaskDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public Guid ProjectId { get; set; }


        [Required]
        public string Performer { get; set; }

        [Required]
        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public List<CommentShortDto> Comments { get; set; } = new List<CommentShortDto>();

    }
}
