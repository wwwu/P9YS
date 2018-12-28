using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using P9YS.EntityFramework.Models;
using P9YS.Services;
using System.Linq;

namespace P9YS.Services.Base
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            #region User

            CreateMap<EntityFramework.Models.User, User.Dto.CurrentUser>()
                .ForMember(s => s.UserId, opt => opt.MapFrom(u => u.Id));

            #endregion

            #region Carousel

            CreateMap<EntityFramework.Models.Carousel, Carousel.Dto.CarouselOutput>();
            CreateMap<Carousel.Dto.Carouselnput, EntityFramework.Models.Carousel>();
                //.ForAllMembers(opt => opt.Condition((s, d, sourceMember) => sourceMember != null)); //忽略空值映射

            #endregion

            #region MovieRecommend

            CreateMap<EntityFramework.Models.MovieRecommend, MovieRecommend.Dto.MovieRecommendOutput>();
            CreateMap<MovieRecommend.Dto.MovieRecommendInput, EntityFramework.Models.MovieRecommend>()
                .ForMember(s => s.AddTime, option => option.MapFrom(s => DateTime.Now));

            #endregion

            #region Movie

            CreateMap<EntityFramework.Models.Movie, Movie.Dto.MovieInfoOutput>()
                .ForMember(s => s.MovieAreaName, option => option.MapFrom(m => m.MovieArea.Area))
                .ForMember(s => s.MovieGenres, option => option.MapFrom(m =>
                    m.MovieTypes.Select(g => g.MovieGenre)));

            #endregion

            #region MovieComment

            CreateMap<MovieComment.Dto.MovieCommentInput, EntityFramework.Models.MovieComment>()
                .BeforeMap((src,dest)=> dest.AddTime = dest.UpdTime = DateTime.Now);

            #endregion

            #region MovieQuestion

            CreateMap<MovieQuestion.Dto.QuestionInput, EntityFramework.Models.MovieQuestion>()
                .BeforeMap((src, dest) => dest.AddTime = dest.UpdTime = DateTime.Now);

            CreateMap<MovieQuestion.Dto.QuestionAnswerInput, EntityFramework.Models.MovieQuestionAnswer>()
                .BeforeMap((src, dest) => dest.AddTime = dest.UpdTime = DateTime.Now);

            //CreateMap<EntityFramework.Models.MovieQuestion, MovieQuestion.Dto.QuestionOutput>()
            //    .ForMember(s => s.Content, opt => opt.MapFrom(t => t.Content.Replace("\r\n", "<br/>")));
            //    //.AfterMap((src, dest) => dest.Content = src.Content.Replace("\r\n", "<br/>"));

            #endregion
        }
    }
}
