using System.Collections.Generic;
using System.Threading.Tasks;
using P9YS.Services.Carousel.Dto;

namespace P9YS.Services.Carousel
{
    public interface ICarouselService
    {
        Task<List<CarouselOutput>> GetCarousels();
        Task<PagingOutput<EntityFramework.Models.Carousel>> GetCarousels(PagingInput pagingInput);
        Task<Result> AddCarousel(Carouselnput carouselnput);
        Task<Result> UpdCarousel(Carouselnput carouselnput);
    }
}