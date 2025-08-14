using PinchPayments.ElevatorApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegisterServices();

var app = builder.Build();

app.SetupApp();

app.Run();
