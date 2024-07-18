namespace WebApp.Domain.Models
{
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ProductsCategory> ProductsCategories { get; set; }
    }
}
