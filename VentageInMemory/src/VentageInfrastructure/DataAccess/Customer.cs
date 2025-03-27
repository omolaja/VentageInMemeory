using System;
using System.Data;
using Dapper;
using VentageApplication.Models;
using VentageDomain.Entity;

namespace VentageInfrastructure.DataAccess
{
	public class Customer
	{
		private readonly IDbConnection _dbConnection;
        public Customer(IDbConnection dbConnection)
		{
			_dbConnection = dbConnection;
		}

        public async Task<int> CreateCustomer(CustomerEntity entity)
        {
            try
            {
                return await _dbConnection.QuerySingleAsync<int>(@"
                INSERT INTO Customers (Name, PhoneNumber, Website)
                VALUES (@Name, @PhoneNumber, @Website);
                SELECT last_insert_rowid();", new
                {
                    entity.Name,
                    entity.PhoneNumber,
                    entity.Website
                });
            }
            catch
            {
                return -1;
            }
        }


        public async Task<IEnumerable<CustomerEntity>> GetAllCustomer()
        {
            return await _dbConnection.QueryAsync<CustomerEntity>("SELECT * FROM Customers WHERE IsDeleted = 0");
        }


        public async Task<bool> UpdateCustomer(CustomerEntity entity)
        {
            var response =  await _dbConnection.ExecuteAsync(@"
                UPDATE Customers
                SET Name = @Name, PhoneNumber = @PhoneNumber, Website = @Website
                WHERE Id = @Id", new { entity.Name, entity.PhoneNumber, entity.Website, entity.Id });

            return response > 0;
        }

        public async Task<int> DeleteCustomer(int Id)
        {
           return await _dbConnection.ExecuteAsync("UPDATE Customers SET IsDeleted = 1 WHERE Id = @Id", new { Id });
           
        }

        public async Task<CustomerEntity?> GetCustomerById(int Id)
        {
            var query = "SELECT * FROM Customers WHERE Id = @Id and IsDeleted = 0";
            return await _dbConnection.QueryFirstOrDefaultAsync<CustomerEntity>(query, new { Id });

        }
    }
}

