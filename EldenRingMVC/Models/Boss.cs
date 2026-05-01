using System.ComponentModel.DataAnnotations;

namespace EldenRingMVC.Models
{
    public class Boss
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        /// <summary>Difficulty from 1 to 10</summary>
        public int Difficulty { get; set; }

        /// <summary>Imatge horitzontal — per a la llista de bosses</summary>
        public string ImageUrl { get; set; } = string.Empty;

        /// <summary>Imatge vertical — per a la pàgina de detall</summary>
        public string DetailImageUrl { get; set; } = string.Empty;

        public int Health { get; set; }

        public bool IsOptional { get; set; }

        public string Drops { get; set; } = string.Empty;

        public string LoreSnippet { get; set; } = string.Empty;
    }
}
