using System;
using AutoMapper;
using MediatR;
using VentageApplication.IRepository;
using VentageApplication.Models;

namespace VentageApplication.Features.Customer.Queries
{
	public class GetCustomerRequest : IRequest<IEnumerable<CustomerDTO>>
	{

		public GetCustomerRequest()
		{
		}
	}

    public class GetCustomerRequestHandler : IRequestHandler<GetCustomerRequest, IEnumerable<CustomerDTO>>
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerRequestHandler(IMapper mapper, ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerDTO>> Handle(GetCustomerRequest request, CancellationToken cancellationToken)
        {
            var response = await _customerRepository.GetAllCustomers();

            if (response == null)
            {
                return null;
            }

            var customer = _mapper.Map<IEnumerable<CustomerDTO>>(response);
            
            return customer;

          
        }
    }
}

