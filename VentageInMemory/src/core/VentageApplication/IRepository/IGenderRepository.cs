using System;
using VentageApplication.Models;
using VentageDomain.Entity;

namespace VentageApplication.IRepository
{
	public interface IGenderRepository
    {
		Task<GenderEntity> GetGenderByIdAsync(int Id);

		Task<int> AddGenderAsync(GenderEntity genderEntity);

		Task<bool> UpdateGendeAsync(GenderEntity genderEntity);

		Task<int> DeleteGenderAsync(int Id);

		Task<IEnumerable<GenderEntity>> GetAllGenderAsync();

	}
}

