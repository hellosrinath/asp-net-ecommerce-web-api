using Microsoft.AspNetCore.Mvc;

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

//    Create => Create a category => POST: /api/v1/categories
app.MapPost("/api/v1/categories", ([FromBody] Category categoryData) => 
{
   // Console.WriteLine($"{categoryData}");
    var newCategory = new Category{
        CategoryId = Guid.NewGuid(),
        Name = categoryData.Name,
        Description = categoryData.Description,
        CreatedAt = DateTime.UtcNow,
    };
    categories.Add(newCategory);
    return Results.Created($"/api/v1/categories/{newCategory.CategoryId}",newCategory);
});


 ///Delete => Delete a category => Delete: /api/v1/categories/{categoryId}
app.MapDelete("/api/v1/categories/{categoryId}", (Guid categoryId) =>
{
    var foundCategory = categories.FirstOrDefault(c => c.CategoryId 
    == categoryId);

    if (foundCategory == null){
        return Results.NotFound("Category with this id does not exits");
    }
    
    categories.Remove(foundCategory);

    return Results.NoContent();

});


//Update => Update a category => POST:/api/v1/categories{categoryId}
app.MapPut("/api/v1/categories/{categoryId}", (Guid categoryId, [FromBody] Category categoryData) =>
{
    var foundCategory = categories.FirstOrDefault(c => c.CategoryId 
    == categoryId);

    if (foundCategory == null){
        return Results.NotFound("Category with this id does not exits");
    }
    
    foundCategory.Name = categoryData.Name;
    foundCategory.Description = categoryData.Description;
    
    return Results.NoContent();

});

//    Read   => Read a category => GET: /api/v1/categories
app.MapGet("/api/v1/categories", (string searchValue = "") =>
{
    System.Console.WriteLine(searchValue);

     var searchCategory = categories.Where(c => !string.IsNullOrEmpty(c.Name) && 
     c.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();

    return Results.Ok(searchCategory);
});



app.Run();

public record Category {
    public Guid CategoryId {get; set;}
    public required string Name {get; set;}
    public required string Description {get; set;}
    public DateTime CreatedAt {get; set;}
};

/*
    CRUD

    Create => Create a category => POST: /api/v1/categories
    Read   => Read a category => GET: /api/v1/categories
    Update => Update a category => POST:/api/v1/categories
    Delete => Delete a category => Delete: /api/v1/categories
*/