using KvesterosAdminApi;
using KvesterosAdminApi.Configuration;
using KvesterosAdminApi.Models;
using KvesterosAdminApi.Repository;
using KvesterosAdminApi.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseInMemoryDatabase("InMemoryDatabase"));
builder.Services.AddScoped<IRepository<Hike>, HikeRepository>();

var imageStorageSettings = new ImageStorageSettings();
builder.Configuration.GetSection("ImageStorageSettings").Bind(imageStorageSettings);
builder.Services.AddSingleton(imageStorageSettings);

builder.Services.AddSingleton<IImageStorageService>(serviceProvider =>
{
    // Resolve ImageStorageSettings from DI container
    var settings = serviceProvider.GetRequiredService<ImageStorageSettings>();
    return new LocalImageStorageService(settings.FolderPath);
});

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

// Add test data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    AddTestData(context);
}

app.Run();

void AddTestData(ApplicationDbContext context)
{
    var testHike1 = new Hike { Id = 1, Name = "Hike 1", Description = "Description 1", DifficultyLevel = DifficultyLevel.Moderate, Distance = 10, Route = "Route 1" };
    var testHike2 = new Hike { Id = 2, Name = "Hike 2", Description = "Description 2", DifficultyLevel = DifficultyLevel.Hard, Distance = 20, Route = "Route 2" };
    var testHike3 = new Hike { Id = 3, Name = "Hike 3", Description = "Description 3", DifficultyLevel = DifficultyLevel.Easy, Distance = 5, Route = "Route 3" };
    var testHike4 = new Hike { Id = 4, Name = "Hike 4", Description = "Description 3", DifficultyLevel = DifficultyLevel.Uknown, Distance = 8, Route = "Route 4" };

    context.Hikes.AddRange(testHike1, testHike2, testHike3, testHike4);
    context.SaveChanges();
}