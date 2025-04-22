using Backend_SafeSpace;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Backend_SafeSpace_Guest
{

   
    [Route("api/[controller]")]

    public class AuthController : ControllerBase
    {
        private readonly MyAppDbContext _context;
        private readonly ILogger<AuthController> _logger;

        public AuthController(MyAppDbContext context, ILogger<AuthController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public class GuestLoginRequest
        {
            public string UserName { get; set; }
            public string? Password { get; set; }
            public string? Email { get; set; }

        }

        [HttpPost("guest-login")]
        public async Task<IActionResult> GuestLogin()
        {
            var random = new Random();
            var guestUserName = $"Guest{random.Next(1000, 9999)}";

            var user = new User
            {
                Name = guestUserName,
                Email = null,
                Password = null,
                createdAt = DateTime.UtcNow
            };

            _context.users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Guest account created",
                guestName = user.Name,
                userId = user.Id
            });
        }

    }
}
