namespace Infectology.Library.DTOs.Requests.Users;

public record ContactMessageRequest(
    string Name,
    string Email,
    string Message,
    bool GdprAccepted
);