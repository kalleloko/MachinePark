
using MachinePark.Shared.Models;
using System.Net.Http.Json;

namespace MachinePark.Client.Services;

public class MachineApi(HttpClient http)
{
    public async Task<List<Machine>> GetMachinesAsync()
        => await http.GetFromJsonAsync<List<Machine>>("/api/machines") ?? [];

    public Task StartMachineAsync(Guid id)
        => http.PostAsync($"/api/machines/{id}/start", null);

    public Task StopMachineAsync(Guid id)
        => http.PostAsync($"/api/machines/{id}/stop", null);

    public Task RemoveMachineAsync(Guid id)
        => http.DeleteAsync($"/api/machines/{id}");

    public Task AddMachineAsync(Machine machine)
        => http.PostAsJsonAsync("/api/machines", machine);

    public Task UpdateMachineDataAsync(Machine machine)
        => http.PatchAsync("/api/machines/{machine.Id}/data", JsonContent.Create(machine.LastData));
}