using aspnet.Controllers;
using aspnet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace LocationVoiture.Tests;

public class HomeControllerTests
{
    [Fact]
    public void Index_ReturnsViewResult()
    {
        var controller = new HomeController(NullLogger<HomeController>.Instance);

        var result = controller.Index();

        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void Privacy_ReturnsViewResult()
    {
        var controller = new HomeController(NullLogger<HomeController>.Instance);

        var result = controller.Privacy();

        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void Error_ReturnsErrorViewModelWithRequestId()
    {
        var controller = new HomeController(NullLogger<HomeController>.Instance)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { TraceIdentifier = "trace-id-123" }
            }
        };

        var result = controller.Error() as ViewResult;

        Assert.NotNull(result);
        var model = Assert.IsType<ErrorViewModel>(result!.Model);
        Assert.Equal("trace-id-123", model.RequestId);
        Assert.True(model.ShowRequestId);
    }
}