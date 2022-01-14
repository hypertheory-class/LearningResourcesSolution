using AutoMapper;
using LearningResourcesApi.Data;
using LearningResourcesApi.Profiles;
using LearningResourcesApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// if some thing you create needs X, use Y

// Managing the lifetime of the dependency.
builder.Services.AddTransient<ISystemTime, SystemTime>();

builder.Services.AddDbContext<LearningResourcesDataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("resources"));
});

var mapperConfiguration = new MapperConfiguration((config) =>
{
    config.AddProfile<LearningResourcesProfile>();
    // more profiles.
});

var mapper = mapperConfiguration.CreateMapper();

builder.Services.AddSingleton<MapperConfiguration>(mapperConfiguration);
builder.Services.AddSingleton<IMapper>(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();

app.MapControllers();

app.Run();
