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
        try
        {
            // Use the constructor to ensure all domain rules are enforced
            var car = new Car(
                request.Brand,
                request.Model,
                request.Year,
                request.Color,
                request.VIN,
                request.Price,
                request.ImageUrl
            );

            _unitOfWork.Cars.Add(car);
            await _unitOfWork.SaveChangesAsync();

            return Result<Car>.Success(car);
        }
        catch (ArgumentException ex)
        {
            return Result<Car>.Failure(ex.Message, ErrorType.ValidationError);
        }
    }
}
