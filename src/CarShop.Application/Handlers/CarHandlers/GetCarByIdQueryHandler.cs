using CarShop.Application.Abstractions;
using CarShop.Application.Common;
using CarShop.Application.Queries.CarQueries;
using CarShop.Domain.Entities;
using CarShop.Shared.Data;

namespace CarShop.Application.Handlers.CarHandlers;

public class GetCarByIdQueryHandler : ICommandHandler<GetCarByIdQuery, Car>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCarByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Car>> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
    {
        var car = await _unitOfWork.Cars.GetByIdAsync(request.Id);

        if (car == null)
        {
            return Result<Car>.Failure("The car was not found");
        }

        return Result<Car>.Success(car);
    }
}
