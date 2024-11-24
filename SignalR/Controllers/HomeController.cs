using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalR.Helpers;
using SignalR.Hubs;
using SignalR.Models;

namespace SignalR.Controllers;

public class HomeController : Controller
{
  
        private readonly ILogger<HomeController> _logger;
        private readonly IHubContext<DeathlyHallowsHub> _deathlyHub;

        public HomeController(ILogger<HomeController> logger,
            IHubContext<DeathlyHallowsHub> deathlyHub)
        {
            _logger = logger;
            _deathlyHub = deathlyHub;   
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> DeathlyHallows([FromQuery] string type)
        {
            if (string.IsNullOrEmpty(type))
            {
                _logger.LogWarning("Type parameter is missing");
                return BadRequest("Type parameter is required.");
            }

            // Convert type to lowercase to make comparison case-insensitive
            type = type.ToLower();

            _logger.LogInformation($"DeathlyHallows method called with type: {type}");

            if (!SD.DealthyHallowRace.ContainsKey(type))
            {
                return BadRequest($"Invalid type: {type}");
            }

            // Increment the counter
            SD.DealthyHallowRace[type]++;

            // Broadcast the updated counts to all clients
            await _deathlyHub.Clients.All.SendAsync("updateDeathlyHallowCount",
                SD.DealthyHallowRace[SD.Cloak],
                SD.DealthyHallowRace[SD.Stone],
                SD.DealthyHallowRace[SD.Wand]);

            return Ok(SD.DealthyHallowRace);
        }

        public IActionResult Notification()
        {
            return View();
        }
        public IActionResult DeathlyHallowRace()
        {
            return View();
        }
        public IActionResult HarryPotterHouse()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
}