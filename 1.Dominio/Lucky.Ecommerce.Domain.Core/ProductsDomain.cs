using Lucky.Ecommerce.Domain.Entity;
using Lucky.Ecommerce.Domain.Interface;
using Lucky.Ecommerce.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucky.Ecommerce.Domain.Core
{
    public class ProductsDomain:IProductsDomain
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsDomain(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }
        #region Métodos Asíncronos

        public async Task<bool> InsertAsync(Products products)
        {
            return await _productsRepository.InsertAsync(products);
        }
        public async Task<bool> UpdateAsync(Products products)
        {
            return await _productsRepository.UpdateAsync(products);
        }
        public async Task<bool> DeleteAsync(int productId)
        {
            return await _productsRepository.DeleteAsync(productId);
        }
        public async Task<Products> GetAsync(int productId)
        {
            return await _productsRepository.GetAsync(productId);
        }
        public async Task<IEnumerable<Products>> GetAllAsync()
        {
            return await _productsRepository.GetAllAsync();
        }
        #endregion
    }
}
