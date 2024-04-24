using RealWorldBackendNet.Core.Dto;

namespace RealWorldBackendNet.Api.Features.Articles;

public interface IArticlesHandler
{
    public Task<Article> CreateArticleAsync(
        NewArticleDto newArticle, string username, CancellationToken cancellationToken);

    public Task<Article> UpdateArticleAsync(
        ArticleUpdateDto update, string slug, string username, CancellationToken cancellationToken);

    public Task DeleteArticleAsync(string slug, string username, CancellationToken cancellationToken);

    public Task<ArticlesResponseDto> GetArticlesAsync(ArticlesQuery query, string? username, bool isFeed,
        CancellationToken cancellationToken);

    public Task<Article> GetArticleBySlugAsync(string slug, string? username, CancellationToken cancellationToken);

    public Task<Core.Entities.Comment> AddCommentAsync(string slug, string username, CommentDto commentDto,
        CancellationToken cancellationToken);

    public Task RemoveCommentAsync(string slug, int commentId, string username,
        CancellationToken cancellationToken);

    public Task<List<Core.Entities.Comment>> GetCommentsAsync(string slug, string? username,
        CancellationToken cancellationToken);

    public Task<Article> AddFavoriteAsync(string slug, string username, CancellationToken cancellationToken);

    public Task<Article> DeleteFavorite(string slug, string username, CancellationToken cancellationToken);
}
