using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StockSmart.Models;
using System.Text;

namespace StockSmart.Controllers
{
    public class ProductosController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductosController()
        {
            // Configura HttpClient con BaseAddress
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5000/")
            };
        }

        [HttpGet]
        // GET: productos
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("productos"); 
            var products = await response.Content.ReadAsStringAsync();
            var productsList = JsonConvert.DeserializeObject<List<Producto>>(products);
            return View(productsList);

        }


        [HttpGet]
        // GET: ProductosController/ficha/5
        public async Task<IActionResult> Ficha(int id)
        {
            var response = await _httpClient.GetAsync($"productos/ficha/{id}"); 
            var products = await response.Content.ReadAsStringAsync();
            var productList = JsonConvert.DeserializeObject<List<Producto>>(products);
            var productDetails = productList.FirstOrDefault();
            return View(productDetails);
        }


        // PUT: ProductosController/Ficha/5
        [HttpPut]
        [ValidateAntiForgeryToken]
        public ActionResult Ficha(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(new Producto());
            }
        }

        
        [HttpGet]
        // GET: ProductosController/Nuevo
        public ActionResult Nuevo()
        {
            return View("Ficha", new Producto());
        }

     
        // POST: ProductosController/Nuevo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Nuevo(IFormCollection collection)
        {
            try
            {
                // Crear un objeto Producto a partir de la colección de formulario
                var producto = new Producto
                {
                    ProductName = collection["ProductName"],
                    SupplierID = int.Parse(collection["SupplierID"]),
                    CategoryID = int.Parse(collection["CategoryID"]),
                    QuantityPerUnit = collection["QuantityPerUnit"],
                    UnitPrice = decimal.Parse(collection["UnitPrice"]),
                    UnitsInStock = short.Parse(collection["UnitsInStock"]),
                    UnitsOnOrder = short.Parse(collection["UnitsOnOrder"]),
                    ReorderLevel = short.Parse(collection["ReorderLevel"]),
                    Discontinued = int.Parse(collection["Discontinued"])
                };

                // Serializar el objeto Producto a JSON
                var json = JsonConvert.SerializeObject(producto);

                // Enviar la solicitud POST al servidor
                var response = await _httpClient.PostAsync("productos", new StringContent(json, Encoding.UTF8, "application/json"));

                // Verificar si la solicitud se realizó correctamente
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Manejar el error
                    return View("Ficha", producto);
                }
            }
            catch
            {
                // Manejar la excepción
                return View("Ficha", new Producto());
            }
        }
    }
}
