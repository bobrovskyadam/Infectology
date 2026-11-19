using Infectology.Library.DTOs.Requests.Users;
using Infectology.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Infectology.Tests;

[TestClass]
public sealed class ContactControllerTests
{
    private ContactController _controller = null!;

    [TestInitialize]
    public void TestInit()
    {
        _controller = new ContactController();
    }

    [TestMethod]
    public void SubmitMessage_GdprNotAccepted_ReturnsBadRequest()
    {
        var dto = new ContactMessageRequest(
            Name: "Test",
            Email: "test@test.sk",
            Message: "Test message",
            GdprAccepted: false
        );

        var result = _controller.SubmitMessage(dto);

        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public void SubmitMessage_GdprAccepted_ReturnsOk()
    {
        var dto = new ContactMessageRequest(
            Name: "Test",
            Email: "test@test.sk",
            Message: "Test message",
            GdprAccepted: true
        );

        var result = _controller.SubmitMessage(dto);

        Assert.IsInstanceOfType(result, typeof(OkResult));
    }
}