using CarShop.Domain.Entities;
using CarShop.Integration.Tests.Common;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Json;

namespace CarShop.Integration.Tests.Controllers;

public class CarControllerTest : BaseWebApp
{
    private readonly CarSetup _carSetup;

    public CarControllerTest() : base()
    {
        _carSetup = new CarSetup();
    }

    [Fact]
    public async Task Get_GetAll_ReturnsCars()
    {
        // Arrange

        // Act  
        var response = await _client.GetAsync(Constants.CAR_ENDPOINT);
        var cars = await response.Content.ReadFromJsonAsync<IEnumerable<Car>>();

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.NotNull(cars);
        Assert.NotEmpty(cars);
    }

    [Fact]
    public async Task Get_GetById_ReturnsCar()
    {
        // Arrange
        var id = 1;

        // Act
        var response = await _client.GetAsync($"{Constants.CAR_ENDPOINT}/{id}");
        var car = await response.Content.ReadFromJsonAsync<Car>();

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.NotNull(car);
        Assert.Equal(id, car.Id);
    }

    [Fact]
    public async Task Get_GetById_ReturnsNotFound()
    {
        // Arrange
        var id = 1000;

        // Act
        var response = await _client.GetAsync($"{Constants.CAR_ENDPOINT}/{id}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Post_Create_ReturnsCar()
    {
        // Arrange
        var car = _carSetup.Create();

        // Act
        var response = await _client.PostAsJsonAsync(Constants.CAR_ENDPOINT, car);
        var createdCar = await response.Content.ReadFromJsonAsync<Car>();

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(createdCar);
        AssertCar(car, createdCar);
    }

    [Fact]
    public async Task Post_Create_ReturnsBadRequest()
    {
        // Arrange
        var car = _carSetup.Create();
        car.VIN = null;

        // Act
        var response = await _client.PostAsJsonAsync(Constants.CAR_ENDPOINT, car);
        var problemDetails = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        
        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        Assert.NotNull(problemDetails);
        Assert.Equal("One or more validation errors occurred.", problemDetails.Title);
        Assert.Equal(400, problemDetails.Status);
        Assert.Contains("VIN", problemDetails.Errors.Keys);
        Assert.Contains("The VIN field is required.", problemDetails.Errors["VIN"]);
    }

    [Fact]
    public async Task Put_Update_ReturnsCar()
    {
        // Arrange
        var id = 2;
        var car = _carSetup.Update(id);

        // Act
        var response = await _client.PutAsJsonAsync($"{Constants.CAR_ENDPOINT}/{id}", car);
        var updatedCar = await response.Content.ReadFromJsonAsync<Car>();

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.NotNull(updatedCar);
        AssertCar(car, updatedCar);
    }

    [Fact]
    public async Task Put_Update_ReturnsNotFound()
    {
        // Arrange
        var id = 1000;
        var car = _carSetup.Update(id);

        // Act
        var response = await _client.PutAsJsonAsync($"{Constants.CAR_ENDPOINT}/{id}", car);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Delete_Delete_ReturnsNoContent()
    {
        // Arrange
        var id = 1;

        // Act
        var response = await _client.DeleteAsync($"{Constants.CAR_ENDPOINT}/{id}");
        var returnId = await response.Content.ReadFromJsonAsync<int>();

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(id, returnId);
    }

    private void AssertCar(Car car, Car createdCar)
    {
        Assert.Equal(car.Brand, createdCar.Brand);
        Assert.Equal(car.Model, createdCar.Model);
        Assert.Equal(car.Year, createdCar.Year);
        Assert.Equal(car.Price, createdCar.Price);
        Assert.Equal(car.Color, createdCar.Color);
        Assert.Equal(car.VIN, createdCar.VIN);
    }
}
