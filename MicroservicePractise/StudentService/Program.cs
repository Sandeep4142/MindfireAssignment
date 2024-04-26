using Microsoft.EntityFrameworkCore;
using StudentService.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpClient("course", config =>
    config.BaseAddress = new Uri("http://localhost:5276"));

builder.Services.AddHttpClient("courseByGateway", config =>
    config.BaseAddress = new Uri("http://localhost:5175"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TempContext>(options
    =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DBCS")));

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
