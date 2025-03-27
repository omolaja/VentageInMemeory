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
                INSERT INTO Customers (FirstName, LastName, GenderId, Website)
                VALUES (@FirstName, @LastName,@GenderId, @Website);
                SELECT last_insert_rowid();", new
                {
                    entity.FirstName,
                    entity.LastName,
                    entity.GenderId,
                    entity.Website
                });
            }
            catch
            {
                return -1;
            }
        }

        // return await _dbConnection.QueryAsync<CustomerEntity>("SELECT * FROM Customers WHERE IsDeleted = 0");
        public async Task<IEnumerable<CustomerEntity>> GetAllCustomer()
        {
            var sql = @"
        SELECT 
            c.Id, c.FirstName, c.LastName, c.GenderId, c.Website, c.IsDeleted, c.DateCreated,
            a.Id AS CustomerAddressId, a.Address, a.PostCode, a.CountryId,
            con.Id AS CountryId, con.Name AS CountryName
        FROM Customers c
        LEFT JOIN CustomerAddress a ON c.Id = a.CustomerId
        LEFT JOIN Country con ON a.CountryId = con.Id
        WHERE c.IsDeleted = 0";

            var customerDictionary = new Dictionary<int, CustomerEntity>();

            var customers = await _dbConnection.QueryAsync<CustomerEntity, CustomerAddressEntity, CountryEntity, CustomerEntity>(
                sql,
                (customer, address, country) =>
                {
                    if (!customerDictionary.TryGetValue(customer.Id, out var existingCustomer))
                    {
                        existingCustomer = customer;
                        existingCustomer.customerAddress = address;
                        customerDictionary.Add(existingCustomer.Id, existingCustomer);
                    }
                    else
                    {
                        existingCustomer.customerAddress = address;
                       
                    }
                    return existingCustomer;
                },
                splitOn: "CustomerAddressId,CountryId"
            );

            return customers.Distinct();
        }



        public async Task<bool> UpdateCustomer(CustomerEntity entity)
        {
            var response = await _dbConnection.ExecuteAsync(@"
                UPDATE Customers
                SET Firstname = @Firstname, Lastname = @Lastname, GenderId=@GenderId, Website = @Website
                WHERE Id = @Id", new
            {
                entity.FirstName,
                entity.LastName,
                entity.GenderId,
                entity.Website,
                entity.Id
            });

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

