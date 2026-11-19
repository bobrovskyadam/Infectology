namespace Infectology.Library.DTOs.Requests.Content;

public record ArticleUpdateRequest(
    string Heading,
    string? HeadingImage,
    string Markdown
);
