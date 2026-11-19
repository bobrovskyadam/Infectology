namespace Infectology.Library.DTOs.Requests.Users;

public record RegisterRequest(
    string FirstName,
    string LastName,
    DateOnly DateOfBirth,

    string? TitleBefore,
    string? TitleAfter,

    string Email,
    string? PhoneNumber,
    string? MobileNumber,

    string PersonalCity,
    string PersonalAddress,
    string PersonalPostalCode,
    string? Fax,

    string WorkplaceAddress,
    string WorkplacePostalCode,
    string WorkplaceCity,
    string? WorkplacePhone,
    string? WorkplaceEmail,
    string? WorkplaceFax,

    string WorkplaceFunction,
    string Focus,

    int GraduationYear,
    string Chamber,
    string ChamberId,

    string Password
);