namespace Infectology.Library.DTOs.Requests.Content;

public record EventUpdateRequest(
    uint Id,
    string Name,
    DateOnly Start,
    DateOnly? End,
    string? Place,
    string? Link
);
