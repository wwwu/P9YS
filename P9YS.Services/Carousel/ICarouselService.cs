using System.Collections.Generic;
using System.Threading.Tasks;
using P9YS.Services.Carousel.Dto;

namespace P9YS.Services.Carousel
{
    public interface ICarouselService
    {
        Task<List<Carousel_Output>> GetCarousels();
        Task<PagingOutput<EntityFramework.Models.Carousel>> GetCarousels(PagingInput pagingInput);
        Task<Result> AddCarousel(Carousel_Input carouselnput);
        Task<Result> UpdCarousel(Carousel_Input carouselnput);
    }
}