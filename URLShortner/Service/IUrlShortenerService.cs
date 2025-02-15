namespace URLShortner.Service
{
    public interface IUrlShortenerService
    {
        Task<string> ShortenUrlAsync(string originalUrl);

        Task<string?> GetOriginalUrlAsync(string shortCode);

    }
}
