using System;
namespace VentageDomain.Entity
{
	public class CustomerEntity
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Website { get; set; }
        public bool IsDeleted { get; set; }  
        public DateTime DateCreated { get; set; }

    }
}

