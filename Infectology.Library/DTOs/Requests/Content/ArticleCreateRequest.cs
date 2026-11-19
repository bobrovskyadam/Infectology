namespace Infectology.Library.DTOs.Requests.Content;

public record ArticleCreateRequest(
    uint AuthorId,
    string Heading,
    string? HeadingImage,
    string Markdown
);
