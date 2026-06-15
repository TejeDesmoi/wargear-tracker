using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WargearTracker.Web;
using WargearTracker.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:8080/") });

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ArmyService>();
builder.Services.AddScoped<MiniaturesService>();

await builder.Build().RunAsync();
