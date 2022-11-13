using Microsoft.EntityFrameworkCore;
using Model.DBModel;

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

builder.Services.AddDbContext<DemoApiContext>(options =>
    options.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=DemoApi;Trusted_Connection=True"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
