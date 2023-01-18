using System.Threading.Tasks;

namespace ConsumeSpotifywebApiUsingHttpClient.services
{
    public interface ISpotifyAccountservices
    {
        Task<string> GetToken(string clientId, string clientSecret);
    }
}
