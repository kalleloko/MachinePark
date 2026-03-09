using MachinePark.Client.State;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace MachinePark.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddSingleton<MachineState>();

            await builder.Build().RunAsync();
        }
    }
}
