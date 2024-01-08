using AutoMapper;
using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Services.Interfaces;
using FreeCourse.Services.Catalog.Settings;
using FreeCourse.Shared.Dtos;
using FreeCourse.Shared.Messages;
using MassTransit;
using MongoDB.Driver;

namespace FreeCourse.Services.Catalog.Services.Concrete
{
    public class CourseService : ICourseService
    {

        private readonly IMongoCollection<Course> _courseCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;

        public CourseService(IMapper mapper, IDatabaseSettings databaseSettings, IPublishEndpoint publishEndpoint)
        {

            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);



            _courseCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Shared.Dtos.Response<CourseDto>> CreateAsync(CreateCourseDto course)
        {
            var newCourse= _mapper.Map<Course>(course);

            newCourse.CreatedTime = DateTime.Now;
            await _courseCollection.InsertOneAsync(newCourse);

            return Shared.Dtos.Response<CourseDto>.Success(_mapper.Map<CourseDto>(newCourse), 200);
        }

        public async Task<Shared.Dtos.Response<NoContent>> DeleteAsync(string id)
        {
           var result = await _courseCollection.DeleteOneAsync(x=>x.CourseId==id);

            if (result.DeletedCount > 0)
            {
                return Shared.Dtos.Response<NoContent>.Success(204);
            }

            else
            {
                return Shared.Dtos.Response<NoContent>.Fail("Course Not Found", 404);
            }
        }

        public async Task<Shared.Dtos.Response<List<CourseDto>>> GetAllAsync()
        {
            var courses = await _courseCollection.Find(course => true).ToListAsync();





            if (courses.Any())
            {
                foreach (var item in courses)
                {
                    item.Category = await _categoryCollection.Find<Category>(x => x.CategoryId == item.CategoryId).FirstAsync();
                }
            }

            else
            {
                courses = new List<Course>();
            }

            return Shared.Dtos.Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);


        }

        public async Task<Shared.Dtos.Response<List<CourseDto>>> GetAllByUserIdAsync(string userId)
        {
            var courses = await _courseCollection.Find(x => x.UserId == userId).ToListAsync();

            if (courses.Any())
            {
                foreach (var item in courses)
                {
                    item.Category = await _categoryCollection.Find<Category>(x => x.CategoryId == item.CategoryId).FirstAsync();
                }
            }

            else
            {
                courses = new List<Course>();
            }

            return Shared.Dtos.Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);


        }

        public async Task<Shared.Dtos.Response<CourseDto>> GetByIdAsync(string id)
        {
            var course = await _courseCollection.Find<Course>(x => x.CourseId == id).FirstOrDefaultAsync();

            if (course == null)
            {
                return Shared.Dtos.Response<CourseDto>.Fail("Course Not Found", 404);
            }

            course.Category = await _categoryCollection.Find<Category>(x => x.CategoryId == course.CategoryId).FirstAsync();

            return Shared.Dtos.Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);

        }

        public async Task<Shared.Dtos.Response<NoContent>> UpdateAsync(UpdateCourseDto course)
        {
           var updateCourse = _mapper.Map<Course>(course);

            var result = await _courseCollection.FindOneAndReplaceAsync(x=>x.CourseId == course.CourseId, updateCourse);

            if (result == null)
            {
                return Shared.Dtos.Response<NoContent>.Fail("Course Not Found", 404);
            }

            await _publishEndpoint.Publish(new CourseNameChangedEvent
            {
                CourseId = course.CourseId,
                UpdatedName = course.Name
            });

            return Shared.Dtos.Response<NoContent>.Success(204);
        }
    }
}
