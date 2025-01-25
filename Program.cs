using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// adding swaggerApi
builder.Services.AddControllers();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{

    options.InvalidModelStateResponseFactory = context =>
    {

        var errors = context.ModelState
        .Where(e => e.Value!.Errors.Count > 0)
        .Select(e => new
        {
            Field = e.Key,
            Errors = e.Value!.Errors.Select(
                x => x.ErrorMessage
            ).ToArray()
        }).ToList();

        return new BadRequestObjectResult(new
        {
            Message = "Validation Faied",
            Errors = errors
        });
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
