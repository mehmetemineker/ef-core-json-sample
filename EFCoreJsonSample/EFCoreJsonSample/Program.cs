using EFCoreJsonSample;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/products", async (AppDbContext db) =>
{
    //var products = await db.Products
    //    .Where(p => EF.Functions.JsonContains(p.Details, new { Manufacturer = "Sample" }))
    //    .ToListAsync();

    var products = await db.Products
        .Where(p => p.Details.Manufacturer == "Sample")
        .ToListAsync();

    return Results.Ok(products);
});

app.UseHttpsRedirection();

app.Run();