using Entities.DTOs.Product;
using Entities.Models;

namespace Services.Contracts
{
    public interface IProductService
    {
       IEnumerable<Product> GetAllProducts(bool trackChanges);
       IEnumerable<Product> GetShowcaseProducts(bool trackChanges);
       IQueryable<Product> GetAllProductsWitDetails(ProductRequestParameters p);
       IEnumerable<Product> GetLastestProducts(int n, bool trackChanges);
       Product? GetOneProduct(int id, bool trackChanges);
       void CreateProduct(ProductDTOForInsertion productDto);
       void UpdateOneProduct(ProductDTOForUpdate product);
       void DeleteOneProduct(int id);
       ProductDTOForUpdate GetOneProductForUpdate(int id, bool trackChanges);
    }
}