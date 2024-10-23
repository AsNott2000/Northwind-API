namespace ProductsAPI.Models;

public class Product{
    public int ProductID { get; set; } 
    public string ProductName { get; set; } = null!;
    public int CategoryID { get; set; }
    public string QuantityPerUnit { get; set; } = null!;
    public decimal UnitPrice { get; set; }

}