using System.Text.Json.Serialization;
using TrucksApi;
using TrucksApi.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Trucks") ?? "Data Source=Trucks.db";
builder.Services.AddSqlite<TrucksDb>(connectionString);

// Add services to the container.

builder.Services.AddControllers()
        .AddJsonOptions(options => 
        { 
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); 
        });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
builder.Services.AddScoped<ITrucksRepository, TrucksRepository>();

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
