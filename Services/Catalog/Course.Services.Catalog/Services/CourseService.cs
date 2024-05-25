using AutoMapper;
using Course.Services.Catalog.Dtos;
using Course.Services.Catalog.Models;
using Course.Services.Catalog.Settings;
using Course.Shared.Dtos;
using MongoDB.Driver;

namespace Course.Services.Catalog.Services
{
    public class CourseService:ICourseService
    {
        private readonly IMongoCollection<Courses> _courseCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CourseService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString); // hangi veri tabanınına bağlanacağımızın yolunu veriyoruz. ve burada bağlanıyor veri tabanına
            var database = client.GetDatabase(databaseSettings.DatabaseName); //bu bağlandığımız client üzerinden de bana bir database ismi veriyor

            _courseCollection = database.GetCollection<Courses>(databaseSettings.CourseCollectionName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionNmae);
            _mapper = mapper;
        }

        public async Task<Response<List<CourseDto>>> GetAllAsync()
        {
            var courses = await _courseCollection.Find(course => true).ToListAsync();


            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category=await _categoryCollection.Find(x=>x.Id==course.CategoryId).FirstAsync();
                }
            }
            else { courses= new List<Courses>();}

            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses),200);
        }

        public async Task<Response<CourseDto>> GetByIdAsync(string id)
        {
            var course=await _courseCollection.Find<Courses>(x=>x.Id==id).FirstOrDefaultAsync();

            if (course == null)
            {
                return Response<CourseDto>.Fail("Course not found", 404);
            }

            course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.CategoryId).FirstAsync();

            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course),200);
        }

        public async Task<Response<List<CourseDto>>> GetAllByUserId(string userId)
        {
            var courses = await _courseCollection.Find<Courses>(x => x.UserId == userId).ToListAsync();

            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = await _categoryCollection.Find(x => x.Id == course.CategoryId).FirstAsync();
                }
            }
            else { courses = new List<Courses>(); }

            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        } 

        public async Task<Response<CourseCreateDto>> CreateAsync(CourseCreateDto courseCreateDto)
        {
            var newCourse=_mapper.Map<Courses>(courseCreateDto);
            newCourse.CreatedTime = DateTime.Now;
            await _courseCollection.InsertOneAsync(newCourse);

            return Response<CourseCreateDto>.Success(_mapper.Map<CourseCreateDto>(newCourse),200);  
        }

        public async Task<Response<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto)
        {
            var updateCourse = _mapper.Map<Courses>(courseUpdateDto);

            var result = await _courseCollection.FindOneAndReplaceAsync(x => x.Id == courseUpdateDto.Id, updateCourse); // eğer x.Id == courseUpdateDto.Id bu şart sağlanırsa updateCourse bilgileri ile eski data güncellenir. eğer bu şart sağlanmazsa result null gelir. bunun kontrolü yapılmalıdır.

            if (result==null)
            {
                return Response<NoContent>.Fail("Course not found", 404);
            }

            return Response<NoContent>.Success(204);
        }

        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var result=await _courseCollection.DeleteOneAsync(x=>x.Id == id);

            if (result.DeletedCount>0)
            {
                return Response<NoContent>.Success(204);
            }

            return Response<NoContent>.Fail("Course not found", 404);
        }
    }
}
