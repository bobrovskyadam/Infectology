namespace Infectology.Library.DTOs.Responses.Users;

public record LoginResponse(
    string Token,
    string Email,
    string Role,
    string FirstName,
    string LastName
);
