using System;
using System.Data;
using Dapper;
using VentageDomain.Entity;

namespace VentageInfrastructure.DataAccess
{
	public class CustomerAddress
	{
        private readonly IDbConnection _dbConnection;

        public CustomerAddress(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<int> CreateCustomerAddress(CustomerAddressEntity entity)
        {
            try
            {
                return await _dbConnection.QuerySingleAsync<int>(@"
                INSERT INTO CustomerAddress (CustomerId, CountryId, Address, PostCode)
                VALUES (@CustomerId, @CountryId,@Address, @PostCode);
                SELECT last_insert_rowid();", new
                {
                    entity.CustomerId,
                    entity.CountryId,
                    entity.Address,
                    entity.PostCode
                });
            }
            catch(Exception ex)
            {
                string error = ex.ToString();
                return -1;
            }
        }


        public async Task<IEnumerable<CustomerAddressEntity>> GetAllCustomerAddress()
        {
            return await _dbConnection.QueryAsync<CustomerAddressEntity>("SELECT * FROM CustomerAddress WHERE IsDeleted = 0");
        }


        public async Task<bool> UpdateCustomer(CustomerAddressEntity entity)
        {
            var response = await _dbConnection.ExecuteAsync(@"
                UPDATE CustomerAddress
                SET CustomerId = @CustomerId, CountryId = @CountryId, Address=@Address, PostCode = @PostCode
                WHERE Id = @Id", new
            {
                entity.CustomerId,
                entity.CountryId,
                entity.Address,
                entity.PostCode,
                entity.Id
            });

            return response > 0;
        }

        public async Task<int> DeleteCustomerAddress(int Id)
        {
            return await _dbConnection.ExecuteAsync("UPDATE CustomerAddress SET IsDeleted = 1 WHERE Id = @Id", new { Id });

        }

        public async Task<CustomerAddressEntity?> GetCustomerAddressById(int Id)
        {
            var query = "SELECT * FROM CustomerAddress WHERE Id = @Id and IsDeleted = 0";
            return await _dbConnection.QueryFirstOrDefaultAsync<CustomerAddressEntity>(query, new { Id });

        }

        public async Task<CustomerAddressEntity?> GetCustomerAddressByCustomerId(int CustomerId)
        {
            var query = "SELECT * FROM CustomerAddress WHERE CustomerId = @CustomerId and IsDeleted = 0";
            return await _dbConnection.QueryFirstOrDefaultAsync<CustomerAddressEntity>(query, new { CustomerId });

        }
    }
}

