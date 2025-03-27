using VentageDomain.Entity;

namespace VentageApplication.IRepository
{
    public interface ICustomerRepository
	{
		Task<int> AddCustomer(CustomerEntity customerEntity);
		Task<bool> UpdateCustomer(CustomerEntity customerEntity);
		Task<CustomerEntity> GetCustomerById(int Id);
		Task<IEnumerable<CustomerEntity>> GetAllCustomers();
		Task<int> DeleteCustomer(int Id);
	}
}
