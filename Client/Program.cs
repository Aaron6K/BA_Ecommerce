global using System.Net.Http.Json;
global using BA_Ecommerce.Shared;
global using BA_Ecommerce.Client.Services.ProductService;
global using BA_Ecommerce.Client.Services.CategoryService;

using BA_Ecommerce.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using BA_Ecommerce.Client.Services.CartService;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICartservice, CartService>();
await builder.Build().RunAsync();
