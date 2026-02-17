using DemoWebApi.Models;

namespace DemoWebApi.Services;

public class MyDataService
    : IMyDataService
{

    private List<Product> products;

    public MyDataService()
    {
        products = new List<Product>();

        products.Add(new Product() { ProductId = 1, Name = "First Product", Price = 20M });
        products.Add(new Product() { ProductId = 2, Name = "Second Product", Price = 20.75M });
        products.Add(new Product() { ProductId = 3, Name = "Third Product", Price = 90.50M });
    }

    public IEnumerator<Product> GetProducts()
    {
        return products.GetEnumerator();
    }

    public IEnumerable<Product> GetAllProducts()
    {
        return products; 
    }

    public void AddProduct(Product newProduct)
    {
        products.Add(newProduct);
    }

}
