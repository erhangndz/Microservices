using FreeCourse.Shared.Services.Abstract;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Web.Controllers
{
	[Authorize]
	public class CoursesController : Controller
	{
		private readonly ICatalogService _catalogService;
		private readonly ISharedIdentityService _identityService;

		public CoursesController(ICatalogService catalogService, ISharedIdentityService identityService)
		{
			_catalogService = catalogService;
			_identityService = identityService;
		}

		public async Task<IActionResult> Index()
		{
			return View(await _catalogService.GetAllCourseByUserIdAsync(_identityService.GetUserId));
		}
	}
}
