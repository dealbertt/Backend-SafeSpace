using Backend_SafeSpace.Migrations;

namespace Backend_SafeSpace
{
    public class Chatroom
    {
        public int ChatroomId { get; set; }  // Primary key
        public string Name { get; set; }     // Chatroom name
        public ICollection<Message> Messages { get; set; } = new List<Message>();
        public ICollection<UserChatroom> UserChatrooms { get; set; }
    }

    public class UserChatroom
    {
        public int UserId { get; set; } // Foreign key to User
        public User User { get; set; }

        public int ChatroomId { get; set; } // Foreign key to Chatroom
        public Chatroom Chatroom { get; set; }
    }
    public class ChatRoomDto
    {
        public int ChatroomId { get; set; }
        public string Name { get; set; }
        public string LastMessage { get; set; }
    }
}
