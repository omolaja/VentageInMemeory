using AutoMapper;
using MediatR;
using VentageApplication.IRepository;
using VentageApplication.Models;

namespace VentageApplication.Features.Customer.Queries
{
    public class GetCustomerById : IRequest<CustomerModel>
	{
		public int Id { get; set; }

		public GetCustomerById(int id)
		{
			Id = id;
		}
	}

    public class GetCustomerByIdHandler : IRequestHandler<GetCustomerById, CustomerModel>
    {
		private readonly ICustomerRepository _customerRepository;
		private readonly IMapper _mapper;

		public GetCustomerByIdHandler(ICustomerRepository customerRepository, IMapper mapper)
		{
			_customerRepository = customerRepository;
			_mapper = mapper;
        }

        public async Task<CustomerModel> Handle(GetCustomerById request, CancellationToken cancellationToken)
        {
            var response = await _customerRepository.GetCustomerById(request.Id);

            if (response != null)
            {
                return _mapper.Map<CustomerModel>(response);

            }
            return null;
        }
    }
}

