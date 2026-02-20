using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MS.Compra.Application.Facades;
using MS.Compra.Application.UseCases;
using MS.Compra.Domain.Interfaces;
using MS.Compra.Domain.Services;
using MS.Compra.Infrastructure.Decorators;
using MS.Compra.Infrastructure.Repositories;
using System.Text;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICreateOrderRepository, CreateOrderRepository>();

builder.Services.AddScoped<CreateOrderService>();
builder.Services.AddScoped<GetAllOrdersService>();
builder.Services.AddScoped<CreateOrderHandler>();
builder.Services.AddScoped<GetAllOrdersHandler>();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddScoped<OrderFacade>();

builder.Services.AddScoped<IGetAllOrdersRepository>(sp =>
    new LoggingGetAllOrdersRepositoryDecorator(
        new GetAllOrdersRepository(sp.GetRequiredService<IConfiguration>()),
        sp.GetRequiredService<ILogger<LoggingGetAllOrdersRepositoryDecorator>>()
    )
);

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
