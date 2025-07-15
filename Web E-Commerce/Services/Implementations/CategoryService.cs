//using AutoMapper;
//using Web_E_Commerce.DTOs.Category.Responses;
//using Web_E_Commerce.Repositories.Interfaces;
//using Web_E_Commerce.Services.Interfaces;

//namespace Web_E_Commerce.Services.Implementations
//{
//    public class CategoryService (ICategoryRepositories categoryRepositories,
//        IMapper mapper) : ICategoryService
//    {
//        public async Task<IEnumerable<CategoryResponse>> GetAllAsync()
//        {
//            var categories = await categoryRepositories.GetAllAsync();
//            return mapper.Map<IEnumerable<CategoryResponse>>(categories);
//        }
//    }
//}
