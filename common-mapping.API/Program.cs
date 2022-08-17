using common_mapping.API;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddJsonOptions((x =>
{
    x.JsonSerializerOptions.WriteIndented = true;
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
}));

builder.Services.AddMappingSettings(builder.Configuration);
builder.Services.AddSingleton<common_mapping.Interfaces.IMapping, common_mapping.Mapping>();

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
