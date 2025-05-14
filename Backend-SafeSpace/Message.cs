namespace Backend_SafeSpace
{
    public class Message
    {
        public int MessageId { get; set; }
        public int ChatroomId { get; set; }
        public int SenderId { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public Chatroom Chatroom { get; set; }
        public User Sender { get; set; }
    }
    public class SendMessageDto
    {
        public int ChatroomId { get; set; }
        public int SenderId { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
