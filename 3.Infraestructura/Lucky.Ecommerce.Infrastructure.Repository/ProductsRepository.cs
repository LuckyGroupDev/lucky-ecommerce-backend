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
    public class ProductsRepository : IProductsRepository
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
                // Consulta a la función en PostgreSQL
                var query = "SELECT public.usp_ins_products(@product_name, @supplier_id, @category_id, @quantity_per_unit, @unit_price, @units_in_stock, @units_on_order, @reorder_level, @discontinued)";

                // Usamos un objeto DynamicParameters para mapear los nombres de las propiedades a las columnas de la base de datos
                var parameters = new DynamicParameters();
                parameters.Add("product_name", products.ProductName);
                parameters.Add("supplier_id", products.SupplierID);
                parameters.Add("category_id", products.CategoryID);
                parameters.Add("quantity_per_unit", products.QuantityPerUnit);
                parameters.Add("unit_price", products.UnitPrice);
                parameters.Add("units_in_stock", products.UnitsInStock);
                parameters.Add("units_on_order", products.UnitsOnOrder);
                parameters.Add("reorder_level", products.ReorderLevel);
                parameters.Add("discontinued", products.Discontinued);

                // Ejecutar la consulta
                var result = await connection.ExecuteScalarAsync<int>(query, param: parameters, commandType: CommandType.Text);

                // Si el resultado es mayor que 0, entonces la inserción fue exitosa
                return result > 0;
            }
        }
        public async Task<bool> UpdateAsync(Products products)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "SELECT public.usp_upd_products(@ProductID, @ProductName, @SupplierID, @CategoryID, @QuantityPerUnit, @UnitPrice, @UnitsInStock, @UnitsOnOrder, @ReorderLevel, @Discontinued)";
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

                // Ejecutamos la función y obtenemos el resultado
                var result = await connection.ExecuteScalarAsync<int>(query, param: parameters, commandType: CommandType.Text);

                // Si result es 1, el producto se actualizó correctamente
                return result > 0;
            }
        }
        public async Task<bool> DeleteAsync(int productId)
        {
            using var connection = _connectionFactory.GetConnection;

            const string sql = "SELECT public.usp_del_products(@p_productid)";

            var parameters = new { p_productid = productId };

            var result = await connection.ExecuteScalarAsync<int>(
                sql,
                param: parameters,
                commandType: CommandType.Text
            );

            return result > 0;
        }
        public async Task<Products?> GetAsync(int productId)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "SELECT * FROM public.usp_get_products_by_id(@p_productid)";
                var parameters = new { p_productid = productId };
                var product = await connection.QuerySingleOrDefaultAsync<Products>(query, param: parameters, commandType: CommandType.Text);
                return product;
            }
        }
        public async Task<IEnumerable<Products>> GetAllAsync()
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                // Aquí usamos directamente la función en una consulta SELECT.
                var query = "SELECT * FROM public.usp_get_list_products()";
                var product = await connection.QueryAsync<Products>(query, commandType: CommandType.Text);
                return product;
            }
        }

        #endregion
    }
}
