using DemoWebApi.Models;

namespace DemoWebApi.Services;

public interface IMyDataService
{
    IEnumerator<Product> GetProducts();

    IEnumerable<Product> GetAllProducts();

    void AddProduct(Product product);
}
