using System;
using AutoMapper;
using MediatR;
using VentageApplication.IRepository;
using VentageApplication.Models;
using VentageApplication.StandardResponse;
using VentageDomain.Entity;

namespace VentageApplication.Features.Customer.Command
{
    public class CustomerRequest : IRequest<Response>
    {
        public CustomerModel CustomerModel { get; set; }

        public CustomerRequest(CustomerModel customerModel)
        {
            CustomerModel = customerModel;

        }
    }


    public class CustomerRequestHandler : IRequestHandler<CustomerRequest, Response>
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerAddressRepository _customerAddressRepository;

        public CustomerRequestHandler(IMapper mapper, ICustomerRepository customerRepository, ICustomerAddressRepository customerAddressRepository)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
            _customerAddressRepository = customerAddressRepository;
        }


        public async Task<Response> Handle(CustomerRequest request, CancellationToken cancellationToken)
        {


            var customer = _mapper.Map<CustomerEntity>(request.CustomerModel);

            customer.IsDeleted = false;

            customer.DateCreated = DateTime.UtcNow;

            var response = await _customerRepository.AddCustomer(customer);

            if(response > 0)
            {
                if(customer.customerAddress != null)
                {
                    var customerAddress = _mapper.Map<CustomerAddressEntity>(request.CustomerModel.customerAddress);
                    customerAddress.CustomerId = response;
                    var customerAddressResponse = await _customerAddressRepository.AddCustomerAddressAsync(customerAddress);

                    return customerAddressResponse > 0 ? ResponseHelper.CreateResponse(ResponseStatus.Success, null) :
                        ResponseHelper.CreateResponse(ResponseStatus.PartialSuccess, null);
                }
            }

            return response > 0 ? ResponseHelper.CreateResponse(ResponseStatus.Success, null) :
                ResponseHelper.CreateResponse(ResponseStatus.Failure, null);
        }
    }
}

