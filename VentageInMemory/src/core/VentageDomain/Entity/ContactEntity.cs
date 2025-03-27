using System;
namespace VentageDomain.Entity
{
	public class ContactEntity
	{
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public int CustomerId { get; set; }  
        public bool IsDeleted { get; set; } = true;  
    }
}

