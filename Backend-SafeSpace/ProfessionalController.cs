using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend_SafeSpace
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessionalController : ControllerBase
    {
        private readonly MyAppDbContext _context;

        public ProfessionalController(MyAppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Professional>>> GetAllProfessionals()
        {
            var professionals = await _context.Professionals.ToListAsync();
            return Ok(professionals);
        }
    }
}

