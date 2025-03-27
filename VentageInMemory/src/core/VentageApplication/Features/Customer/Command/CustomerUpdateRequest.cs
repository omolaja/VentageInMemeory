using System;
using AutoMapper;
using MediatR;
using VentageApplication.IRepository;
using VentageApplication.Models;
using VentageApplication.StandardResponse;
using VentageDomain.Entity;

namespace VentageApplication.Features.Customer.Command
{
	public class CustomerUpdateRequest : IRequest<Response>
	{
		public CustomerUpdateModel CustomerUpdateModel { get; set; }
        public CustomerUpdateRequest(CustomerUpdateModel customerUpdateModel)
		{
			CustomerUpdateModel = customerUpdateModel;
        }

	}

    public class CustomerUpdateRequestHandler : IRequestHandler<CustomerUpdateRequest, Response>
    {
        public readonly ICustomerRepository _customerRepository;
        public readonly IMapper _mapper;

        public CustomerUpdateRequestHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<Response> Handle(CustomerUpdateRequest request, CancellationToken cancellationToken)
        {
           
            var response = await _customerRepository.GetCustomerById(request.CustomerUpdateModel.Id);

            if(response != null)
            {
                var customer = _mapper.Map<CustomerEntity>(request.CustomerUpdateModel);

                var isSuccessful = await _customerRepository.UpdateCustomer(customer);
               
                return isSuccessful ? ResponseHelper.CreateResponse(ResponseStatus.Success, null) : ResponseHelper.CreateResponse(ResponseStatus.NoUpdate, null);
            }

            return ResponseHelper.CreateResponse(ResponseStatus.NoRecord, null);
        }
    }
}

