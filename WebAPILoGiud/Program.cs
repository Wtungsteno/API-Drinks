using Microsoft.EntityFrameworkCore;
using WebAPILoGiud;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? connStr = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<DrinkDbContext>(options => options.UseSqlServer(connStr));

builder.Services.AddSingleton<Mapper>();

builder.Services.AddCors(options => options.AddDefaultPolicy(config =>
{
    config.AllowAnyHeader().AllowAnyMethod();
    config.AllowAnyOrigin();
}));

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
