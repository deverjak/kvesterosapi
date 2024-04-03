using KvesterosApi;
using KvesterosApi.Configuration;
using KvesterosApi.Models;
using KvesterosApi.Repository;
using KvesterosApi.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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

app.Run();