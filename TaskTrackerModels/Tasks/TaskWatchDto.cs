using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TaskTrackerModels.Tasks
{
    public class TaskWatchDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Performer { get; set; }

        [Required]
        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public int CommentsCount { get; set; } 

    }
}
