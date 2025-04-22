using Backend_SafeSpace;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Backend_SafeSpace
{

    [ApiController]
    [Route("api/[controller]")]

    public class AuthController : ControllerBase
    {
        private readonly MyAppDbContext _context;
      

        public AuthController(MyAppDbContext context)
        {
            _context = context;
            
        }

        public class RegisterRequest
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }

            var existingUser = await _context.users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (existingUser != null)
            {
                return Conflict(new { message = "email is already in use for another account" });
            }

            var newUser = new User
            {
                Name = request.UserName,
                Password = request.Password,
                Email = request.Email,
                createdAt = DateTime.UtcNow,
                profile = new Profile
                {
                    FullName = request.UserName,
                    Age = 0,
                    Bio = ""
                }
            };

            _context.users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(new {message = "User registered successfully"});
        }
    }
}
