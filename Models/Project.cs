using System.ComponentModel.DataAnnotations;

namespace BolascoProel4.Models
{
    public class Project
    {
        public int ProjectId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string? TechnologyUsed { get; set; }

        public string? ProjectUrl { get; set; }

        public byte[]? ImageData { get; set; }

        public string? ImageType { get; set; }

        public DateTime ReleasedDate { get; set; }
    }
}
