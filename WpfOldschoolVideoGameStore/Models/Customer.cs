namespace WpfOldschoolVideoGameStore.Models
{
    public class Customer : IUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAbove18 { get; set; }
    }
}
