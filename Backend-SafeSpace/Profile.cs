namespace Backend_SafeSpace
{
    public class Profile
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Bio {  get; set; }

        public User user { get; set; }
    }

    public class ProfileUpdateDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Bio { get; set; }
    }

}
