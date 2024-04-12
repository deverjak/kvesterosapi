using Kvesteros.Api;
using Kvesteros.Api.Configuration;
using Kvesteros.Api.Extensions;
using Kvesteros.Api.Repository;
using Kvesteros.Api.Services;
using Kvesteros.Application;
using Kvesteros.Application.Database;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddRepositories(typeof(HikeRepository).Assembly);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowEverything", builder =>
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader());
    });

builder.Services.AddDatabase(configuration["Database:ConnectionString"]!);

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

app.UseCors("AllowEverything");

var dbInitializer = app.Services.GetRequiredService<DbInitializer>();
await dbInitializer.InitializeAsync();

app.Run();