namespace Infectology.Library.DTOs.Responses.Users;

public record UserResponse(
    uint Id,
    string FirstName,
    string LastName,
    string Role,
    string Email,
    string? PhoneNumber,
    bool IsVerified,
    string DateTimeCreated    
);
