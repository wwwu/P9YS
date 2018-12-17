using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using P9YS.EntityFramework;
using P9YS.Services.MovieResource.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P9YS.Services.MovieResource
{
    public class MovieResourceService : IMovieResourceService
    {
        private readonly MovieResourceContext _movieResourceContext;
        public MovieResourceService(MovieResourceContext movieResourceContext)
        {
            _movieResourceContext = movieResourceContext;
        }

        public async Task<List<MovieResourceOutput>> GetMovieResources(int movieId)
        {
            var movieResources = await _movieResourceContext.MovieResources
                .Where(s => s.MovieId == movieId)
                .OrderBy(s => s.Size)
                .ProjectTo<MovieResourceOutput>()
                .AsNoTracking()
                .ToListAsync();
            movieResources.ForEach(item =>
            {
                var size = int.Parse(item.Size);
                item.Size = size < 1024 ? item.Size + "MB" : (size / 1024m).ToString("0.00") + "GB";
            });

            return movieResources;
        }

        public async Task<List<MovieOnlinePlayOutput>> GetMovieOnlinePlays(int movieId)
        {
            var movieOnlinePlays = await _movieResourceContext.MovieOnlinePlays
                .Where(s => s.MovieId == movieId)
                .ProjectTo<MovieOnlinePlayOutput>()
                .AsNoTracking()
                .ToListAsync();
            return movieOnlinePlays;
        }
    }
}
