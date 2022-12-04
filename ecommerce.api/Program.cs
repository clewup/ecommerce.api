using ecommerce.api.Data;
using ecommerce.api.Managers;
using Microsoft.EntityFrameworkCore;

var  CorsPolicy = "_corsPolicy";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
string ecommerceConnection = "Host=ec2-63-32-248-14.eu-west-1.compute.amazonaws.com;Database=detpeg9hipregd;Username=potzztwyoahhzh;Password=cbfa39381d0ec04e581a98d2e9b9b19c79003b9c6ddd125347617f7ef38383d6";
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<EcommerceDbContext>(opt =>
    opt.UseNpgsql(ecommerceConnection));

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

// Managers
builder.Services.AddTransient<AuthManager>();
builder.Services.AddTransient<DiscountManager>();
builder.Services.AddTransient<CartItemManager>();
builder.Services.AddTransient<ProductManager>();
builder.Services.AddTransient<CartManager>();
builder.Services.AddTransient<OrderManager>();

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