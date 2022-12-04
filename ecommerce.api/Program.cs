using CloudinaryDotNet;
using ecommerce.api.Data;
using ecommerce.api.Managers;

var  CorsPolicy = "_corsPolicy";

var builder = WebApplication.CreateBuilder(args);

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

// Configuration
builder.Services.AddSingleton<EcommerceDbContext>();

// Managers
builder.Services.AddSingleton<AuthManager>();
builder.Services.AddSingleton<DiscountManager>();
builder.Services.AddSingleton<CartItemManager>();
builder.Services.AddSingleton<ProductManager>();
builder.Services.AddSingleton<CartManager>();
builder.Services.AddSingleton<OrderManager>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(CorsPolicy);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();