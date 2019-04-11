using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using P9YS.EntityFramework.Models;
using P9YS.Services;
using System.Linq;
using P9YS.Common;
using P9YS.Services.MovieArea.Dto;
using P9YS.Services.MovieComment.Dto;
using P9YS.Services.MovieDraft.Dto;
using P9YS.Services.Movie.Dto;
using P9YS.Services.MovieGenres.Dto;
using P9YS.Services.MovieQuestion.Dto;
using P9YS.Services.MovieRecommend.Dto;
using P9YS.Services.MovieResource.Dto;

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

            CreateMap<EntityFramework.Models.Carousel, Carousel.Dto.Carousel_Output>();
            CreateMap<Carousel.Dto.Carousel_Input, EntityFramework.Models.Carousel>();
            //.ForAllMembers(opt => opt.Condition((s, d, sourceMember) => sourceMember != null)); //忽略空值映射

            #endregion

            #region MovieRecommend

            CreateMap<EntityFramework.Models.MovieRecommend, MovieRecommend_Output>()
                .ForMember(s => s.MovieImgUrl, option => option.MapFrom(s => appSettings.TxCos.CosDomain + s.Movie.ImgUrl))
                .ForMember(s => s.MovieActor, option => option.MapFrom(s => s.Movie.Actor.Replace("\r\n","、")));
            CreateMap<MovieRecommend_Input, EntityFramework.Models.MovieRecommend>()
                .ForMember(s => s.AddTime, option => option.MapFrom(s => DateTime.Now));

            #endregion

            #region Movie

            CreateMap<EntityFramework.Models.Movie, MovieInfo_Output>()
                .ForMember(s => s.MovieAreaName, option => option.MapFrom(m => m.MovieArea.Area))
                .ForMember(s => s.MovieGenres, option => option.MapFrom(m =>
                    m.MovieTypes.Select(g => g.MovieGenre)));

            CreateMap<EntityFramework.Models.Movie, Movie_Manage_Output>()
                .ForMember(s => s.MovieTypes, opt => opt.Ignore())
                .ForMember(s => s.MovieSeries, opt => opt.Ignore());

            CreateMap<Movie_Manage_Input, EntityFramework.Models.Movie>()
                .ForMember(s => s.MovieTypes, opt => opt.Ignore())
                .ForMember(s => s.MovieArea, opt => opt.Ignore())
                .ForMember(s => s.MovieOrigins, opt => opt.Ignore())
                .ForMember(s => s.MovieComments, opt => opt.Ignore())
                .ForMember(s => s.MovieQuestions, opt => opt.Ignore());

            CreateMap<MovieDraft_Detail_Input, EntityFramework.Models.Movie>()
                .BeforeMap((src, dest) => dest.AddTime = dest.UpdTime = DateTime.Now)
                .ForMember(s => s.ScoreSum, opt => opt.MapFrom(m => m.Score * m.ScoreCount))
                .ForMember(s => s.MovieResources, opt => opt.Ignore());

            #endregion

            #region MovieComment

            CreateMap<MovieComment_Input, EntityFramework.Models.MovieComment>()
                .BeforeMap((src,dest)=> dest.AddTime = dest.UpdTime = DateTime.Now);

            #endregion

            #region MovieQuestion

            CreateMap<Question_Input, EntityFramework.Models.MovieQuestion>()
                .BeforeMap((src, dest) => dest.AddTime = dest.UpdTime = DateTime.Now);

            CreateMap<QuestionAnswer_Input, MovieQuestionAnswer>()
                .BeforeMap((src, dest) => dest.AddTime = dest.UpdTime = DateTime.Now);

            //CreateMap<EntityFramework.Models.MovieQuestion, MovieQuestion.Dto.QuestionOutput>()
            //    .ForMember(s => s.Content, opt => opt.MapFrom(t => t.Content.Replace("\r\n", "<br/>")));
            //    //.AfterMap((src, dest) => dest.Content = src.Content.Replace("\r\n", "<br/>"));

            #endregion

            #region MovieArea

            CreateMap<MovieArea_Input, EntityFramework.Models.MovieArea>()
                .ForMember(s => s.AddTime, option => option.MapFrom(s => DateTime.Now));

            #endregion  

            #region MovieGenre

            CreateMap<MoiveGenre_Input, MovieGenre>()
                .ForMember(s => s.AddTime, option => option.MapFrom(s => DateTime.Now));

            CreateMap<string, MovieType>()
                .ForMember(s => s.MovieGenreId, opt => opt.MapFrom(m => int.Parse(m)));

            #endregion

            #region MovieResource

            CreateMap<MovieResource_Input, EntityFramework.Models.MovieResource>()
                .ForMember(s => s.AddTime, option => option.MapFrom(s => DateTime.Now))
                .ForMember(s => s.UpdTime, option => option.MapFrom(s => DateTime.Now));

            #endregion

            #region MovieOnlinePlay

            CreateMap<MovieOnlinePlay_Output, MovieOnlinePlay>();

            #endregion
        }
    }
}
