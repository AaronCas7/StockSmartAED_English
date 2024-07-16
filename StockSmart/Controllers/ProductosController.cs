using Microsoft.AspNetCore.Mvc;
using StockSmart.Services;
using Newtonsoft.Json;
using StockSmart.Models;


public class ProductsController : Controller
{
    private readonly ApiService _apiService;

    public ProductsController()
    {
        _apiService = new ApiService();
    }

    public async Task<IActionResult> Index()
    {
        var products = await _apiService.GetProducts();
        // Aquí puedes deserializar el JSON y pasarlo a la vista
        var productsList = JsonConvert.DeserializeObject<List<Producto>>(products);
        return View(productsList);
    }

    public async Task<IActionResult> Details(int id)
    {
        var product = await _apiService.GetProduct(id);
        var productDetails = JsonConvert.DeserializeObject<Producto>(product);
        return View(productDetails);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Producto product)
    {
        var result = await _apiService.CreateProduct(product);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Producto product)
    {
        var result = await _apiService.UpdateProduct(id.ToString(), product);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _apiService.DeleteProduct(id.ToString());
        return RedirectToAction("Index");
    }
}
