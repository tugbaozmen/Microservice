using Course.Services.Catalog.Controllers;
using Course.Services.Catalog.Dtos;
using Course.Services.Catalog.Services;
using Course.Services.Catalog.Settings;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICourseService, CourseService>();

//Automapper eklendi
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//mongo için db bilgilerinin getirildiði kýsým appsettings içindeki databasesettings ile
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));

builder.Services.AddSingleton<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});


var app = builder.Build();

app.MapControllers();


//Burasý mongodbye proje ayaða kalkarken direk datalarý oluþturmasý için yazýlmýþtýr. Create metodu ile oluþturulan datalarýn aynýsýdýr.
//using (var scope = app.Services.CreateScope())
//{
//    var serviceProvider = scope.ServiceProvider;

//    var categoryService = serviceProvider.GetRequiredService<ICategoryService>();

//    if (!(await categoryService.GetAllAsync()).Data.Any())
//    {
//        await categoryService.CreateAsync(new CategoryCreateDto { Name = "Asp.net Core Kursu" });
//        await categoryService.CreateAsync(new CategoryCreateDto { Name = "Asp.net Core API Kursu" });
//    }
//}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();


}


app.Run();

