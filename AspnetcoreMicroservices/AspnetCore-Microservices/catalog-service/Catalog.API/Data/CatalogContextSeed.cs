using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(_ => true).Any();
            if (!existProduct)
            {
                productCollection.InsertManyAsync(getPreconfiguredProducts());
            }
        }

        private static IEnumerable<Product> getPreconfiguredProducts()
        {
            return new List<Product>
            {
                new Product("product 1", "category 1", "abc", "def", "url1", 100),
                new Product("product 1", "category 1", "abc", "def", "url1", 100),
                new Product("product 1", "category 1", "abc", "def", "url1", 100)
            };
        }
    }
}
