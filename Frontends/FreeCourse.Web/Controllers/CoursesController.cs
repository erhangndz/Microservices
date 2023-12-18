using FreeCourse.Shared.Services.Abstract;
using FreeCourse.Web.Models.Catalogs;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
			var values = await _catalogService.GetAllCourseByUserIdAsync(_identityService.GetUserId);

            return View(values);
		}


		public async Task<IActionResult> Create()
		{
			var categories = await _catalogService.GetAllCategoriesAsync();

			ViewBag.categoryList = new List<SelectListItem> (from x in categories select new SelectListItem
			{
				Text= x.Name,
				Value=x.CategoryId
			}).ToList();

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateCourseInput createCourseInput)
		{
            var categories = await _catalogService.GetAllCategoriesAsync();

            ViewBag.categoryList = new SelectList(categories, "Id", "Name");

            createCourseInput.UserId = _identityService.GetUserId;

			await _catalogService.CreateCourseAsync(createCourseInput);

			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Update(string id)
		{
            

            var course =   await _catalogService.GetByCourseIdAsync(id);

			var courseUpdate = new UpdateCourseInput()
			{
				CategoryId = course.CategoryId,
				CourseId = course.CourseId,
				Name = course.Name,
				Description = course.Description,
				Feature = new FeatureViewModel() { Duration = course.Feature.Duration },
				Picture = course.Picture,
				Price = course.Price,
				UserId = course.UserId
			};
            var categories = await _catalogService.GetAllCategoriesAsync();

            ViewBag.categoryList = new List<SelectListItem>(from x in categories
                                                            select new SelectListItem
                                                            {
                                                                Text = x.Name,
                                                                Value = x.CategoryId
                                                            }).ToList();
            return View(courseUpdate);
		}

		[HttpPost]
		public async Task<IActionResult> Update(UpdateCourseInput updateCourseInput)
		{
			await _catalogService.UpdateCourseAsync(updateCourseInput);
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Delete(string id)
		{
			await _catalogService.DeleteCourseAsync(id);
			return RedirectToAction("Index");
		}
	}
}
