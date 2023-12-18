namespace FreeCourse.Web.Models.Catalogs
{
    public class UpdateCourseInput
    {
        public string CourseId { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }


        public decimal Price { get; set; }

        public string? UserId { get; set; }


        public string? Picture { get; set; }


        public FeatureViewModel Feature { get; set; }


        public string CategoryId { get; set; }
    }
}
