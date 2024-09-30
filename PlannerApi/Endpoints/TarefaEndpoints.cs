using Microsoft.EntityFrameworkCore;

public static class TarefaEndpoints
{
    public static void MapTarefaEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/tarefa", async (AppDbContext dbContext) => await dbContext.Tarefas.ToListAsync());

        routes.MapGet("/tarefa/{id}", async (int id, AppDbContext dbContext) => await dbContext.Tarefas.FindAsync(id)
            is Tarefa tarefa
            ? Results.Ok(tarefa)
            : Results.NotFound());

        routes.MapPost("/tarefa", async (Tarefa tarefa, AppDbContext dbContext) =>
        {
            dbContext.Tarefas.Add(tarefa);
            await dbContext.SaveChangesAsync();
            return Results.Created($"/tarefa/{tarefa.Id}", tarefa);
        });

        routes.MapPut("/tarefa/{id}", async (int id, Tarefa tarefaAtualizada, AppDbContext dbContext) =>
        {
            var tarefa = await dbContext.Tarefas.FindAsync(id);
            if (tarefa is null) return Results.NotFound();

            tarefa.Desc = tarefaAtualizada.Desc;
            tarefa.Concluida = tarefaAtualizada.Concluida;

            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        });

        routes.MapDelete("/tarefa/{id}", async (int id, AppDbContext dbContext) =>
        {
            if (await dbContext.Tarefas.FindAsync(id) is Tarefa tarefa)
            {
                dbContext.Remove(tarefa);
                await dbContext.SaveChangesAsync();
                return Results.NoContent();
            }
            return Results.NotFound();
        });
    }
}