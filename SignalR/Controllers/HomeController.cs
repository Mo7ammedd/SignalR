using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalR.Data;
using SignalR.Helpers;
using SignalR.Hubs;
using SignalR.Models;
using SignalR.Models.ViewModels;

namespace SignalR.Controllers;

public class HomeController : Controller
{
  
        private readonly ILogger<HomeController> _logger;
        private readonly IHubContext<DeathlyHallowsHub> _deathlyHub;
        private readonly IHubContext<OrderHub> _orderHub;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger,
            IHubContext<DeathlyHallowsHub> deathlyHub,
            IHubContext<OrderHub> orderHub,
            ApplicationDbContext context)
        {
            _logger = logger;
            _deathlyHub = deathlyHub;
            _orderHub = orderHub;
            _context = context;
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
        public IActionResult BasicChat()
        {
            return View();
        }
        public IActionResult HarryPotterHouse()
        {
            return View();
        }
        [Authorize]
        public IActionResult Chat()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ChatViewModel chatVm = new()
            {
                Rooms = _context.ChatRooms.ToList(),
                MaxRoomAllowed = 4,
                UserId = userId,
            };
            return View(chatVm); 
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [ActionName("Order")]
        public async Task<IActionResult> Order()
        {
            string[] name = { "Bhrugen", "Ben", "Jess", "Laura", "Ron" };
            string[] itemName = { "Food1", "Food2", "Food3", "Food4", "Food5" };

            Random rand = new Random();
            // Generate a random index less than the size of the array.  
            int index = rand.Next(name.Length);

            Order order = new Order()
            {
                Name = name[index],
                ItemName = itemName[index],
                Count = index
            };

            return View(order);
        }

        [ActionName("Order")]
        [HttpPost]
        public async Task<IActionResult> OrderPost(Order order)
        {

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            await _orderHub.Clients.All.SendAsync("newOrder");
            return RedirectToAction(nameof(Order));
        }
        [ActionName("OrderList")]
        public async Task<IActionResult> OrderList()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetAllOrder()
        {
            var productList = _context.Orders.ToList();
            return Json(new { data = productList });
        }

}