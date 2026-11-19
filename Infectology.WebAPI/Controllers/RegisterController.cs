using Infectology.Database.Contexts;
using Isopoh.Cryptography.Argon2;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infectology.WebAPI.Resources;
using Infectology.Library.Entities.Users;
using Infectology.Library.DTOs.Requests.Users;

namespace Infectology.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegisterController(UserContext context) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var emailExists = await context.Users.AnyAsync(u => u.PersonalContact.EmailAddress == request.Email);
        if (emailExists)
        {
            return Conflict(new { message = ApiMessages.EmailAlreadyExists });
        }

        var personalContact = new Contact
        {
            EmailAddress = request.Email,
            PhoneNumber = request.PhoneNumber,
            LandlineNumber = request.MobileNumber,
            Fax = request.Fax
        };

        var workplace = new Address
        {
            Street = request.WorkplaceAddress,
            City = request.WorkplaceCity,
            PostalCode = request.WorkplacePostalCode,
            Country = "Slovensko"
        };

        var workplaceContact = new Contact
        {
            EmailAddress = request.WorkplaceEmail ?? string.Empty,
            PhoneNumber = request.WorkplacePhone,
            Fax = request.WorkplaceFax
        };

        var residence = new Address
        {
            Street = request.PersonalAddress,
            City = request.PersonalCity,
            PostalCode = request.PersonalPostalCode,
            Country = "Slovensko"
        };

        var titles = new List<string>();
        if (!string.IsNullOrWhiteSpace(request.TitleBefore))
        {
            titles.Add(request.TitleBefore);
        }
        if (!string.IsNullOrWhiteSpace(request.TitleAfter))
        {
            titles.Add(request.TitleAfter);
        }

        var doctor = new Doctor
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            PasswordHash = Argon2.Hash(request.Password),
            Role = Role.Doctor,
            DateOfBirth = request.DateOfBirth,
            Titles = titles,
            GraduationYear = request.GraduationYear,
            WorkplaceFunction = request.WorkplaceFunction,
            Focus = request.Focus,
            Chamber = request.Chamber,
            ChamberId = request.ChamberId,
            PersonalContact = personalContact,
            WorkplaceContact = workplaceContact,
            Workplace = workplace,
            Residence = residence
        };

        context.Users.Add(doctor);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(Register), new { id = doctor.Id }, new
        {
            doctor.Id,
            doctor.FirstName,
            doctor.LastName,
            doctor.Role
        });
    }
}