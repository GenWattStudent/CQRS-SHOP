using CarShop.API.Middlewares;
using CarShop.Application.Common.Behaviours;
using CarShop.Application.Handlers.CarHandlers;
using CarShop.Shared.Data;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<DataSeeder>();
builder.Services.AddValidatorsFromAssembly(typeof(CreateCarCommandHandler).Assembly);
builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(typeof(CreateCarCommandHandler).Assembly);
    configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
builder.Services.AddAutoMapper(typeof(CreateCarCommandHandler).Assembly);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ValidationExceptionHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var seeder = services.GetRequiredService<DataSeeder>();
    
    await seeder.SeedData();
}

app.Run();
