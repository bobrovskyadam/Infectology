namespace Infectology.Library.DTOs.Requests.Users;

public record LoginRequest(
    string Email,
    string Password
);
