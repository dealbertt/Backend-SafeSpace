using Backend_SafeSpace;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
namespace Backend_SafeSpace
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatRoomController : ControllerBase
    {
        private readonly MyAppDbContext _context;


        public ChatRoomController(MyAppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChatRoomDto>>> GetChatrooms()
        {
            var chatrooms = await _context.Chatrooms
                .Select(c => new ChatRoomDto
                {
                    ChatroomId = c.ChatroomId,
                    Name = c.Name,
                    LastMessage = c.Messages
                        .OrderByDescending(m => m.Timestamp)
                        .Select(m => m.Content)
                        .FirstOrDefault()
                })
                .ToListAsync();

            return Ok(chatrooms);
        }
    }
}
