using System;
using VentageDomain.Entity;

namespace VentageApplication.IRepository
{
	public interface ICustomerAddressRepository
	{
		Task<int> AddCustomerAddressAsync(CustomerAddressEntity customerAddressEntity);

		Task<CustomerAddressEntity> GetAllCustomerAddressByIdAsync(int Id);

		Task<IEnumerable<CustomerAddressEntity>> GetAllCustomerAddress();

		Task<int> DeleteCustomerAddress(int Id);

		Task<CustomerAddressEntity> GetCustomerAddressByCustomerId(int CustomerId);
	}
}

