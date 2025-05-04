using Lucky.Ecommerce.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucky.Ecommerce.Domain.Interface
{
    public interface IProductsDomain
    {
        #region Métodos Asíncronos
        Task<bool> InsertAsync(Products product);
        Task<bool> UpdateAsync(Products product);
        Task<bool> DeleteAsync(int productId);
        Task<Products> GetAsync(int productId);
        Task<IEnumerable<Products>> GetAllAsync();
        #endregion
    }
}
