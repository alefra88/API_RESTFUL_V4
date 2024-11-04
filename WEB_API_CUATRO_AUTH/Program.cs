using WEB_API_CUATRO_AUTH.Config.Impl;
using WEB_API_CUATRO_AUTH.Config.Interfaces;
using WEB_API_CUATRO_AUTH.Repositories.Impl;
using WEB_API_CUATRO_AUTH.Repositories.Interfaces;
using WEB_API_CUATRO_AUTH.Services.Impl;
using WEB_API_CUATRO_AUTH.Services.Interfaces;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//SeriLog Config
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

//conexion
builder.Services.AddScoped<IConnection, Connection>();

//reps
builder.Services.AddScoped<IAlumnoRepository, AlumnoRepository>();
builder.Services.AddScoped<IAlumnoService, AlumnoService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//habilita cors
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
try
{
    {
        Log.Information("Iniciando app...");
        app.Run();
    }
}
catch(Exception ex)
{
    Log.Fatal(ex,"La app falló al inciar");
}
finally
{
    Log.CloseAndFlush();
}