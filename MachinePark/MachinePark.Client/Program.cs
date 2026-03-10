using MachinePark.Client.Services;
using MachinePark.State;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace MachinePark.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.Services.AddSingleton<MachineState>();

            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            });
            builder.Services.AddScoped<MachineApi>();

            await builder.Build().RunAsync();
        }
    }
}
