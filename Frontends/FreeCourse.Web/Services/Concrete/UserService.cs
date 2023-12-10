using FreeCourse.Web.Models;
using FreeCourse.Web.Services.Interfaces;

namespace FreeCourse.Web.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly HttpClient _client;

        public UserService(HttpClient httpClient)
        {
            _client = httpClient;
        }

        public async Task<UserViewModel> GetUser()
        {
            return await _client.GetFromJsonAsync<UserViewModel>("/api/user/getuser");
        }
    }
}
