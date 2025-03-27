using MediatR;
using Microsoft.AspNetCore.Mvc;
using VentageApplication.Features.Customer.Command;
using VentageApplication.Features.Customer.Queries;
using VentageApplication.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VentageApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        public readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AddNewCustomer")]
        public async Task<IActionResult> AddCustomer([FromBody] CustomerModel customers)
        {
            var response = await _mediator.Send(new CustomerRequest(customers));
          
            return Ok(response);
        }

        [HttpGet("GetAllCustomers")]
        public async Task<IActionResult> GetAllCustomerDetail()
        {
            var response = await _mediator.Send(new GetCustomerRequest());
           
            return Ok(response);
        }


        [HttpPut("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerUpdateModel customer)
        {
            var response = await _mediator.Send(new CustomerUpdateRequest(customer));
           
            return Ok(response);
        }

        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> DeleteCustomerDetailById([FromRoute] int Id)
        {
            var response = await _mediator.Send(new DeleteCustomerRequest(Id));
            
            return Ok(response);
        }

        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetCustomerDetailById([FromRoute] int Id)
        {
            var response = await _mediator.Send(new GetCustomerById(Id));
           
            return Ok(response);
        }
    }
}

