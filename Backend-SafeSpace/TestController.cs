using Backend_SafeSpace;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class TestConnectionController : ControllerBase
{
    private readonly MyAppDbContext _context;
    private readonly ILogger<TestConnectionController> _logger;

    public TestConnectionController(MyAppDbContext context, ILogger<TestConnectionController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("check-connection")]
    public async Task<IActionResult> CheckConnection()
    {
        
            Console.WriteLine("Attempting to connect to database...");
            var canConnect = await _context.Database.CanConnectAsync();
        

            if (canConnect == false)
            {
                return StatusCode(500, new { success = false , body = "Hola buenas"});

            }
            return Ok(new { success = canConnect });

      
    }
}
