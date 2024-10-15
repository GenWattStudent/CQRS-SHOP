using CarShop.Application.Abstractions;

namespace CarShop.Application.Commands.CarCommands;

public record DeleteCarCommand(int Id) : ICommand<int>
{
}
