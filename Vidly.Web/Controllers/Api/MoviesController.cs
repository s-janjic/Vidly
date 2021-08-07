using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Vidly.Web.Dtos;
using Vidly.Web.Models;

namespace Vidly.Web.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/movies
        [HttpGet]
        public IHttpActionResult GetMovies()
        {
            // returning the list of movies and map to DTO
            return Ok(_context.Movies.ToList().Select(Mapper.Map<Movie, MovieDto>));
        }

        // GET /api/movies/1
        [HttpGet]
        public IHttpActionResult GetMovie(int id)
        {
            // try to get movie from DB
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            // check if not found
            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        // Create a new movie
        // POST /api/movies
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            // we check if the mode state is valid
            if (!ModelState.IsValid)
                return BadRequest();

            // map dto to movie
            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            // add to DB and save changes
            _context.Movies.Add(movie);
            _context.SaveChanges();

            // add new ID from movie to dto
            movieDto.Id = movie.Id;

            // return created status that contains a URI location + new movie ID and context as moviedto
            return Created(new Uri(Request.RequestUri + "/" + movieDto.Id), movieDto);
        }
    }
}
