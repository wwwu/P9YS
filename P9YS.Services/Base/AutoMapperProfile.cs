using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using P9YS.EntityFramework.Models;
using P9YS.Services;
using System.Linq;
using P9YS.Common;

namespace P9YS.Services.Base
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile(AppSettings appSettings)
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

            CreateMap<EntityFramework.Models.Movie, Movie.Dto.Movie_Manage_Output>()
                .ForMember(s => s.MovieTypes, opt => opt.Ignore())
                .ForMember(s => s.MovieSeries, opt => opt.Ignore());

            CreateMap<Movie.Dto.Movie_Manage_Input, EntityFramework.Models.Movie>()
                .ForMember(s => s.MovieTypes, opt => opt.Ignore())
                .ForMember(s => s.MovieArea, opt => opt.Ignore())
                .ForMember(s => s.MovieOrigins, opt => opt.Ignore())
                .ForMember(s => s.MovieComments, opt => opt.Ignore())
                .ForMember(s => s.MovieQuestions, opt => opt.Ignore());

            CreateMap<MovieDraft.Dto.MovieDraftDetailInput, EntityFramework.Models.Movie>()
                .BeforeMap((src, dest) => dest.AddTime = dest.UpdTime = DateTime.Now)
                .ForMember(s => s.ScoreSum, opt => opt.MapFrom(m => m.Score * m.ScoreCount));

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

            #region MovieArea

            CreateMap<MovieArea.Dto.MovieAreaInput, EntityFramework.Models.MovieArea>()
                .ForMember(s => s.AddTime, option => option.MapFrom(s => DateTime.Now));

            #endregion  

            #region MovieGenre

            CreateMap<MovieGenres.Dto.MoiveGenreInput, EntityFramework.Models.MovieGenre>()
                .ForMember(s => s.AddTime, option => option.MapFrom(s => DateTime.Now));

            CreateMap<string, MovieType>()
                .ForMember(s => s.MovieGenreId, opt => opt.MapFrom(m => int.Parse(m)));

            #endregion

            #region MovieResource

            CreateMap<MovieResource.Dto.MovieResourceInput, EntityFramework.Models.MovieResource>()
                .ForMember(s => s.AddTime, option => option.MapFrom(s => DateTime.Now))
                .ForMember(s => s.UpdTime, option => option.MapFrom(s => DateTime.Now));

            #endregion

            #region MovieOnlinePlay

            CreateMap<MovieResource.Dto.MovieOnlinePlayOutput, MovieOnlinePlay>();

            #endregion
        }
    }
}
