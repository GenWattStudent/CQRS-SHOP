using CarShop.Application.Commands.CarCommands;
using CarShop.Application.Queries.CarQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarShop.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CarController : ControllerBase
{
    private readonly ILogger<CarController> _logger;
    private readonly IMediator _mediator;  

    public CarController(ILogger<CarController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCar(CreateCarCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }

        return CreatedAtAction(nameof(CreateCar), new { result.Value }, result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCars()
    {
        var result = await _mediator.Send(new GetAllCarsQuery());

        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCar(UpdateCarCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess) {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCar(int id)
    {
        var command = new DeleteCarCommand(id);
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCarById(int id)
    {
        var command = new GetCarByIdQuery(id);
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }
}
