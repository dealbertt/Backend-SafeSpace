using Backend_SafeSpace;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

[ApiController]
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

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest(new { success = false, message = "Username and password are required." });
        }

        var user = await _context.users
            .Include(u => u.profile)
            .FirstOrDefaultAsync(u => u.Name == request.Username && u.Password == request.Password);

        if (user == null)
        {
            return Unauthorized(new { success = false, message = "Invalid username or password." });
        }

        return Ok(new
        {
            success = true,
            message = "Login successful!",
            user = new
            {
                user.Id,
                user.Name,
                user.Email,
                user.Password,
                profile = new
                {
                    user.profile.UserId,
                    user.profile.FullName,
                    user.profile.Bio,
                    user.profile.Age
                }
            }
        });
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateProfile([FromBody] ProfileUpdateDto updatedProfile)
    {
        if (updatedProfile == null || updatedProfile.UserId == 0)
        {
            return BadRequest(new { success = false, message = "Invalid user data." });
        }

        var existingProfile = await _context.profiles
            .FirstOrDefaultAsync(p => p.UserId == updatedProfile.UserId);

        if (existingProfile == null)
        {
            return NotFound(new { success = false, message = "User not found." });
        }

        existingProfile.FullName = updatedProfile.FullName;
        existingProfile.Bio = updatedProfile.Bio;
        existingProfile.Age = updatedProfile.Age;

        try
        {
            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "Profile updated successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { success = false, message = $"Error updating profile: {ex.Message}" });
        }
    }

}
