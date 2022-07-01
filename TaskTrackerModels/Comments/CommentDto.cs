using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackerModels
{
    public class CommentDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public Guid TaskId { get; set; }

        [Required]
        public DateTime Date { get; set; }

    }
}
