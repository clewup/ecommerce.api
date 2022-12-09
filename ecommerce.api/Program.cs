using System.Text;
using AutoMapper;
using CloudinaryDotNet;
using ecommerce.api.Data;
using ecommerce.api.Managers;
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
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<EcommerceDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("EcommerceConnection")));

// Cloudinary
var cloudName = builder.Configuration.GetValue<string>("CloudinaryConfig:CloudName");
var apiKey = builder.Configuration.GetValue<string>("CloudinaryConfig:ApiKey");
var apiSecret = builder.Configuration.GetValue<string>("CloudinaryConfig:ApiSecret");

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
                    "https://localhost:3000")
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
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
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