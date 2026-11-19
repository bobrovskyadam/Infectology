using Infectology.Database.Contexts;
using Infectology.Library.DTOs.Requests.Users;
using Infectology.Library.Entities.Users;
using Infectology.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Infectology.Tests;

[TestClass]
public sealed class LoginControllerTests
{
    private UserContext _context = null!;
    private LoginController _controller = null!;

    [TestInitialize]
    public void TestInit()
    {
        var options = new DbContextOptionsBuilder<UserContext>()
            .UseInMemoryDatabase("TestDb_Login")
            .Options;

        _context = new UserContext(options);

        var configMock = new Mock<IConfiguration>();
        configMock.Setup(c => c["Jwt:Key"]).Returns("TestKeyMinimalne32ZnakovDlhy!!");
        configMock.Setup(c => c["Jwt:Issuer"]).Returns("Infectology");
        configMock.Setup(c => c["Jwt:Audience"]).Returns("InfectologyUsers");

        _controller = new LoginController(configMock.Object, _context);
    }

    [TestCleanup]
    public void TestCleanup()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [TestMethod]
    public async Task Login_UserNotFound_ReturnsUnauthorized()
    {
        var request = new LoginRequest(
            Email: "notexist@test.sk",
            Password: "Heslo123!"
        );

        var result = await _controller.Login(request);

        Assert.IsInstanceOfType(result, typeof(UnauthorizedObjectResult));
    }

    [TestMethod]
    public async Task Login_WrongPassword_ReturnsUnauthorized()
    {
        var contact = new Contact { EmailAddress = "test@test.sk" };
        var user = new Doctor
        {
            FirstName = "Test",
            LastName = "User",
            PasswordHash = Isopoh.Cryptography.Argon2.Argon2.Hash("SpravneHeslo123!"),
            Role = Role.Doctor,
            DateOfBirth = new DateOnly(1990, 1, 1),
            GraduationYear = 2015,
            WorkplaceFunction = "Lekar",
            Focus = "Infektologia",
            Chamber = "SLK",
            ChamberId = "12345",
            PersonalContact = contact
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var request = new LoginRequest(
            Email: "test@test.sk",
            Password: "ZleHeslo123!"
        );

        var result = await _controller.Login(request);

        Assert.IsInstanceOfType(result, typeof(UnauthorizedObjectResult));
    }

}