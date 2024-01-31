using Domain.Interfaces;
using Infrastructure.Maps;
using Infrastructure.Repositories;
using Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

InsuranceMapper.ConfigureMappings();
UserMapper.ConfigureMappings();

builder.Services.AddScoped<IInsuranceService>(Provider =>
{
    return new InsuranceService(new InsuranceRepository());
});

builder.Services.AddScoped<IUserService>(Provider =>
{
    return new UserService(new UserRepository());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();
