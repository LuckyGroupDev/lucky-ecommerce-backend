using AutoMapper;
using Lucky.Ecommerce.Application.Dto;
using Lucky.Ecommerce.Application.Interface;
using Lucky.Ecommerce.Domain.Entity;
using Lucky.Ecommerce.Domain.Interface;
using Lucky.Ecommerce.Transversal.Common;

namespace Lucky.Ecommerce.Application.Main
{
    public class ProductsApplication:IProductsApplication
    {
        private readonly IProductsDomain _productsDomain;
        private readonly IMapper _mapper;

        public ProductsApplication(IProductsDomain productsDomain, IMapper mapper)
        {
            _productsDomain = productsDomain;
            _mapper = mapper;
        }
            #region Métodos Asíncronos
            public async Task<Response<bool>> InsertAsync(ProductsDto productDto)
            {
                var response = new Response<bool>();
                try
                {
                    var product = _mapper.Map<Products>(productDto);
                    response.Data = await _productsDomain.InsertAsync(product);
                    if (response.Data)
                    {
                        response.IsSuccess = true;
                        response.Message = "Registro Exitoso";
                    }
                }
                catch (Exception ex)
                {
                    response.Message = ex.Message;
                }
                return response;
            }
            public async Task<Response<bool>> UpdateAsync(ProductsDto productDto)
            {
                var response = new Response<bool>();
                try
                {
                    var product = _mapper.Map<Products>(productDto);
                    response.Data = await _productsDomain.UpdateAsync(product);
                    if (response.Data)
                    {
                        response.IsSuccess = true;
                        response.Message = "Actualización Exitosa";
                    }
                }
                catch (Exception e)
                {
                    response.Message = e.Message;
                }
                return response;
            }
            public async Task<Response<bool>> DeleteAsync(int productId)
            {
                var response = new Response<bool>();
                try
                {
                    response.Data = await _productsDomain.DeleteAsync(productId);
                    if (response.Data)
                    {
                        response.IsSuccess = true;
                        response.Message = "Eliminación Exitosa";
                    }
                }
                catch (Exception e)
                {
                    response.Message = e.Message;
                }
                return response;
            }
            public async Task<Response<ProductsDto>> GetAsync(int productId)
            {
                var response = new Response<ProductsDto>();
                try
                {
                    var product = await _productsDomain.GetAsync(productId);
                    response.Data = _mapper.Map<ProductsDto>(product);
                    if (response.Data != null)
                    {
                        response.IsSuccess = true;
                        response.Message = "Consulta Exitosa";
                    }
                }
                catch (Exception e)
                {
                    response.Message = e.Message;
                }
                return response;
            }
            public async Task<Response<IEnumerable<ProductsDto>>> GetAllAsync()
            {
                var response = new Response<IEnumerable<ProductsDto>>();
                try
                {
                    var product = await _productsDomain.GetAllAsync();
                    response.Data = _mapper.Map<IEnumerable<ProductsDto>>(product);
                    if (response.Data != null)
                    {
                        response.IsSuccess = true;
                        response.Message = "Consulta Exitosa";
                    }
                }
                catch (Exception e)
                {
                    response.Message = e.Message;
                }
                return response;
            }
            #endregion
        }
    
}
