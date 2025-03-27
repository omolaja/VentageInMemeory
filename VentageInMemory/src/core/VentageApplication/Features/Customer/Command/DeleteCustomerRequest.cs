using System;
using AutoMapper;
using MediatR;
using VentageApplication.IRepository;
using VentageApplication.Models;
using VentageApplication.StandardResponse;
using VentageDomain.Entity;

namespace VentageApplication.Features.Customer.Command
{
	public class DeleteCustomerRequest : IRequest<Response>
	{
		public int Id { get; set; }

		public DeleteCustomerRequest(int id)
		{
			Id = id;
		}
	}

    public class DeleteCustomerRequestHandler : IRequestHandler<DeleteCustomerRequest, Response>
    {
		public readonly ICustomerRepository _customerRepository;
		public readonly IMapper _mapper;

		public DeleteCustomerRequestHandler(ICustomerRepository customerRepository, IMapper mapper)
		{
			_customerRepository = customerRepository;
			_mapper = mapper;
        }

        public async Task<Response> Handle(DeleteCustomerRequest request, CancellationToken cancellationToken)
        {

			var response = await _customerRepository.GetCustomerById(request.Id);

			if(response != null)
			{
				
				var isDeleted = await _customerRepository.DeleteCustomer(request.Id);

                return isDeleted > 0 ? ResponseHelper.CreateResponse(ResponseStatus.Success, null) : ResponseHelper.CreateResponse(ResponseStatus.NoDelete, null);
            }

            return ResponseHelper.CreateResponse(ResponseStatus.NoRecord, null);
        }
    }
}

