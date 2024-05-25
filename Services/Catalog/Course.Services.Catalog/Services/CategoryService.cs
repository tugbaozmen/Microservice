using AutoMapper;
using Course.Services.Catalog.Dtos;
using Course.Services.Catalog.Models;
using Course.Services.Catalog.Settings;
using Course.Shared.Dtos;
using MongoDB.Driver;

namespace Course.Services.Catalog.Services
{
    public class CategoryService : ICategoryService
    {
        //veri tabanından almış olduğumuz datayı dto'ya dönüştüreceğimiz için services içerisinde yazdık.
        //Dönüştürma olmasaydı repostory klasörü içinde yazılması daha sağlıklı olurdu.
        //veri tabanı bilgileri için mongodb ye bağlanma bilgileri lazım
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString); // hangi veri tabanınına bağlanacağımızın yolunu veriyoruz. ve burada bağlanıyor veri tabanına
            var database = client.GetDatabase(databaseSettings.DatabaseName); //bu bağlandığımız client üzerinden de bana bir database ismi veriyor

            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionNmae);
            _mapper = mapper;
        }

        public async Task<Response<List<CategoryDto>>> GetAllAsync()
        {
            var categories = await _categoryCollection.Find(category => true).ToListAsync();
            return Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories), 200);
        }

        public async Task<Response<CategoryCreateDto>> CreateAsync(CategoryCreateDto categoryCreateDto)
        {
            var category = _mapper.Map<Category>(categoryCreateDto);
            await _categoryCollection.InsertOneAsync(category);

            return Response<CategoryCreateDto>.Success(_mapper.Map<CategoryCreateDto>(category), 200);
        }

        public async Task<Response<CategoryDto>> GetByIdAsync(string id)
        {
            var category = await _categoryCollection.Find<Category>(x => x.Id == id).FirstOrDefaultAsync();

            if (category == null)
            {
                return Response<CategoryDto>.Fail("Category not found", 404);
            }

            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }
    }
}
