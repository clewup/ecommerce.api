using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using AutoMapper;
using CloudinaryDotNet;
using ecommerce.api.Data;
using ecommerce.api.DataManagers;
using ecommerce.api.DataManagers.Contracts;
using ecommerce.api.Infrastructure;
using ecommerce.api.Managers;
using ecommerce.api.Managers.Contracts;
using ecommerce.api.Mappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var CorsPolicy = "_corsPolicy";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var isDevelopment = builder.Environment.IsDevelopment();

if (isDevelopment)
{
    // Local Connection Strings
    builder.Configuration.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);
}

// Database
var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");
                           
if (isDevelopment)
    connectionString = builder.Configuration.GetConnectionString("EcommerceConnection");

builder.Services.AddEntityFrameworkNpgsql().AddDbContext<EcommerceDbContext>(options =>
{
    var m = Regex.Match(connectionString, @"postgres://(.*):(.*)@(.*):(.*)/(.*)");
    options.UseNpgsql(
        $"Server={m.Groups[3]};Port={m.Groups[4]};User Id={m.Groups[1]};Password={m.Groups[2]};Database={m.Groups[5]};sslmode=Prefer;Trust Server Certificate=true");
});
    
// Cloudinary
var cloudName = Environment.GetEnvironmentVariable("CLOUDINARY_NAME");
var apiKey = Environment.GetEnvironmentVariable("CLOUDINARY_KEY");
var apiSecret = Environment.GetEnvironmentVariable("CLOUDINARY_SECRET");

if (isDevelopment)
{
    cloudName = builder.Configuration["Cloudinary:CloudName"];
    apiKey = builder.Configuration["Cloudinary:ApiKey"];
    apiSecret = builder.Configuration["Cloudinary:ApiSecret"];
}

builder.Services.AddSingleton(new Cloudinary(new Account(cloudName, apiKey, apiSecret)));

// Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CorsPolicy,
        policy  =>
        {
            policy.WithOrigins(
                    "http://localhost:3000",
                    "https://localhost:3000",
                    "http://ecommerce.clewup.co.uk",
                    "https://ecommerce.clewup.co.uk")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// Jwt
var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY");

if (isDevelopment)
{
    jwtIssuer = builder.Configuration["Jwt:Issuer"];
    jwtAudience = builder.Configuration["Jwt:Audience"];
    jwtKey = builder.Configuration["Jwt:Key"];
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});
builder.Services.AddAuthorization(options =>
{
    // Role based policies
    options.AddPolicy(RoleType.Developer, policy =>
        policy.RequireRole(RoleType.Developer));
    options.AddPolicy(RoleType.Employee, policy =>
        policy.RequireRole(RoleType.Employee, RoleType.Developer));
    options.AddPolicy(RoleType.External, policy =>
        policy.RequireRole(RoleType.External, RoleType.Employee, RoleType.Developer));
    options.AddPolicy(RoleType.User, policy =>
        policy.RequireRole(RoleType.User, RoleType.External, RoleType.Employee, RoleType.Developer));
});

// Managers
builder.Services.AddTransient<IProductManager, ProductManager>();
builder.Services.AddTransient<ICartManager, CartManager>();
builder.Services.AddTransient<IOrderManager, OrderManager>();
builder.Services.AddTransient<IUploadManager, UploadManager>();
builder.Services.AddTransient<IStatisticsManager, StatisticsManager>();    
builder.Services.AddTransient<IClaimsManager, ClaimsManager>();    
builder.Services.AddTransient<ICategoriesManager, CategoriesManager>();    

// Data Managers
builder.Services.AddTransient<IProductDataManager, ProductDataManager>();
builder.Services.AddTransient<ICartDataManager, CartDataManager>();
builder.Services.AddTransient<IOrderDataManager, OrderDataManager>();
builder.Services.AddTransient<IImageDataManager, ImageDataManager>(); 
builder.Services.AddTransient<IStatisticsDataManager, StatisticsDataManager>();    
builder.Services.AddTransient<ICategoriesDataManager, CategoriesDataManager>();    

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(CorsPolicy);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();