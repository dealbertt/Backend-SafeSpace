namespace Backend_SafeSpace
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public DateTime createdAt {  get; set; }
        public Profile profile { get; set; } //
    }
}
