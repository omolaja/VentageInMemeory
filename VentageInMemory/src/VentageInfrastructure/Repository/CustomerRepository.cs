using System;
using VentageApplication.IRepository;
using VentageDomain.Entity;
using VentageInfrastructure.DataAccess;

namespace VentageInfrastructure.Repository
{
	public class CustomerRepository : ICustomerRepository
	{
        private readonly Customer _customer;
		public CustomerRepository(Customer customer)
		{
            _customer = customer;
		}

        public async Task<int> AddCustomer(CustomerEntity customerEntity)
        {
            return await _customer.CreateCustomer(customerEntity);
        }

        public async Task<int> DeleteCustomer(int Id)
        {
            return await _customer.DeleteCustomer(Id);
        }

        public async Task<IEnumerable<CustomerEntity>> GetAllCustomers()
        {
            return await _customer.GetAllCustomer();
        }

        public async Task<CustomerEntity?> GetCustomerById(int Id)
        {
            return await _customer.GetCustomerById(Id);
        }

        public async Task<bool> UpdateCustomer(CustomerEntity customerEntity)
        {
            return await _customer.UpdateCustomer(customerEntity);
        }
    }
}

