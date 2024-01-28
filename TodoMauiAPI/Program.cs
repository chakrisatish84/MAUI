using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoMauiAPI.Data;
using TodoMauiAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDBContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("sqliteConnectionString")));

var app = builder.Build();

//app.UseHttpsRedirection();

app.MapGet("api/todo", async (AppDBContext context) =>
{
    var items = await context.ToDos.ToListAsync();
    return Results.Ok(items);
});

app.MapPost("api/todo", async (AppDBContext context, [FromBody] Todo todo) =>
{
    await context.ToDos.AddAsync(todo);
    await context.SaveChangesAsync();
    return Results.Created($"api/todo/{todo.Id}", todo);
});

app.MapPut("api/todo/{id}", async (AppDBContext context, int id, [FromBody] Todo todo) =>
{
    var todoModel = await context.ToDos.FirstOrDefaultAsync(t => t.Id == id);
    if (todoModel == null)
    {
        return Results.NotFound();
    }
    todoModel.TodoName = todo.TodoName;

    await context.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("api/todo/{id}", async (AppDBContext context, int id) =>
{
    var todoModel = await context.ToDos.FirstOrDefaultAsync(t => t.Id == id);
    if (todoModel == null)
    {
        return Results.NotFound();
    }

    context.ToDos.Remove(todoModel);
    await context.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();



