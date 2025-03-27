using System;
using AutoMapper;
using VentageApplication.Models;
using VentageDomain.Entity;

namespace VentageApplication.Mapper
{
	public class DataObjectMappers : Profile
	{
		public DataObjectMappers() 
		{
			CreateMap<CustomerModel, CustomerEntity>();
			CreateMap<CustomerEntity, CustomerDTO>();
			CreateMap<CustomerUpdateModel, CustomerEntity>();
            CreateMap<CustomerEntity, CustomerModel>();
        }
	}
}

