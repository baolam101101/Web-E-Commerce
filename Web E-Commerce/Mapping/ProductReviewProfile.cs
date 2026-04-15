using AutoMapper;
using Web_E_Commerce.DTOs.ProductPreview.Responses;
using Web_E_Commerce.Models;

namespace Web_E_Commerce.Mapping
{
    public class ProductReviewProfile : Profile
    {
        public ProductReviewProfile()
        {
            CreateMap<ProductReview, ReviewResponse>()
                .ForMember(dest => dest.UserName,
                    opt => opt.MapFrom(src => src.User != null ? src.User.UserName : string.Empty));
        }
    }
}
