using Microsoft.EntityFrameworkCore;
using URLShortner.Models;

namespace URLShortner.Data
{
    public class UrlShortenerContext : DbContext
    {
        public UrlShortenerContext(DbContextOptions<UrlShortenerContext> options) : base(options)
        {
        }

        public DbSet<UrlMapping> urlMappings { get; set; }
    }
}
