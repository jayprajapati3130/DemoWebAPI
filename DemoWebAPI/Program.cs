using DemoWebAPI.CustomMiddlewares;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Model.DBModel;
using Model.ModelValidator;
using Model.RequestModel;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "My API",
            Version = "v1"
        }
        );
    var filePath = Path.Combine(System.AppContext.BaseDirectory, "WebApi.xml");

    c.IncludeXmlComments(filePath);
});

//JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

//builder.Services.AddFluentValidation(s =>
//{
//   // s.RegisterValidatorsFromAssembly(typeof(Program).Assembly);
//   // s.AutomaticValidationEnabled = false;

//    s.RegisterValidatorsFromAssemblyContaining<Program>();
//    s.AutomaticValidationEnabled = false;
//});

//builder.Services.AddScoped<IValidator<EmployeeCreate>, EmployeeCreateValidator>();
//builder.Services.AddValidatorsFromAssemblyContaining<EmployeeCreateValidator>();

builder.Services.AddControllers().AddFluentValidation();
builder.Services.AddScoped<IValidator<EmployeeCreate>, EmployeeValidator>();


builder.Services.AddDbContext<DemoApiContext>(options =>
    options.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=DemoApi;Trusted_Connection=True"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseMiddleware<ExceptionHandlingMiddleware>();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
