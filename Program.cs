var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseHttpsRedirection();

// REST API: => GET, POST, PUT, DELETE

app.MapGet("/", () => {
    return "Api is working fine";
});

app.MapGet("/hello", () => {
    return "Get Method: Hello";
});

app.MapPost("/hello", () => {
    return "Post Method: Hello";
});

app.MapPut("/hello", () => {
    return "Put Method: Hello";
});

app.MapDelete("/hello", () => {
    return "Delete Method: Hello";
});

app.Run();
