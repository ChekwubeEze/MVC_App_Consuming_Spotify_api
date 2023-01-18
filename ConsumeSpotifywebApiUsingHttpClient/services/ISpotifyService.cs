using ConsumeSpotifywebApiUsingHttpClient.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsumeSpotifywebApiUsingHttpClient.services
{
    public interface ISpotifyService
    {
        Task<IEnumerable<Release>> GetNewReleases(string countryCode, int limit, string accessToken);
    }
}
