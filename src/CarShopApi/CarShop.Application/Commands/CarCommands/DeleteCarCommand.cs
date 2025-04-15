using CarShop.Application.Abstractions;

namespace CarShop.Application.Commands.CarCommands;

public record DeleteCarCommand(Guid Id) : ICommand<Guid>
{
}
