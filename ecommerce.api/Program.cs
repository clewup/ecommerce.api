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

// Configuration
builder.Services.Configure<FreeImageHostConfig>(builder.Configuration.GetSection("FreeImageHostConfig"));
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