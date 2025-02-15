using System.ComponentModel.DataAnnotations;

namespace URLShortner.Models
{
    public class UrlMapping
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string OrignalUrl { get; set; } = string.Empty;

        [Required]
        public string ShortenUrl { get; set; } = string.Empty;

    }
}
