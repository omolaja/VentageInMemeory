using System;
namespace VentageApplication.Models
{
	public class CustomerModel
	{
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int GenderId { get; set; }
        public string Website { get; set; }

        public CustomerAddressModel customerAddress { get; set; }

    }
}

