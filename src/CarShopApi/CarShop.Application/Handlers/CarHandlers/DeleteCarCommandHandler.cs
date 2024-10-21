using CarShop.Application.Abstractions;
using CarShop.Application.Commands.CarCommands;
using CarShop.Core.Presistence;
using CarShop.Domain.Results;

namespace CarShop.Application.Handlers.CarHandlers;

public class DeleteCarCommandHandler : ICommandHandler<DeleteCarCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCarCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
    {
        var carToDelete = await _unitOfWork.Cars.GetByIdAsync(request.Id);

        if (carToDelete == null)
        {
            return Result<int>.Failure("The car was not found", ErrorType.NotFound);
        }

        _unitOfWork.Cars.Delete(carToDelete);
        await _unitOfWork.SaveChangesAsync();

        return Result<int>.Success(carToDelete.Id);
    }
}
