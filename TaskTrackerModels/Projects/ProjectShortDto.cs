using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackerModels
{
    public class ProjectShortDto
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

        public bool IsUserIn { get; set; }
        [Required]
        public bool IsPersonal { get; set; }

        [Required]
        public int UsersCount { get; set; }


    }
}
