using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjection.Models
{
    public class SimpleRepository
    {
        private Dictionary<string, Product> products = new Dictionary<string, Product>();
        private static SimpleRepository sharedRepository = new SimpleRepository();

        public static SimpleRepository SharedRepositoy => sharedRepository;
        public SimpleRepository()
        {
            
            var initialItems = new[]
            {
                new Product { Name = "Kayak", Price = 275M },
                new Product { Name = "Lifejacket", Price = 48.95M },
                new Product { Name = "Soccer ball", Price = 19.50M },
                new Product { Name = "Corner flag", Price = 34.95M }
            };
            foreach(var item in initialItems)
            {
                AddProduct(item);
            }
            //products.Add("error", null);
        }
        public IEnumerable<Product> Products => products.Values;
        public void AddProduct(Product pro) => products.Add(pro.Name, pro);
    }
}
