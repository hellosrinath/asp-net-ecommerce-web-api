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
List<Category> categories = new List<Category>();

app.MapGet("/", () =>  "Api is working fine");

app.MapPost("/api/v1/categories", () => 
{
    var newCategory = new Category{
        CategoryId = Guid.Parse("df5a7127-a044-49c2-b977-f47ad18c52fb"),
        Name = "Smart Phones",
        Description = "Smart Phone is a nice categories",
        CreatedAt = DateTime.UtcNow,
    };
    categories.Add(newCategory);
    return Results.Created($"/api/v1/categories/{newCategory.CategoryId}",newCategory);
});


app.MapDelete("/api/v1/categories", () =>
{
    var foundCategory = categories.FirstOrDefault(c => c.CategoryId 
    == Guid.Parse("df5a7127-a044-49c2-b977-f47ad18c52fb"));

    if (foundCategory == null){
        return Results.NotFound("Category with this id does not exits");
    }
    
    categories.Remove(foundCategory);

    return Results.NoContent();

});

app.MapPut("/api/v1/categories", () =>
{
    var foundCategory = categories.FirstOrDefault(c => c.CategoryId 
    == Guid.Parse("df5a7127-a044-49c2-b977-f47ad18c52fb"));

    if (foundCategory == null){
        return Results.NotFound("Category with this id does not exits");
    }
    
    foundCategory.Name = "Electronics";
    foundCategory.Description = "This is good categories";
    
    return Results.NoContent();

});


app.MapGet("/api/v1/categories", () =>
{

    return Results.Ok(categories);

});



app.Run();

public record Category {
    public Guid CategoryId {get; set;}
    public string? Name {get; set;}
    public string? Description {get; set;}
    public DateTime CreatedAt {get; set;}
};

/*
    CRUD

    Create => Create a category => POST: /api/v1/categories
    Read   => Read a category => GET: /api/v1/categories
    Update => Update a category => POST:/api/v1/categories
    Delete => Delete a category => Delete: /api/v1/categories
*/