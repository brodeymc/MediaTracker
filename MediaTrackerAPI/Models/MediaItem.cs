using System.ComponentModel.DataAnnotations;

namespace MediaTrackerAPI.Models
{
    public class MediaItem
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string MediaType { get; set; } // Movie, Game, TV Show, Book, etc.
        public string Status { get; set; }
        public string Progress { get; set; }
        public string Notes { get; set; }
        public string ImageUrl { get; set; } // For custom image upload
        public DateTime lastUpdated { get; set; } = DateTime.UtcNow;

        public int UserId { get; set; }
        public User User { get; set; }

    }
}
