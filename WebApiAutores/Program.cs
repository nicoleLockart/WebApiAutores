using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using WebApiAutores;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var service = builder.Services;
service.AddControllers();
service.AddDbContext<AplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnexion")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
