using aspnet.Controllers;
using aspnet.Models;
using aspnet.Models.ViewModels;
using LocationVoiture.Tests.TestDoubles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace LocationVoiture.Tests;

public class CarControllerTests
{
    [Fact]
    public void Index_ReturnsCarsFromRepository()
    {
        var cars = new List<Car>
        {
            new() { Id = 1, PlateNumber = "AA-111-AA", CarModelId = 10 },
            new() { Id = 2, PlateNumber = "BB-222-BB", CarModelId = 11 }
        };
        var repository = new InMemoryCarRepository(cars);
        var controller = new CarController(NullLogger<CarController>.Instance, repository);

        var result = controller.Index() as ViewResult;

        Assert.NotNull(result);
        Assert.Same(repository.Cars, result!.Model);
    }

    [Fact]
    public void CreateGet_ReturnsViewResult()
    {
        var controller = new CarController(NullLogger<CarController>.Instance, new InMemoryCarRepository());

        var result = controller.Create();

        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void CreatePost_WhenModelIsValid_AddsCarAndRedirectsToIndex()
    {
        var repository = new InMemoryCarRepository();
        var controller = new CarController(NullLogger<CarController>.Instance, repository);

        var result = controller.Create(new CarCreateVM
        {
            PlateNumber = "CC-333-CC",
            CarModelId = 42
        }) as RedirectToActionResult;

        Assert.NotNull(result);
        Assert.Equal("Index", result!.ActionName);
        Assert.Single(repository.GetAllCars());
        Assert.Equal("CC-333-CC", repository.GetAllCars()[0].PlateNumber);
        Assert.Equal(42, repository.GetAllCars()[0].CarModelId);
    }

    [Fact]
    public void CreatePost_WhenModelIsInvalid_ReturnsViewWithSameModel()
    {
        var controller = new CarController(NullLogger<CarController>.Instance, new InMemoryCarRepository());
        controller.ModelState.AddModelError(nameof(CarCreateVM.PlateNumber), "Required");

        var input = new CarCreateVM { PlateNumber = string.Empty, CarModelId = 0 };
        var result = controller.Create(input) as ViewResult;

        Assert.NotNull(result);
        Assert.Same(input, result!.Model);
    }

    [Fact]
    public void UpdateGet_WhenCarExists_ReturnsPrefilledViewModel()
    {
        var repository = new InMemoryCarRepository(new[]
        {
            new Car { Id = 7, PlateNumber = "DD-444-DD", CarModelId = 99 }
        });
        var controller = new CarController(NullLogger<CarController>.Instance, repository);

        var result = controller.Update(7) as ViewResult;

        Assert.NotNull(result);
        var model = Assert.IsType<CarUpdateVM>(result!.Model);
        Assert.Equal(7, model.Id);
        Assert.Equal("DD-444-DD", model.PlateNumber);
        Assert.Equal(99, model.CarModelId);
    }

    [Fact]
    public void UpdateGet_WhenCarDoesNotExist_ReturnsNotFound()
    {
        var controller = new CarController(NullLogger<CarController>.Instance, new InMemoryCarRepository());

        var result = controller.Update(404);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void DeleteConfirmed_WhenCarExists_RemovesCarAndRedirects()
    {
        var repository = new InMemoryCarRepository(new[]
        {
            new Car { Id = 3, PlateNumber = "EE-555-EE", CarModelId = 12 }
        });
        var controller = new CarController(NullLogger<CarController>.Instance, repository);

        var result = controller.DeleteConfirmed(3) as RedirectToActionResult;

        Assert.NotNull(result);
        Assert.Equal("Index", result!.ActionName);
        Assert.Empty(repository.GetAllCars());
    }
}