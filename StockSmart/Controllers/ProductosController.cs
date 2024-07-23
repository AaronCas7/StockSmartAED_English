//     [HttpPost("/nuevo")]
//     public async Task<IActionResult> Create(Producto product)
//     {
//         var result = await _apiService.CreateProduct(product);
//         return RedirectToAction("Index");
//     }

//     [HttpPut("{id}")]
//     public async Task<IActionResult> Edit(int id, Producto product)
//     {
//         var result = await _apiService.UpdateProduct(id.ToString(), product);
//         return RedirectToAction("Index");
//     }

//     [HttpDelete("{id}")]
//     public async Task<IActionResult> Delete(int id)
//     {
//         var result = await _apiService.DeleteProduct(id.ToString());
//         return RedirectToAction("Index");
//     }
// }



using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StockSmart.Models;
using StockSmart.Services;

namespace StockSmart.Controllers
{
    [Route("productos")]
    public class ProductosController : Controller
    {
        private readonly ApiService _apiService;

        public ProductosController()
        {
            _apiService = new ApiService();
        }

        [HttpGet]
        // GET: ProductosController
        public async Task<IActionResult> Index()
        {
            var products = await _apiService.GetProducts();
            var productsList = JsonConvert.DeserializeObject<List<Producto>>(products);
            return View(productsList);
        }

        
       [HttpGet("ficha/{id}")]
       // GET: ProductosController/ficha/5
       public async Task<IActionResult> Ficha(int id)
        {
            var product = await _apiService.GetProduct(id);
            var productList = JsonConvert.DeserializeObject<List<Producto>>(product);
            var productDetails = productList.FirstOrDefault();
            return View(productDetails);
        }
        
        // POST: ProductosController/ficha/5
        [HttpPost]
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


        // GET: ProductosController/Nuevo
        public ActionResult Nuevo()
        {
            return View("Ficha", new Producto());
        }

        // POST: ProductosController/Nuevo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Nuevo(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Ficha", new Producto());
            }
        }
    }
}
