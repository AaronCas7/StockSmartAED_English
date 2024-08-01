using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StockSmart.Models;

namespace StockSmart.Controllers
{
    public class ProductosController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductosController(IHttpClientFactory http)
        {
            _httpClient = http.CreateClient("APIApp");
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
            ViewBag.id = id;
            return View(productDetails);
        }

        // PUT: ProductosController/Ficha/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Ficha(int id, Producto updatedProduct)
        {
            try
            {
                // Serializamos el objeto Producto actualizado a JSON
                var json = JsonConvert.SerializeObject(updatedProduct);

                // Enviamos la solicitud PUT al servidor
                var response = await _httpClient.PutAsync(
                    $"productos/{id}",
                    new StringContent(json, Encoding.UTF8, "application/json")
                );

                // Verificamos si la solicitud se realizó correctamente
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Si hubo un error, retornamos a la vista Ficha con los datos del producto actualizado
                    return View("Ficha", updatedProduct);
                }
            }
            catch
            {
                // Manejo de errores
                return View("Ficha", new Producto());
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
        public async Task<ActionResult> Nuevo(Producto product)
        {
            try
            {
                // Serializamos el objeto Producto a JSON
                product.id = Guid.NewGuid().ToString();
                var json = JsonConvert.SerializeObject(product);

                // Enviamos la solicitud POST al servidor
                var response = await _httpClient.PostAsync(
                    "productos/nuevo",
                    new StringContent(json, Encoding.UTF8, "application/json")
                );

                // Verificamos si la solicitud se realizó correctamente
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View("Ficha", product);
                }
            }
            catch (Exception)
            {
                return View("Ficha", new Producto());
            }
        }

        // DELETE: ProductosController/Eliminar/34
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                // Enviamos la solicitud DELETE al servidor
                var response = await _httpClient.DeleteAsync($"productos/{id}");

                // Verificamos si la solicitud se realizó correctamente
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Aquí puedes añadir un manejo de errores más detallado si es necesario
                    TempData["Error"] = "No se pudo eliminar el producto.";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {
                // Manejo de excepciones
                TempData["Error"] = "Se produjo un error al intentar eliminar el producto.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
