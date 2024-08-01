using System.ComponentModel.DataAnnotations;

namespace StockSmart.Models
{
    public class Producto
    {
        [Display(Name = "Referencia")]
        public string ProductID { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "La <b>Descripción</b> es un dato requerido.")]
        [MinLength(5, ErrorMessage = "Debe contener al menos 5 caracteres.")]
        public string ProductName { get; set; }

        [Display(Name = "Proveedor")]
        public string SupplierID { get; set; }

        [Display(Name = "Categoría")]
        public string CategoryID { get; set; }

        [Display(Name = "Cantidades por unidad")]
        public string QuantityPerUnit { get; set; }

        [Display(Name = "Precio")]
        public string UnitPrice { get; set; }

        [Display(Name = "Stock")]
        public string UnitsInStock { get; set; }

        [Display(Name = "Stock Pedido")]
        public string UnitsOnOrder { get; set; }

        [Display(Name = "Nivel")]
        public string ReorderLevel { get; set; }

        [Display(Name = "Descuentos")]
        public string Discontinued { get; set; }

        public string id { get; set; }
    }
}
