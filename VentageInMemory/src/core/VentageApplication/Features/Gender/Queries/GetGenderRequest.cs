using System;
using System.Collections.Generic;
using AutoMapper;
using MediatR;
using VentageApplication.IRepository;
using VentageApplication.Models;

namespace VentageApplication.Features.Gender.Queries
{
	public class GetGenderRequest : IRequest<IEnumerable<GenderDTO>>
	{
		public GetGenderRequest()
        {
        }
    }


    public class GetGenderRequestHandler : IRequestHandler<GetGenderRequest, IEnumerable<GenderDTO>>
    {
        private readonly IGenderRepository _genderRepository;
        private readonly IMapper _mapper;

        public GetGenderRequestHandler(IGenderRepository genderRepository, IMapper mapper)
        {
            _genderRepository = genderRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GenderDTO>> Handle(GetGenderRequest request, CancellationToken cancellationToken)
        {
            var response = await _genderRepository.GetAllGenderAsync();

            if(response != null)
            {
                return _mapper.Map<IEnumerable<GenderDTO>>(response);
            }

            return null;
        }
    }
}

