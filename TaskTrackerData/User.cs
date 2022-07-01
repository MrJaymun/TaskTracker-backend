using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackerData
{
    public class User
    {
        [Key]
        public Guid Guid { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public List<Project> Projects { get; set; } = new List<Project>();
    }
}
