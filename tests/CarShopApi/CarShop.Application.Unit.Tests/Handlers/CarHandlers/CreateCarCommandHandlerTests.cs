using CarShop.Application.Commands.CarCommands;
using CarShop.Application.Handlers.CarHandlers;
using CarShop.Application.Unit.Tests.Fixtures;
using CarShop.Application.Unit.Tests.Setups;
using CarShop.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace CarShop.Application.Unit.Tests.Handlers.CarHandlers;

public class CreateCarCommandHandlerTests : IClassFixture<CarFixture>, IDisposable 
{
    private readonly CarSetup _carSetup;
    private readonly CreateCarCommandHandler _handler;

    public CreateCarCommandHandlerTests(CarFixture fixture)
    {
        _carSetup = fixture.ServiceProvider.GetRequiredService<CarSetup>();
        _handler = new CreateCarCommandHandler(_carSetup.MockUnitOfWork.Object);
    }

    [Theory]
    [InlineData("Toyota", "Corolla", 2021, 20000, "Red", "12345678901234567")]
    [InlineData("Honda", "Civic", 2020, 18000, "Blue", "12345678901234568")]
    public async Task Handle_ShouldCreateCar_WhenCommandIsValid(string brand, string model, int year, decimal price, string color, string vin)
    {
        // Arrange
        _carSetup.MockUnitOfWork.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);
        var command = new CreateCarCommand(brand, model, year, color, vin, price);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _carSetup.MockUnitOfWork.Verify(u => u.Cars.Add(It.IsAny<Car>()), Times.Once);
        _carSetup.MockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Brand.Should().Be(command.Brand);
        result.Value.Model.Should().Be(command.Model);
        result.Value.Year.Should().Be(command.Year);
        result.Value.Price.Should().Be(command.Price);
        result.Value.Color.Should().Be(command.Color);
        result.Value.VIN.Should().Be(command.VIN);
    }

    public void Dispose()
    {
        _carSetup.MockUnitOfWork.Invocations.Clear();
        _carSetup.MockCarRepository.Invocations.Clear();
    }
}
