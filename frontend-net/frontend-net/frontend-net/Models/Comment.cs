namespace frontend_net.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public string Username { get; set; }
        public Guid ArticleId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public User Author { get; set; } = null!;
        public Article Article { get; set; } = null!;
    }
}
