namespace Infectology.Library.DTOs.Responses.Content;

public record EventResponse(
    uint Id,
    string Name,
    DateOnly Start,     
    DateOnly? End,      
    string? Place,
    string? Link,
    uint AuthorId
);
