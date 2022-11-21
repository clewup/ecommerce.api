using ecommerce.api.Managers;

var builder = WebApplication.CreateBuilder(args);

var DefaultCorsPolicy = "DefaultCorsPolicy";

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: DefaultCorsPolicy,
        policy =>
        {
            policy.WithOrigins("http://localhost:3000",
                    "https://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// Managers
builder.Services.AddSingleton<ProductManager>();
builder.Services.AddSingleton<OrderManager>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("DefaultCorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();