using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using AutoMapper;
using CloudinaryDotNet;
using ecommerce.api.Data;
using ecommerce.api.Infrastructure;
using ecommerce.api.Managers;
using ecommerce.api.Managers.Data;
using ecommerce.api.Services.Mappers;
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
// Auto Mapper
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new OrderMapper());
    mc.AddProfile(new ProductMapper());
    mc.AddProfile(new CartMapper());
    mc.AddProfile(new ImageMapper());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// Managers
builder.Services.AddTransient<AuthManager>();
builder.Services.AddTransient<ProductManager>();
builder.Services.AddTransient<ProductDataManager>();
builder.Services.AddTransient<CartManager>();
builder.Services.AddTransient<CartDataManager>();
builder.Services.AddTransient<OrderManager>();
builder.Services.AddTransient<OrderDataManager>();
builder.Services.AddTransient<UploadManager>();
builder.Services.AddTransient<ImageDataManager>();    
builder.Services.AddTransient<StatisticsManager>();    
builder.Services.AddTransient<StatisticsDataManager>();    
builder.Services.AddTransient<ClaimsManager>();    

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