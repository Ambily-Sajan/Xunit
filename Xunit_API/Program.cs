using Microsoft.EntityFrameworkCore;
using Xunit_API.Data;
using Xunit_API.Services;
using Xunit_API.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//inject mysql
builder.Services.AddDbContext<VehicleDbContext>(option =>
option.UseMySQL(builder.Configuration.GetConnectionString("connectionString")));

builder.Services.AddScoped<IVehicleType,vehicleTypeRepository>();
builder.Services.AddScoped<IBrand,brandRepository>();
builder.Services.AddScoped<IModelInterface, modelRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
