using AutoMapper;
using Entities.DTOs.Product;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class ProductManager : IProductService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public ProductManager(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public void CreateProduct(ProductDTOForInsertion productDto)
        {
            Product product=_mapper.Map<Product>(productDto);
            
            _manager.Product.Create(product);
            _manager.Save();
        }

        public void DeleteOneProduct(int id)
        {
            Product product=GetOneProduct(id,false);
            if(product is not null)
            {
                _manager.Product.Remove(product);
                _manager.Save();
            }
        }

        public IEnumerable<Product> GetAllProducts(bool trackChanges)
        {
            return _manager.Product.GetAllProducts(trackChanges);
        }

        public IQueryable<Product> GetAllProductsWitDetails(ProductRequestParameters p)
        {
            return _manager.Product.GetAllProductsWitDetails(p);
        }

        public IEnumerable<Product> GetLastestProducts(int n, bool trackChanges)
        {
            return _manager
                .Product
                .FindAll(trackChanges)
                .OrderByDescending(prd => prd.ProductID)
                .Take(n);
        }

        public Product? GetOneProduct(int id, bool trackChanges)
        {
            var product=_manager.Product.GetOneProduct(id,trackChanges);
            if(product is null)
                   throw new Exception("Not Found");
            return product;

        }

        public ProductDTOForUpdate GetOneProductForUpdate(int id, bool trackChanges)
        {
            var product=GetOneProduct(id,false);
            var productDto=_mapper.Map<ProductDTOForUpdate>(product);
            return productDto;
        }

        public IEnumerable<Product> GetShowcaseProducts(bool trackChanges)
        {
            var products=_manager.Product.GetShowcaseProducts(trackChanges);
            return products;
        }

        public void UpdateOneProduct(ProductDTOForUpdate productDto)
        {
            
            var entity=_mapper.Map<Product>(productDto);
            _manager.Product.UpdateOneProduct(entity);
            _manager.Save();
        }
    }
}