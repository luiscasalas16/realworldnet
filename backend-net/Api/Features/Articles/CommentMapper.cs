using CommentEntity = RealWorldBackendNet.Core.Entities.Comment;
using CommentModel = RealWorldBackendNet.Api.Features.Articles.Comment;

namespace RealWorldBackendNet.Api.Features.Articles;

public static class CommentMapper
{
    public static CommentModel MapFromCommentEntity(CommentEntity commentEntity)
    {
        var author = new Author(
            commentEntity.Author.Username,
            commentEntity.Author.Image,
            commentEntity.Author.Bio,
            commentEntity.Author.Followers.Any());
        return new CommentModel(commentEntity.Id,
            commentEntity.CreatedAt,
            commentEntity.UpdatedAt,
            commentEntity.Body,
            author);
    }
}
