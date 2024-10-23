namespace ProductsAPI.DTO
{
    public class ProductDTO
    {
        // tum bilgileri gondermek istiyorum fakat categoryID gondermek istemiyorum
        public int ProductID { get; set; }
        public string ProductName { get; set; } = null!;
        public string QuantityPerUnit { get; set; } = null!;
        public decimal UnitPrice { get; set; }
    }
}