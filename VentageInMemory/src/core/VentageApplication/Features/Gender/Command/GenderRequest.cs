using System;
using AutoMapper;
using MediatR;
using VentageApplication.IRepository;
using VentageApplication.Models;
using VentageApplication.StandardResponse;
using VentageDomain.Entity;

namespace VentageApplication.Features.Gender.Command
{
	public class GenderRequest : IRequest<Response>
	{
		public GenderModel GenderModel { get; set; }

		public GenderRequest(GenderModel genderModel)
		{
			GenderModel = genderModel;

        }
	}


    public class GenderRequesgHandler : IRequestHandler<GenderRequest, Response>
    {

        private readonly IGenderRepository _genderRepository;
        private readonly IMapper _mapper;

        public GenderRequesgHandler(IGenderRepository genderRepository, IMapper mapper)
        {
            _genderRepository = genderRepository;
            _mapper = mapper; 
        }

        public async Task<Response> Handle(GenderRequest request, CancellationToken cancellationToken)
        {

            var Gender = _mapper.Map<GenderEntity>(request.GenderModel);


            var response = await _genderRepository.AddGenderAsync(Gender);


            return response > 0 ? ResponseHelper.CreateResponse(ResponseStatus.Success, null) :
                ResponseHelper.CreateResponse(ResponseStatus.Failure, null);
        }
    }
}
