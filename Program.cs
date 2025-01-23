var builder = WebApplication.CreateBuilder(args);

// adding swaggerApi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// adding swagger middleware
if (app.Environment.IsDevelopment()){
    app.UseSwagger();
    app.UseSwaggerUI();
}    

app.UseHttpsRedirection();

// REST API: => GET, POST, PUT, DELETE

app.MapGet("/", () =>  "Api is working fine");

app.MapGet("/hello", () => "Get Method: Hello");

// return list of products
var products = new List<Product>(){
    new Product("Samsung s20", 44000),
    new Product("iPhone 14", 150000),
    new Product("Mac Mini", 120000)
};

app.MapGet("/products", () => Results.Ok(products));

// return html
app.MapGet("/html", () =>  Results.Content("<h1>Hello, HTML</h1>","text/html"));

app.MapPost("/hello", () => {
    // return json object
    var response = new {message = "Data successfully updated", success = true};
    return Results.Ok(response);
});

app.MapPut("/hello", () => {
    return Results.Accepted();  
});

app.MapDelete("/hello", () => {
    return Results.NoContent();
});

app.Run();

public record Product(string ProductName, decimal Price); 
