using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackerData
{
    public class Task
    {

        [Key]
        public Guid Guid { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Project Project { get; set; }

        public User Performer { get; set; }

        public DateTime BeginDate { get; set; }
        //Не обязательное поле
        public DateTime EndDate { get; set; }

        public TaskStatus Status { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
