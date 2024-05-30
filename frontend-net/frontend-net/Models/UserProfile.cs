namespace frontend_net.Models
{
    public class UserProfile
    {
        public Profile User { get; set; }
        public List<Article> Articles { get; set; }
        public List<Article> FavoriteArticles { get; set; }
        public bool IsCurrentUser { get; set; }
    }
}
