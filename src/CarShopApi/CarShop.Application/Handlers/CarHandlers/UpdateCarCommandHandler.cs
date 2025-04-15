using CarShop.Application.Abstractions;
using CarShop.Application.Commands.CarCommands;
using CarShop.Core.Presistence;
using CarShop.Domain.Entities;
using CarShop.Domain.Results;

namespace CarShop.Application.Handlers.CarHandlers;

public class UpdateCarCommandHandler : ICommandHandler<UpdateCarCommand, Car>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCarCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Car>> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var carToUpdate = await _unitOfWork.Cars.GetByIdAsync(request.Id);

            if (carToUpdate == null)
            {
                return Result<Car>.Failure("The car was not found", ErrorType.NotFound);
            }

            carToUpdate.UpdateDetails(
                request.Brand,
                request.Model,
                request.Year,
                request.Color,
                request.Price,
                request.ImageUrl
            );

            _unitOfWork.Cars.Update(carToUpdate);
            await _unitOfWork.SaveChangesAsync();

            return Result<Car>.Success(carToUpdate);
        }
        catch (ArgumentException ex)
        {
            return Result<Car>.Failure(ex.Message, ErrorType.ValidationError);
        }
    }
}
