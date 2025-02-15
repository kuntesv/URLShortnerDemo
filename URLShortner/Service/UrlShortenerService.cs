
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using URLShortner.Data;
using URLShortner.Models;

namespace URLShortner.Service
{
    public class UrlShortenerService : IUrlShortenerService
    {
        private UrlShortenerContext _context;
        private IConfiguration _configuration;
        private const string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private readonly Random _random = new();
        public UrlShortenerService(UrlShortenerContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string?> GetOriginalUrlAsync(string shortCode)
        {
            var baseUrl = _configuration.GetSection("AppSettings")["BaseUrl"];
            var shortenedUrl = $"{baseUrl}/{shortCode}";
            var mapping = await _context.urlMappings.FirstOrDefaultAsync(x => x.ShortenUrl == shortenedUrl);
            return mapping?.OrignalUrl;
        }

        public async Task<string> ShortenUrlAsync(string originalUrl)
        {
            if (Uri.IsWellFormedUriString(originalUrl, UriKind.Absolute))
            {
                throw new ArgumentException("Invalid URL format.", nameof(originalUrl));
            }

            var shortCode = GenerateShortCode();
            var baseUrl = _configuration.GetSection("AppSettings")["BaseUrl"];
            var shortnedUrl = $"{baseUrl}/{shortCode}";

            var mapping = new UrlMapping
            {
                OrignalUrl = originalUrl,
                ShortenUrl = shortnedUrl
            };

             _context.urlMappings.Add(mapping);
            await _context.SaveChangesAsync();

            return shortnedUrl;
        }

        private string GenerateShortCode(int length = 6)
        {

            return new string(Enumerable.Repeat(Alphabet, length).Select(s => s[_random.Next(s.Length)]).ToArray());
        }

    }
}
