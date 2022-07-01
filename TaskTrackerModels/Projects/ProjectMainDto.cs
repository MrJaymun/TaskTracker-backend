using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackerModels
{
    public class ProjectMainDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public int allTasks{ get; set; }

        [Required]
        public int activeTasks { get; set; }

        [Required]
        public List<string> users { get; set; } = new List<string>();
    }
}
