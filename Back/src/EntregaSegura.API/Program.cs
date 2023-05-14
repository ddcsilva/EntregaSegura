using EntregaSegura.API.Extensions;
using EntregaSegura.Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EntregaSeguraContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;

});

builder.Services.AddCors(options =>
{
    options.AddPolicy("Development",
        builder =>
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

    options.AddPolicy("Production",
        builder =>
            builder
                .WithMethods("GET")
                .WithOrigins("http://localhost:5000")
                .SetIsOriginAllowedToAllowWildcardSubdomains()
                .AllowAnyHeader());
});

builder.Services.ResolveDependencies();

builder.Services.AddAutoMapper(typeof(Program));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Development");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
