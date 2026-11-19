namespace Infectology.Library.DTOs.Responses.Users;

public record RegisterResponse(
    uint Id,
    string FirstName,
    string LastName,
    string Role
);
