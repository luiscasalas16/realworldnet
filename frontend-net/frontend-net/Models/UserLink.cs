namespace frontend_net.Models
{
    public class UserLink
    {
        public string Username { get; set; }

        public string FollowerUsername { get; set; }

        public User User { get; set; } = null!;
        public User FollowerUser { get; set; } = null!;
    }
}
