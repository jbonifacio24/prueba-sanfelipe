using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MS.Producto.Domain.Interfaces;
using MS.Producto.Application.UseCases;
using MS.Producto.Domain.Services;
using MS.Producto.Infrastucture.Repositories;
using System.Text;
using MS.Producto.Application.Facades;
using MS.Producto.Infrastucture.Decorators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICreateProductRepository, CreateProductRepository>();

builder.Services.AddScoped<CreateProductService>();
builder.Services.AddScoped<GetAllProductsService>();
builder.Services.AddScoped<CreateProductHandler>();
builder.Services.AddScoped<GetAllProductsHandler>();

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddScoped<ProductFacade>();

builder.Services.AddScoped<ICreateProductRepository>(sp =>
    new LoggingProductRepositoryDecorator(
        new CreateProductRepository(sp.GetRequiredService<IConfiguration>()),
        sp.GetRequiredService<ILogger<LoggingProductRepositoryDecorator>>()
    )
);

builder.Services.AddScoped<IGetAllProductsRepository>(sp =>
    new LoggingGetAllProductsRepositoryDecorator(
        new GetAllProductsRepository(sp.GetRequiredService<IConfiguration>()),
        sp.GetRequiredService<ILogger<LoggingGetAllProductsRepositoryDecorator>>()
    )
);

builder.Services.AddScoped<ICreateProductRepository>(sp =>
{
    var inner = new CreateProductRepository(sp.GetRequiredService<IConfiguration>());
    var logger = sp.GetRequiredService<ILogger<LoggingProductRepositoryDecorator>>();
    return new LoggingProductRepositoryDecorator(inner, logger);
});

builder.Services.AddScoped<IGetAllProductsRepository>(sp =>
{
    var inner = new GetAllProductsRepository(sp.GetRequiredService<IConfiguration>());
    var logger = sp.GetRequiredService<ILogger<LoggingGetAllProductsRepositoryDecorator>>();
    return new LoggingGetAllProductsRepositoryDecorator(inner, logger);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services
.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
        )
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("FrontendPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
