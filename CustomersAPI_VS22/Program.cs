using CustomersAPI_VS22.CasosDeUso;
using CustomersAPI_VS22.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(); //se inyectan las dependencias
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CustomerDatabaseContext>(builderSQL =>
{
    builderSQL.UseSqlServer(builder.Configuration.GetConnectionString("Connection1")); //Hemos guardado el string de conexion en el fitxero de configuración de appsettings
});

builder.Services.AddScoped<IUpdateCustomerUseCase, UpdateCustomerUseCase>();
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
