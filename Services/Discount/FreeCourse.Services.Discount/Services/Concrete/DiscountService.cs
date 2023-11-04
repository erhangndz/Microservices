using Dapper;
using FreeCourse.Services.Discount.Services.Abstract;
using FreeCourse.Shared.Dtos;
using Npgsql;
using System.Data;

namespace FreeCourse.Services.Discount.Services.Concrete
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));

        }

        public async Task<Response<NoContent>> Delete(int id)
        {
            var deleteStatus = await _dbConnection.ExecuteAsync($"delete from discount where Id={id}");

            return deleteStatus > 0 ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("Discount Not Found", 404);
        }

        public async Task<Response<List<Models.Discount>>> GetAll()
        {
            var discounts = await _dbConnection.QueryAsync<Models.Discount>("select * from discount");

            return Response<List<Models.Discount>>.Success(discounts.ToList(), 200);
        }

        public async Task<Response<Models.Discount>> GetByCodeAndUserId(string code, string userId)
        {
            var discounts = await _dbConnection.QueryAsync<Models.Discount>($"select * from discount where userId='{userId}' and code='{code}'");

            var hasDiscount = discounts.FirstOrDefault();

            if (hasDiscount == null)
            {
                return Response<Models.Discount>.Fail("Discount Not Found", 404);
            }

            return Response<Models.Discount>.Success(hasDiscount, 200);
        }

        public async Task<Response<Models.Discount>> GetById(int id)
        {
            var discount = (await _dbConnection.QueryAsync<Models.Discount>($"select * from discount where Id={id}")).SingleOrDefault();

            if(discount== null)
            {
                return Response<Models.Discount>.Fail("Discount Not Found", 404);
            }

            return Response<Models.Discount>.Success(discount, 200);
        }

        public async Task<Response<NoContent>> Save(Models.Discount discount)
        {
            var saveStatus = await _dbConnection.ExecuteAsync($"insert into discount(userid,rate,code) values ('{discount.UserId}',{discount.Rate},'{discount.Code}')");

            if (saveStatus > 0)
            {
                return Response<NoContent>.Success(204);
            }

            return Response<NoContent>.Fail("An Error Occured while saving", 500);
        }

        public async Task<Response<NoContent>> Update(Models.Discount discount)
        {
            var updateStatus = await _dbConnection.ExecuteAsync($"update discount set userId='{discount.UserId}',code='{discount.Code}',rate={discount.Rate} where Id={discount.Id}");

            if (updateStatus > 0)
            {
                return Response<NoContent>.Success(204);
            }

            return Response<NoContent>.Fail("Discount Not Found", 404);
        }
    }
}
