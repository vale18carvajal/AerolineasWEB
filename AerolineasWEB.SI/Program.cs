using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




//CONFIGURACIÓN DE LA BD
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AerolineasWEB.DA.DBContexto>(options => options.UseSqlServer(connectionString));

//builder.Services.AddDbContext<AerolineasWEB.DA.DBContexto>(options => options.UseInMemoryDatabase("aerolineas_bd"));
builder.Services.AddScoped<AerolineasWEB.BL.IAerolineaRepository, AerolineasWEB.DA.AerolineaRepository>();
builder.Services.AddScoped<AerolineasWEB.BL.IAvionRepository, AerolineasWEB.DA.AvionRepository>();
builder.Services.AddScoped<AerolineasWEB.BL.IPropietarioRepository, AerolineasWEB.DA.PropietarioRepository>();
builder.Services.AddScoped<AerolineasWEB.BL.IAdministradorAerolinea, AerolineasWEB.BL.AdministradorAerolinea>();
builder.Services.AddScoped<AerolineasWEB.BL.IAdministradorAvion, AerolineasWEB.BL.AdministradorAvion>();
builder.Services.AddScoped<AerolineasWEB.BL.IAdministradorPropietario, AerolineasWEB.BL.AdministradorPropietario>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
