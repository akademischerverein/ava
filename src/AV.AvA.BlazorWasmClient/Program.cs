using System.Text.Json;
using AV.AvA;
using AV.AvA.BlazorWasmClient;
using AV.AvA.BlazorWasmClient.Services;
using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddTransient<IPersonVersionAccessor, CachedPersonVersionAccessor>();
builder.Services.AddTransient(sp => AV.AvA.Common.Json.CreateSTJOptions());

builder.Services.AddMemoryCache();

builder.Services.AddMudServices();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddGrpcClient<PersonVersionRepository.PersonVersionRepositoryClient>(options =>
{
    options.Address = new Uri("https://localhost:7195");
})
.ConfigurePrimaryHttpMessageHandler(
    () => new GrpcWebHandler(new HttpClientHandler()));

await builder.Build().RunAsync();
