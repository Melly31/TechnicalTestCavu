using System.Reflection;
using AutoMapper;
using CavuTechTest.DAL;
using CavuTechTest.DAL.IReadOnlyRepository;
using CavuTechTest.DAL.IRepository;
using CavuTechTest.DAL.ReadOnlyRepository;
using CavuTechTest.DAL.Repository;
using CavuTechTest.DataAccess.IReadOnlyRepository;
using CavuTechTest.DataAccess.ReadOnlyRepository;
using CavuTechTest.Mediation;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DatabaseContext>();
builder.Services.AddTransient<IAvailableSpacesRepository, AvailableSpacesRepository>();
builder.Services.AddTransient<IBookingRepository, BookingRepository>();
builder.Services.AddTransient<IPricingRepository, PricingRepository>();


builder.Services.AddMediatR(typeof(Program).GetTypeInfo().Assembly);


// Auto Mapper Configurations
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddMvc();

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
