using MachinePark.Client.Services;
using MachinePark.Components;
using MachinePark.Data;
using MachinePark.Hubs;
using MachinePark.Services;
using MachinePark.Shared.Models;
using MachinePark.State;
using Microsoft.AspNetCore.JsonPatch.SystemTextJson;
using Microsoft.AspNetCore.Mvc;
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
        builder.Services.AddSingleton<MachineService>();
        builder.Services.AddSignalR();
        builder.Services.AddHostedService<MachineSimulatorService>();

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



        app.MapHub<MachineHub>("/machinehub");


        app.MapGet("/api/machines", (MachineService ms) =>
        {
            return ms.GetMachines();
        });

        app.MapPost("/api/machines", async (Machine machine, MachineService ms) =>
        {
            await ms.AddMachine(machine);
            return Results.Ok();
        });

        app.MapPost("/api/machines/{id:guid}/start", async (Guid id, MachineService ms) =>
        {
            await ms.StartMachine(id);
            return Results.Ok();
        });

        app.MapPost("/api/machines/{id:guid}/stop", async (Guid id, MachineService ms) =>
        {
            await ms.StopMachine(id);
            return Results.Ok();
        });

        app.MapDelete("/api/machines/{id:guid}", async (Guid id, MachineService ms) =>
        {
            await ms.RemoveMachine(id);
            return Results.Ok();
        });

        app.MapPatch("/api/machines/{id:guid}", async (Guid id, [FromBody] JsonPatchDocument<Machine> patch, MachineService ms) =>
        {
            var response = await ms.UpdateMachine(id, patch);
            return Results.Ok(response);
        });

        app.Run();
    }
}
