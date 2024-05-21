namespace frontend_net.Models
{
    public class Article
    {
        public Guid Id { get; set; }

        public string Slug { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Body { get; set; }

        public User Author { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool Favorited { get; set; }

        public int FavoritesCount { get; set; } = 0;

        public ICollection<Tag> Tags { get; set; } = new List<Tag>();

        public List<Comment> Comments { get; set; } = new();
    }
}
