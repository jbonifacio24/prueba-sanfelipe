using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MS.Venta.Application.Facades;
using MS.Venta.Application.UseCases;
using MS.Venta.Domain.Interfaces;
using MS.Venta.Domain.Services;
using MS.Venta.Infrastructure.Data;
using MS.Venta.Infrastructure.Decorators;
using MS.Venta.Infrastructure.Repositories;
using System.Text;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICreateSaleRepositoryEF, CreateSaleRepositoryEF>();
builder.Services.AddScoped<CreateSaleService>();
builder.Services.AddScoped<CreateSaleHandler>();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddScoped<SaleFacade>();
builder.Services.AddDbContext<SaleDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ICreateSaleRepositoryEF>(sp =>
    new LoggingSaleRepositoryDecorator(
        new CreateSaleRepositoryEF(sp.GetRequiredService<SaleDbContext>()),
        sp.GetRequiredService<ILogger<LoggingSaleRepositoryDecorator>>()
    )
);
builder.Services.AddScoped<ICreateSaleRepositoryEF>(sp =>
{
    var inner = new CreateSaleRepositoryEF(sp.GetRequiredService<SaleDbContext>());
    var logger = sp.GetRequiredService<ILogger<LoggingSaleRepositoryDecorator>>();
    return new LoggingSaleRepositoryDecorator(inner, logger);
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
