using EntregaSegura.API.Extensions;
using Microsoft.AspNetCore.Mvc;
using EntregaSegura.Infra.IoC;
using EntregaSegura.Application.Mappings;
using System.Text.Json.Serialization;
using EntregaSegura.Domain.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddContexts(builder.Configuration);
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddIdentityConfiguration(builder.Configuration);

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
builder.Services.AddAutoMapper(typeof(AutoMapperConfiguration));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Development");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();