using CloudinaryDotNet;
using ecommerce.api.Data;
using ecommerce.api.Managers;


var builder = WebApplication.CreateBuilder(args);

var  CorsPolicy = "_corsPolicy";

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CorsPolicy,
        policy  =>
        {
            policy.WithOrigins(
                    "http://localhost:3000",
                    "https://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

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
builder.Services.Configure<CloudinaryConfig>(builder.Configuration.GetSection("CloudinaryConfig"));
builder.Services.Configure<DbConfig>(builder.Configuration.GetSection("DbConfig"));

// Managers
builder.Services.AddSingleton<ProductManager>();
builder.Services.AddSingleton<OrderManager>();
builder.Services.AddSingleton<UploadManager>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(CorsPolicy);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();