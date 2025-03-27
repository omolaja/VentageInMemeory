using System;
namespace VentageDomain.Entity
{
	public class CustomerAddressEntity
	{
        public int Id { get; set; }
        public int CustomerId { get; set; }  
        public int CountryId { get; set; }   
        public string Address { get; set; }
        public string PostCode { get; set; }
        public bool IsDeleted { get; set; } = true;  

    }
}

