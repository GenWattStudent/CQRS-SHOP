using AutoMapper;
using CarShop.Application.Abstractions;
using CarShop.Application.Commands.CarCommands;
using CarShop.Domain.Entities;
using CarShop.Domain.Results;
using CarShop.Shared.Data;

namespace CarShop.Application.Handlers.CarHandlers;

public class UpdateCarCommandHandler : ICommandHandler<UpdateCarCommand, Car>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCarCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<Car>> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
    {
        var carToUpdate = await _unitOfWork.Cars.GetByIdAsync(request.Id);

        if (carToUpdate == null)
        {
            return Result<Car>.Failure("The car was not found");
        }

        _mapper.Map(request, carToUpdate);

        _unitOfWork.Cars.Update(carToUpdate);
        await _unitOfWork.SaveChangesAsync();

        return Result<Car>.Success(carToUpdate);
    }
}
