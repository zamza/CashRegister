using AutoMapper;
using CashRegister.Web.Configuration;
using CashRegister.Web.Configuration.Containers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Automapper
//var mappingConfig = new MapperConfiguration(mapperConfig =>
//{
//    mapperConfig.AddProfile(new WebMapperConfiguration());
//});
//IMapper autoMapper = mappingConfig.CreateMapper();
//builder.Services.AddSingleton(autoMapper);
builder.Services.AddAutoMapper(typeof(WebMapperConfiguration));


DomainContainer.ConfigureDependencies(builder.Services);

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
