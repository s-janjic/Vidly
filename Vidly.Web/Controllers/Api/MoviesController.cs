using System;
using System.Linq;
using System.Net;
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

        // Update an existing movie
        // PUT /api/movies/1
        [HttpPut]
        public void UpdateMovie(int id, MovieDto movieDto)
        {
            // check model if isvalid
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            // try to get movie by id
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            // check if we found one
            if (movie == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            // map new values to the existing movie
            // it will output to the existing movie (as reference to original object in db)
            // after we save, it will be updated
            Mapper.Map(movieDto, movie);

            _context.SaveChanges();
        }

        // Delete an existing movie
        // DELETE /api/movies/1
        public void DeleteMovie(int id)
        {
            // try to get movie
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            // check if we found one
            if (movie == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            // remove from db and save
            _context.Movies.Remove(movie);
            _context.SaveChanges();
        }
    }
}
