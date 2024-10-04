using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Obtener la API Key desde las variables de entorno
string apiKey = Environment.GetEnvironmentVariable("APIKEY");

if (string.IsNullOrEmpty(apiKey))
{
    throw new Exception("API Key not found in environment variables");
}

builder.Services.AddHttpClient("Default");

builder.Services.AddHttpClient(
    "APIApp",
    conf =>
    {
        conf.BaseAddress = new Uri("https://apistocksmartaed.azurewebsites.net");
        conf.DefaultRequestHeaders.Add("Authorization", apiKey);
    }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
