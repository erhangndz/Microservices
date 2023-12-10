using FreeCourse.Web.Models;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> GetUser();

    }
}
