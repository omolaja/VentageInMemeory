using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VentageApplication.Features.Customer.Queries;
using VentageApplication.Features.Gender.Command;
using VentageApplication.Features.Gender.Queries;
using VentageApplication.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VentageApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : Controller
    {

        private readonly IMediator _mediator;

        public GenderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddGender([FromBody] GenderModel genderModel)
        {
            var response = await _mediator.Send(new GenderRequest(genderModel));

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGender()
        {
            var response = await _mediator.Send(new GetGenderRequest());

            return Ok(response);
        }
    }
}

