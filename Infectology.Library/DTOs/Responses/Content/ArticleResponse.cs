namespace Infectology.Library.DTOs.Responses.Content;

public record ArticleResponse(
    uint Id,
    string Heading,
    string? HeadingImage,
    DateTime DateTimeCreated,    
    DateTime? DateTimeEdited,
    string Markdown,
    string Author
);