﻿namespace Backend_SafeSpace.Migrations
{
    public class Message
    {
        public int MessageId { get; set; }

        // Foreign Keys
        public int ChatRoomId { get; set; }
        public int SenderId { get; set; }

        // Content
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }

        // Navigation properties
        public Chatroom ChatRoom { get; set; }
        public User Sender { get; set; }
    }
    public class MessageDto
    {
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public int SenderId { get; set; }
        public string SenderName { get; set; }
    }
}
