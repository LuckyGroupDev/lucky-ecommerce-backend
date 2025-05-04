using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Lucky.Ecommerce.Domain.Entity;
using Lucky.Ecommerce.Infrastructure.Interface;
using Lucky.Ecommerce.Transversal.Common;
namespace Lucky.Ecommerce.Infrastructure.Repository
{
    public class ProductsRepository:IProductsRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public ProductsRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        #region Métodos Asíncronos
        public async Task<bool> InsertAsync(Products products)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "USP_INS_PRODUCTS";
                var parameters = new DynamicParameters();
                parameters.Add("ProductName", products.ProductName);
                parameters.Add("SupplierID", products.SupplierID);
                parameters.Add("CategoryID", products.CategoryID);
                parameters.Add("QuantityPerUnit", products.QuantityPerUnit);
                parameters.Add("UnitPrice", products.UnitPrice);
                parameters.Add("UnitsInStock", products.UnitsInStock);
                parameters.Add("UnitsOnOrder", products.UnitsOnOrder);
                parameters.Add("ReorderLevel", products.ReorderLevel);
                parameters.Add("Discontinued", products.Discontinued);
                //var result = await connection.ExecuteAsync(query, param: parameters, commandType: CommandType.StoredProcedure);
                var result = await connection.ExecuteScalarAsync<int>(query, param: parameters, commandType: CommandType.StoredProcedure);

                return result > 0;
            }
        }
        public async Task<bool> UpdateAsync(Products products)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "USP_UPD_PRODUCTS";
                var parameters = new DynamicParameters();
                parameters.Add("ProductID", products.ProductID);
                parameters.Add("ProductName", products.ProductName);
                parameters.Add("SupplierID", products.SupplierID);
                parameters.Add("CategoryID", products.CategoryID);
                parameters.Add("QuantityPerUnit", products.QuantityPerUnit);
                parameters.Add("UnitPrice", products.UnitPrice);
                parameters.Add("UnitsInStock", products.UnitsInStock);
                parameters.Add("UnitsOnOrder", products.UnitsOnOrder);
                parameters.Add("ReorderLevel", products.ReorderLevel);
                parameters.Add("Discontinued", products.Discontinued);
                //var result = await connection.ExecuteAsync(query, param: parameters, commandType: CommandType.StoredProcedure);
                var result = await connection.ExecuteScalarAsync<int>(query, param: parameters, commandType: CommandType.StoredProcedure);

                return result > 0;
            }
        }
        public async Task<bool> DeleteAsync(int productId)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "USP_DEL_PRODUCTS";
                var parameters = new DynamicParameters();
                parameters.Add("ProductID", productId);
                //var result = await connection.ExecuteAsync(query, param: parameters, commandType: CommandType.StoredProcedure);
                var result = await connection.ExecuteScalarAsync<int>(query, param: parameters, commandType: CommandType.StoredProcedure);

                return result > 0;
            }
        }
        public async Task<Products> GetAsync(int productId)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                //var query = "ProductsGetByID";
                var query = "USP_GET_PRODUCTS_BY_ID";
                var parameters = new DynamicParameters();
                parameters.Add("ProductID", productId);
                var product = await connection.QuerySingleAsync<Products>(query, param: parameters, commandType: CommandType.StoredProcedure);
                return product;
            }
        }
        public async Task<IEnumerable<Products>> GetAllAsync()
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "USP_GET_LIST_PRODUCTS";
                var product = await connection.QueryAsync<Products>(query, commandType: CommandType.StoredProcedure);
                return product;
            }
        }

        #endregion
    }
}
