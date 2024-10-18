using CarShop.Application.Abstractions;
using CarShop.Domain.Entities;

namespace CarShop.Application.Queries.CarQueries;

public record GetAllCarsQuery : ICommand<IEnumerable<Car>>
{
}