// using System;
// using System.Net.Http;
// using System.Threading.Tasks;
// using Newtonsoft.Json;

// namespace StockSmart.Services
// {
//     public class ApiService
//     {
//         private readonly HttpClient _httpClient;

//         public ApiService()
//         {
//             _httpClient = new HttpClient();
//             _httpClient.BaseAddress = new Uri("http://localhost:5000/");
//         }

//         public async Task<string> GetProducts()
//         {
//             var response = await _httpClient.GetAsync("productos");
//             // response.EnsureSuccessStatusCode();
//             var content = await response.Content.ReadAsStringAsync();

//             return content;
//         }

//         public async Task<string> GetProduct(int id)
//         {
//             try
//             {
//                 var response = await _httpClient.GetAsync($"productos/ficha/{id}");
//                 // response.EnsureSuccessStatusCode();
//                 var content = await response.Content.ReadAsStringAsync();

//                 return content;
//             }
//             catch (HttpRequestException ex)
//             {
//                 Console.WriteLine($"Error getting product: {ex.Message}");
//                 throw;
//             }
//         }
        
//         public async Task<string> CreateProduct(object product)
//         {
//             var json = JsonConvert.SerializeObject(product);
//             var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
//             var response = await _httpClient.PostAsync("productos", content);
//             // response.EnsureSuccessStatusCode();
//             return await response.Content.ReadAsStringAsync();
//         }

//         public async Task<string> UpdateProduct(string id, object product)
//         {
//             var json = JsonConvert.SerializeObject(product);
//             var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
//             var response = await _httpClient.PutAsync($"productos/{id}", content);
//             // response.EnsureSuccessStatusCode();
//             return await response.Content.ReadAsStringAsync();
//         }

//         public async Task<string> DeleteProduct(string id)
//         {
//             var response = await _httpClient.DeleteAsync($"productos/{id}");
//             // response.EnsureSuccessStatusCode();
//             return await response.Content.ReadAsStringAsync();
//         }
//     }
// }
