using System;
using VentageApplication.IRepository;
using VentageDomain.Entity;
using VentageInfrastructure.DataAccess;

namespace VentageInfrastructure.Repository
{
	public class CustomerAddressRepository : ICustomerAddressRepository
    {
        private readonly CustomerAddress _customerAddress;

		public CustomerAddressRepository(CustomerAddress customerAddress)
		{
            _customerAddress = customerAddress;
        }

        public async Task<int> AddCustomerAddressAsync(CustomerAddressEntity customerAddressEntity)
        {
            return await _customerAddress.CreateCustomerAddress(customerAddressEntity);
        }

        public async Task<int> DeleteCustomerAddress(int Id)
        {
            return await _customerAddress.DeleteCustomerAddress(Id);
        }

        public async Task<IEnumerable<CustomerAddressEntity>> GetAllCustomerAddress()
        {
            return await _customerAddress.GetAllCustomerAddress();
        }

        public async Task<CustomerAddressEntity> GetAllCustomerAddressByIdAsync(int Id)
        {
            return await _customerAddress.GetCustomerAddressById(Id);
        }

        public async Task<CustomerAddressEntity> GetCustomerAddressByCustomerId(int CustomerId)
        {
            return await _customerAddress.GetCustomerAddressByCustomerId(CustomerId);
        }
    }
}

