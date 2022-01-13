using LearningResourcesApi.Data;
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
