namespace frontend_net.Models
{
    public class ArticleFavorite
    {
        public string Username { get; set; }

        public Guid ArticleId { get; set; }

        public User User { get; set; } = null!;

        public Article Article { get; set; } = null!;
    }
}
