using CarShop.Application.Abstractions;
using CarShop.Application.Commands.CarCommands;
using CarShop.Core.Presistence;
using CarShop.Domain.Entities;
using CarShop.Domain.Results;

namespace CarShop.Application.Handlers.CarHandlers;

public class CreateCarCommandHandler : ICommandHandler<CreateCarCommand, Car>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateCarCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Car>> Handle(CreateCarCommand request, CancellationToken cancellationToken)
    {
        var car = new Car
        {
            Brand = request.Brand,
            Model = request.Model,
            Year = request.Year,
            Color = request.Color,
            VIN = request.VIN,
            Price = request.Price,
            ImageUrl = request.ImageUrl
        };

        _unitOfWork.Cars.Add(car);
        await _unitOfWork.SaveChangesAsync();

        return Result<Car>.Success(car);
    }
}
