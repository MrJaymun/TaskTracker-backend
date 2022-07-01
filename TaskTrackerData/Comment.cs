using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackerData
{
    public class Comment
    {
        [Key]
        public Guid Guid { get; set; }

        public string Text { get; set; }

        public User Author{ get; set; }

        public Task Task { get; set; }

        public DateTime Date { get; set; }

    }
}
