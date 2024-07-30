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

    }
}
