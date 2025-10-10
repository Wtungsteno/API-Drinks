using B_BusinessLogicWebAPILoGiud;
using D_RepoAbstrWebAPILoGiud;
using E_RepoImplWebAPILoGiud;
using Microsoft.EntityFrameworkCore;
using WebAPILoGiud;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? connStr = builder.Configuration.GetConnectionString("Database:ConnectionStrings:Default:");
builder.Services
    .AddDbContext<DrinkDbContext>(opt => opt.UseSqlServer(connStr))
    .AddScoped<IAppRepository, AppRepository>()
    .AddScoped<IDrinkService, DrinkService>();

//builder.Services.AddSingleton<Mapper>();

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
app.UseCors();
app.Run();