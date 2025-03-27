using System;
namespace VentageApplication.Models
{
	public class CustomerDTO : CustomerModel
	{
		public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
    }
}

