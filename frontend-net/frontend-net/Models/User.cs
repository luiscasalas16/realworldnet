namespace frontend_net.Models
{
    public class User
    {
        public string Username { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string Password { get; set; } = default!;

        public string Bio { get; set; } = string.Empty;

        public string Image { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;

        public ICollection<ArticleFavorite>? ArticleFavorites { get; set; }

        public ICollection<Comment>? ArticleComments { get; set; }

        public ICollection<UserLink> Followers { get; set; } = new List<UserLink>();

        public ICollection<UserLink> FollowedUsers { get; set; } = new List<UserLink>();
    }
}
