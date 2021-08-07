using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Vidly.Web.Dtos;
using Vidly.Web.Models;

namespace Vidly.Web.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // map customer to customerdto
            Mapper.CreateMap<Customer, CustomerDto>();

            // map customerdto to customer
            Mapper.CreateMap<CustomerDto, Customer>()
                .ForMember(c => c.Id, opt => opt.Ignore());

            // map movie to moviedto
            Mapper.CreateMap<Movie, MovieDto>();

            // map moviedto to movie
            Mapper.CreateMap<MovieDto, Movie>()
                .ForMember(m => m.Id, opt => opt.Ignore());
        }
    }
}