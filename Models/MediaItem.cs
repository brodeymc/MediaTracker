using System;

namespace MediaTrackerAPI.Models
{
    public class MediaItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string MediaType { get; set; } // Movie, Game, TV Show, Book, etc.
        public string Status { get; set; }
        public string Progress { get; set; }
        public string Notes { get; set; }
        public string ImageUrl { get; set; } // For custom image upload
        public DateTime lastUpdated { get; set; } = DateTime.UtcNow;

    }
}
