using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using vui_demo_api.Models;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "allowOrigins",
        policy =>
        {
            policy
                .WithOrigins("*")
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("dbConnection"))
);

builder.Services.AddSwaggerGen((c) =>
{
    c.EnableAnnotations();
});

WebApplication? app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("allowOrigins");
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/table",
        context => context.Response.WriteAsync("table"))
        .RequireCors("allowOrigins");

    endpoints.MapControllers()
             .RequireCors("allowOrigins");
});

//app.MapControllers();

app.Run();
