using CarShop.Application.Commands.CarCommands;
using CarShop.Application.Handlers.CarHandlers;
using CarShop.Application.Unit.Tests.Setups;
using CarShop.Domain.Entities;
using Moq;

namespace CarShop.Application.Unit.Tests.Handlers.CarHandlers;

public class CreateCarCommandHandlerTests
{
    private readonly CarSetup _carSetup = new();
    private readonly CreateCarCommandHandler _handler;

    public CreateCarCommandHandlerTests()
    {
        _handler = new CreateCarCommandHandler(_carSetup.MockUnitOfWork.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreateCar_WhenCommandIsValid()
    {
        // Arrange
        _carSetup.MockUnitOfWork.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);
        var command = new CreateCarCommand(
                Brand: "Toyota",
                Model: "Corolla",
                Year: 2021,
                Price: 20000,
                Color: "Red",
                VIN: "12345678901234567"
            );

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _carSetup.MockUnitOfWork.Verify(u => u.Cars.Add(It.IsAny<Car>()), Times.Once);
        _carSetup.MockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(command.Brand, result.Value.Brand);
        Assert.Equal(command.Model, result.Value.Model);
        Assert.Equal(command.Year, result.Value.Year);
        Assert.Equal(command.Price, result.Value.Price);
        Assert.Equal(command.Color, result.Value.Color);
        Assert.Equal(command.VIN, result.Value.VIN);
    }
}
