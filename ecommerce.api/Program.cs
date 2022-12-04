using CloudinaryDotNet;
using ecommerce.api.Data;
using ecommerce.api.Managers;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Cloudinary
var cloudName = builder.Configuration.GetValue<string>("CloudinaryConfig:CloudName");
var apiKey = builder.Configuration.GetValue<string>("CloudinaryConfig:ApiKey");
var apiSecret = builder.Configuration.GetValue<string>("CloudinaryConfig:ApiSecret");

if (new[] { cloudName, apiKey, apiSecret }.Any(string.IsNullOrWhiteSpace))
{
    throw new ArgumentException("Please specify Cloudinary account details!");
}

builder.Services.AddSingleton(new Cloudinary(new Account(cloudName, apiKey, apiSecret)));

// Configuration
builder.Services.AddSingleton<EcommerceDbContext>();
builder.Services.Configure<CloudinaryConfig>(builder.Configuration.GetSection("CloudinaryConfig"));

// Managers
builder.Services.AddSingleton<AuthManager>();
builder.Services.AddSingleton<DiscountManager>();
builder.Services.AddSingleton<CartItemManager>();
builder.Services.AddSingleton<ProductManager>();
builder.Services.AddSingleton<CartManager>();
builder.Services.AddSingleton<OrderManager>();
builder.Services.AddSingleton<UploadManager>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();