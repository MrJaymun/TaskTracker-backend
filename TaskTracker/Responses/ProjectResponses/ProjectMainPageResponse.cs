using System.ComponentModel.DataAnnotations;


namespace TaskTracker.Responses.ProjectResponses
{
    public class ProjectMainPageResponse
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public bool IsUserIn { get; set; }

        [Required]
        public bool IsPersonal { get; set; }

        [Required]
        public int usersCount { get; set; }

    }
}
