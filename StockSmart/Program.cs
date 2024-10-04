var builder = WebApplication.CreateBuilder(args);

// Añadir servicios al contenedor
builder.Services.AddControllersWithViews();

//Obtener la API Key desde las variables de entorno
string apiKey = Environment.GetEnvironmentVariable("APIKEY");

if (string.IsNullOrEmpty(apiKey))
{
    throw new Exception("API Key no se encuentra en las variables de entorno");
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

// Configurar el pipeline de las peticiones HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
