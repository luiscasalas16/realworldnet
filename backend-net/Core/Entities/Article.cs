using RealWorldBackendNet.Core.Dto;
using RealWorldBackendNet.Infrastructure.Utils;

namespace RealWorldBackendNet.Core.Entities;

public class Article
{
    public Article(string title, string description, string body)
    {
        Slug = title.GenerateSlug();
        Title = title;
        Description = description;
        Body = body;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

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
    public ICollection<ArticleFavorite> ArticleFavorites { get; set; } = new List<ArticleFavorite>();

    public void UpdateArticle(ArticleUpdateDto update)
    {
        if (!string.IsNullOrWhiteSpace(update.Title))
        {
            Title = update.Title;
            Slug = update.Title.GenerateSlug();
        }

        if (!string.IsNullOrWhiteSpace(update.Body))
        {
            Body = update.Body;
        }

        if (!string.IsNullOrWhiteSpace(update.Description))
        {
            Description = update.Description;
        }

        UpdatedAt = DateTime.UtcNow;
    }
}
