namespace frontend_net.Models
{
    public class UserProfile
    {
        public User User { get; set; }
        public List<Article> Articles { get; set; }
        public bool IsCurrentUser { get; set; }
    }
}
