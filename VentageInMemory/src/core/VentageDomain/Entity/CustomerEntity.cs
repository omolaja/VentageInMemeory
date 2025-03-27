using System;
namespace VentageDomain.Entity
{
	public class CustomerEntity
	{
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int GenderId { get; set; }
        public string Website { get; set; }
        public bool IsDeleted { get; set; }  
        public DateTime DateCreated { get; set; }

    }
}

