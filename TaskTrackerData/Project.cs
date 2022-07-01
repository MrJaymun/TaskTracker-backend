using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackerData
{
    public class Project
    {
        [Key]
        public Guid Guid { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }


        //Пришлось убрать User, потому что конфигуратор не понимает что происходит
        public string Creator { get; set; }
        public bool IsPersonal { get; set; }

        public List<User> Users { get; set; } = new List<User>();
        public List<Task> Tasks { get; set; } = new List<Task>();

    }
}
