﻿namespace WebApp.Domain.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ProductsCategory> ProductsCategories { get; set; }
    }
}
