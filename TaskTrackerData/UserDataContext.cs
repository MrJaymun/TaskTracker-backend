using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackerData
{
    public class UserDataContext : DbContext
    {
        public UserDataContext(DbContextOptions<UserDataContext> options) :
            base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<TaskStatus> TaskStatuses { get; set; }

        
    }
}
