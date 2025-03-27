using System;
using VentageApplication.IRepository;
using VentageApplication.Models;
using VentageDomain.Entity;
using VentageInfrastructure.DataAccess;

namespace VentageInfrastructure.Repository
{
	public class GenderRepository : IGenderRepository
    {
        public readonly Gender _gender;

        public GenderRepository(Gender gender)
		{
            _gender = gender;
        }

        public async Task<int> AddGenderAsync(GenderEntity genderEntity)
        {
            return await _gender.AddGender(genderEntity);
        }

        public async Task<int> DeleteGenderAsync(int Id)
        {
            return await _gender.DeleteGender(Id);
        }

        public async Task<IEnumerable<GenderEntity>> GetAllGenderAsync()
        {
            return await _gender.GetAllGender();
        }

        public async Task<GenderEntity?> GetGenderByIdAsync(int Id)
        {
            return await _gender.GetGenderById(Id);

        }

        public async Task<bool> UpdateGendeAsync(GenderEntity genderEntity)
        {
            return await _gender.UpdateGender(genderEntity);
        }
    }
}

