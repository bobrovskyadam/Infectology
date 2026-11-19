namespace Infectology.Library.DTOs.Responses.Users;

public record UserProfileResponse(
    string FirstName,
    string LastName,
    string Email,
    string Workplace,
    string Specialization
);