using System.ComponentModel.DataAnnotations;

namespace EldenRingMVC.Models
{
    public class LoreEntry
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string Era { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty; // "Age", "Faction", "Character"

        public int Order { get; set; }
    }
}
