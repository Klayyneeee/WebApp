namespace WebApp.Domain.Models
{
    public class ProductsCategory
    {
        public int ProductId { get; set; } 
        public int CategoryId { get; set; }
        public Products Products { get; set; }
        public Category Category { get; set; }
    }
}