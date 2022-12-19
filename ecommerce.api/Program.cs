using System.Text;
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

// Database
var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL") ?? "";
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<EcommerceDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString(connectionString)));

// Cloudinary
var cloudName = Environment.GetEnvironmentVariable("CLOUDINARY_NAME");
var apiKey = Environment.GetEnvironmentVariable("CLOUDINARY_KEY");
var apiSecret = Environment.GetEnvironmentVariable("CLOUDINARY_SECRET");

if (new[] { cloudName, apiKey, apiSecret }.Any(string.IsNullOrWhiteSpace))
{
    throw new ArgumentException("Please specify Cloudinary account details!");
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
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
        ValidAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY")!))
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