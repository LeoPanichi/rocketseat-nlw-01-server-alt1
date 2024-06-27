using Microsoft.AspNetCore.Mvc;
using server;
using server.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext,DataContext>();
builder.Services.AddScoped<DataContext>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();
//IWebHostEnvironment environment = app.Environment;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
builder.Services.AddCors(options => 
{ 
    options.AddPolicy("CorsPolicy", builder => 
        builder.AllowAnyOrigin() 
       .AllowAnyMethod() 
       .AllowAnyHeader()); 
});

app.UseAuthorization();

app.MapControllers();

app.Run();
