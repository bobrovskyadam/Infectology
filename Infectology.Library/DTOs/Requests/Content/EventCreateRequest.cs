namespace Infectology.Library.DTOs.Requests.Content;

public record EventCreateRequest(
    string Name,
    DateOnly Start,
    DateOnly? End,
    string? Place,
    string? Link
);
