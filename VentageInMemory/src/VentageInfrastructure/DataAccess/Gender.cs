using System;
using System.Data;
using Dapper;
using VentageDomain.Entity;

namespace VentageInfrastructure.DataAccess
{
	public class Gender
	{
        private readonly IDbConnection _dbConnection;

        public Gender(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }


        public async Task<int> AddGender(GenderEntity entity)
        {
            try
            {
                return await _dbConnection.QuerySingleAsync<int>(@"
                INSERT INTO Gender (Name)
                VALUES (@Name);
                SELECT last_insert_rowid();", new
                {
                    entity.Name
                });
            }
            catch
            {
                return -1;
            }
        }


        public async Task<IEnumerable<GenderEntity>> GetAllGender()
        {
            return await _dbConnection.QueryAsync<GenderEntity>("SELECT * FROM Gender WHERE IsDeleted = 0");
        }


        public async Task<int> DeleteGender(int Id)
        {
            return await _dbConnection.ExecuteAsync("Update Gender set IsDeleted = 1  WHERE Id = @Id", new { Id });
        }

        public async Task<GenderEntity?> GetGenderById(int Id)
        {
            var query = "SELECT * FROM Gender WHERE Id = @Id and IsDeleted = 0";
            return await _dbConnection.QueryFirstOrDefaultAsync<GenderEntity>(query, new { Id });

        }


        public async Task<bool> UpdateGender(GenderEntity entity)
        {
            var response = await _dbConnection.ExecuteAsync(@"
                UPDATE Gender
                SET Name = @Name
                WHERE Id = @Id", new
            {
                entity.Name,
                entity.Id
            });

            return response > 0;
        }

    }
}

