using ConsumeSpotifywebApiUsingHttpClient.Models;
using ConsumeSpotifywebApiUsingHttpClient.services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumeSpotifywebApiUsingHttpClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISpotifyAccountservices _spotifyAccountservices;
        private readonly IConfiguration _configuration;
        private readonly ISpotifyService _spotifyService;

        public HomeController( 
            ISpotifyAccountservices spotifyAccountservices,
            IConfiguration configuration,
            ISpotifyService spotifyService)
        {
            _spotifyAccountservices = spotifyAccountservices;
            _configuration = configuration;
            _spotifyService = spotifyService;
        }

        public async Task<IActionResult> Index()
        {
            var newrelease = await ReleasesAsync();
            return View(newrelease);
        }
        private async Task<IEnumerable<Release>> ReleasesAsync()
        {
            try
            {
                var toten = await _spotifyAccountservices.GetToken(_configuration.GetSection("Spotify:ClientId").Value, _configuration.GetSection("Spotify:Client_Secret").Value);
                var newRelease = await _spotifyService.GetNewReleases("NG", 40, toten);
                return newRelease;
            }
            catch (Exception ex)
            {

                Debug.Write(ex);
                return Enumerable.Empty<Release>();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
