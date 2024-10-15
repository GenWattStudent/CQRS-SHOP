using CarShop.Domain.Entities;
using CarShop.Application.Abstractions;

namespace CarShop.Application.Queries.CarQueries;

public record GetAllCarsQuery : ICommand<IEnumerable<Car>>
{
}