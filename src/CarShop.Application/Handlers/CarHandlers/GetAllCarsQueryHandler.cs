using CarShop.Application.Abstractions;
using CarShop.Application.Common;
using CarShop.Application.Queries.CarQueries;
using CarShop.Domain.Entities;
using CarShop.Shared.Data;

namespace CarShop.Application.Handlers.CarHandlers;

public class GetAllCarsQueryHandler : ICommandHandler<GetAllCarsQuery, IEnumerable<Car>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllCarsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IEnumerable<Car>>> Handle(GetAllCarsQuery request, CancellationToken cancellationToken)
    {
        var cars = await _unitOfWork.Cars.GetAllAsync();

        return Result<IEnumerable<Car>>.Success(cars);
    }
}
