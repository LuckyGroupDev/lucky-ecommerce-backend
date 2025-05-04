using Lucky.Ecommerce.Application.Dto;
using Lucky.Ecommerce.Transversal.Common;

namespace Lucky.Ecommerce.Application.Interface
{
    public interface IProductsApplication
    {

        #region Métodos Asíncronos
        Task<Response<bool>> InsertAsync(ProductsDto productDto);
        Task<Response<bool>> UpdateAsync(ProductsDto productDto);
        Task<Response<bool>> DeleteAsync(int productId);
        Task<Response<ProductsDto>> GetAsync(int productId);
        Task<Response<IEnumerable<ProductsDto>>> GetAllAsync();
        #endregion
    }
}
