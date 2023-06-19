using System.Linq.Expressions;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.Extensions;

namespace Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateOneProduct(Product product)=>Create(product);

        public void DeleteOneProduct(Product product)=>Remove(product);

        public IQueryable<Product> GetAllProducts(bool trackChanges)=> FindAll(trackChanges);

        public IQueryable<Product> GetAllProductsWitDetails(ProductRequestParameters p)
        {
            return _context
                  .Products
                  .FilteredByCategoryID(p.CategoryID)
                  .FilteredBySearchTerm(p.SearchTerm)
                  .FilteredByPrice(p.MinPrice,p.MaxPrice,p.IsValidPrice)
                  .ToPaginate(p.PageNumber,p.PageSize);
        }  

        public Product? GetOneProduct(int id, bool trackChanges)
        {
            return FindByCondition(x=>x.ProductID.Equals(id),trackChanges);
        }

        public IQueryable<Product> GetShowcaseProducts(bool trackChanges)
        {
            return FindAll(trackChanges).Where(x=>x.ShowCase.Equals(true));
        }

        public void UpdateOneProduct(Product product)=>Update(product);
        
    }
}