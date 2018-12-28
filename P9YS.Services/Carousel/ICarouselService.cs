using System.Collections.Generic;
using System.Threading.Tasks;
using P9YS.Services.Carousel.Dto;

namespace P9YS.Services.Carousel
{
    public interface ICarouselService
    {
        Task<List<CarouselOutput>> GetCarouselsAsync();
        Task<PagingOutput<EntityFramework.Models.Carousel>> GetCarouselsAsync(PagingInput pagingInput);
        Task<Result> AddCarouselAsync(Carouselnput carouselnput);
        Task<Result> UpdCarouselAsync(Carouselnput carouselnput);
    }
}