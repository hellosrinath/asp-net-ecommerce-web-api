using asp_net_ecommerce_web_api.controllers;
using asp_net_ecommerce_web_api.Interface;
using asp_net_ecommerce_web_api.services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<ICategoryService, CategoryServices>();

builder.Services.AddControllers();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{

    options.InvalidModelStateResponseFactory = context =>
    {

        var errors = context.ModelState
        .Where(e => e.Value?.Errors.Count > 0)
        .SelectMany(e => e.Value?.Errors != null ? e.Value.Errors.Select(x => x.ErrorMessage) :
        new List<string>()).ToList();

        return new BadRequestObjectResult(ApiResponse<object>.ErrorResponse(
            errors, 400, "Validation Failed"
        ));
    };

});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// adding swagger middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/", () => "Api is working fine!");

app.MapControllers();

app.Run();
