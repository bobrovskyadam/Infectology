using Infectology.Database.Contexts;
using Infectology.Library.DTOs.Requests.Users;
using Infectology.Library.Entities.Users;
using Infectology.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Infectology.Tests;

[TestClass]
public sealed class RegisterControllerTests
{
    private UserContext _context = null!;
    private RegisterController _controller = null!;

    [TestInitialize]
    public void TestInit()
    {
        var options = new DbContextOptionsBuilder<UserContext>()
            .UseInMemoryDatabase("TestDb_Register")
            .Options;

        _context = new UserContext(options);
        _controller = new RegisterController(_context);
    }

    [TestCleanup]
    public void TestCleanup()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [TestMethod]
    public async Task Register_DuplicateEmail_ReturnsConflict()
    {
        var existingUser = new Doctor
        {
            FirstName = "Jan",
            LastName = "Novak",
            PasswordHash = "hash",
            Role = Role.Doctor,
            DateOfBirth = new DateOnly(1990, 1, 1),
            GraduationYear = 2015,
            WorkplaceFunction = "Lekar",
            Focus = "Infektologia",
            Chamber = "SLK",
            ChamberId = "12345",
            PersonalContact = new Contact
            {
                EmailAddress = "test@test.sk"
            }
        };

        _context.Users.Add(existingUser);
        await _context.SaveChangesAsync();

        var request = new RegisterRequest(
            FirstName: "Peter",
            LastName: "Test",
            DateOfBirth: new DateOnly(1990, 1, 1),

            TitleBefore: null,
            TitleAfter: null,

            Email: "test@test.sk",
            PhoneNumber: null,
            MobileNumber: null,

            PersonalCity: "Kosice",
            PersonalAddress: "Hlavna 1",
            PersonalPostalCode: "04001",
            Fax: null,

            WorkplaceAddress: "Nemocnicna 5",
            WorkplacePostalCode: "04001",
            WorkplaceCity: "Kosice",
            WorkplacePhone: null,
            WorkplaceEmail: null,
            WorkplaceFax: null,

            WorkplaceFunction: "Lekar",
            Focus: "Infektologia",

            GraduationYear: 2015,
            Chamber: "SLK",
            ChamberId: "12345",

            Password: "Heslo123!"
        );

        var result = await _controller.Register(request);

        Assert.IsInstanceOfType(
            result,
            typeof(ConflictObjectResult));
    }

    [TestMethod]
    public async Task Register_NewEmail_ReturnsCreated()
    {
        var request = new RegisterRequest(
            FirstName: "Peter",
            LastName: "Test",
            DateOfBirth: new DateOnly(1990, 1, 1),

            TitleBefore: null,
            TitleAfter: null,

            Email: "new@test.sk",
            PhoneNumber: null,
            MobileNumber: null,

            PersonalCity: "Kosice",
            PersonalAddress: "Hlavna 1",
            PersonalPostalCode: "04001",
            Fax: null,

            WorkplaceAddress: "Nemocnicna 5",
            WorkplacePostalCode: "04001",
            WorkplaceCity: "Kosice",
            WorkplacePhone: null,
            WorkplaceEmail: null,
            WorkplaceFax: null,

            WorkplaceFunction: "Lekar",
            Focus: "Infektologia",

            GraduationYear: 2015,
            Chamber: "SLK",
            ChamberId: "12345",

            Password: "Heslo123!"
        );

        var result = await _controller.Register(request);

        Assert.IsInstanceOfType(
            result,
            typeof(CreatedAtActionResult));
    }
}