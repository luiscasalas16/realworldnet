namespace frontend_net.Models
{
    public class Tag
    {
        public string Id { get; set; }

        public ICollection<Article> Articles { get; set; } = null!;
    }
}
