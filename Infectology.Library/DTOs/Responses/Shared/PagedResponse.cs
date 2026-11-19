namespace Infectology.Library.DTOs.Responses.Shared;

public record PagedResponse<T>(
    IEnumerable<T> Items,
    int TotalCount
);
