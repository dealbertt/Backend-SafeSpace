using Backend_SafeSpace;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly MyAppDbContext _context;

    public ChatController(MyAppDbContext context)
    {
        _context = context;
    }

    [HttpGet("{chatroomId}/messages")]
    public async Task<IActionResult> GetMessages(int chatroomId)
    {
        var messages = await _context.Messages
            .Where(m => m.ChatroomId == chatroomId)
            .OrderBy(m => m.Timestamp)
            .Select(m => new
            {
                m.MessageId,
                m.SenderId,
                SenderName = m.Sender.Name,
                m.Content,
                m.Timestamp
            })
            .ToListAsync();

        if (!messages.Any())
        {
            return NotFound(new { success = false, message = "No messages found for this chatroom." });
        }

        return Ok(new { success = true, messages });
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendMessage([FromBody] SendMessageDto messageDto)
    {
        if (messageDto == null)
        {
            return BadRequest(new { success = false, message = "Invalid message data." });
        }

        var chatroom = await _context.Chatrooms.FindAsync(messageDto.ChatroomId);
        if (chatroom == null)
        {
            return NotFound(new { success = false, message = "Chatroom not found." });
        }

        var user = await _context.users.FindAsync(messageDto.SenderId);
        if (user == null)
        {
            return NotFound(new { success = false, message = "User not found." });
        }

        var message = new Message
        {
            ChatroomId = messageDto.ChatroomId,
            SenderId = messageDto.SenderId,
            Content = messageDto.Content,
            Timestamp = messageDto.Timestamp
        };

        // Add the message to the database
        _context.Messages.Add(message);

        try
        {
            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "Message sent successfully!" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { success = false, message = $"Error sending message: {ex.Message}" });
        }
    }
}
