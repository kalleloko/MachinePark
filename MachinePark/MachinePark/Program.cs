using MachinePark.Client.Services;
using MachinePark.Components;
using MachinePark.Data;
using MachinePark.Shared.Models;
using MachinePark.State;
using System.Net.Http.Headers;

namespace MachinePark;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveWebAssemblyComponents();

        builder.Services.AddSingleton<MachineRepository>();
        builder.Services.AddSingleton<MachineState>();
        builder.Services.AddHttpClient<MachineApi>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:7043");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
        app.UseHttpsRedirection();

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);




        app.MapGet("/api/machines", (MachineRepository repo) =>
        {
            return repo.GetMachines();
        });

        app.MapPost("/api/machines", (Machine machine, MachineRepository repo) =>
        {
            repo.AddMachine(machine);
            return Results.Ok();
        });

        app.MapPost("/api/machines/{id:guid}/start", (Guid id, MachineRepository repo) =>
        {
            repo.StartMachine(id);
            return Results.Ok();
        });

        app.MapPost("/api/machines/{id:guid}/stop", (Guid id, MachineRepository repo) =>
        {
            repo.StopMachine(id);
            return Results.Ok();
        });

        app.MapDelete("/api/machines/{id:guid}", (Guid id, MachineRepository repo) =>
        {
            repo.RemoveMachine(id);
            return Results.Ok();
        });

        app.MapDelete("/api/machines/{id:guid}/data", (Guid id, MachineRepository repo) =>
        {

            return Results.Ok();
        });

        app.Run();
    }
}
