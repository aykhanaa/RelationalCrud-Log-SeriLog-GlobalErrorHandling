using Microsoft.Extensions.DependencyInjection;
using Repository.Repositories.Interfaces;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Services.DTOs.Admin.Countries;
using FluentValidation;
using Services.Helpers;
using Services.Services;
using Services.Services.Interfaces;
namespace Services
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddServiceLayer(this IServiceCollection services)
        {

            services.AddAutoMapper(typeof(MappingProfile).Assembly);


            services.AddFluentValidationAutoValidation(config =>
            {
                config.DisableDataAnnotationsValidation = true;
            });


            services.AddScoped<IValidator<CountryCreateDto>,CountryCreateDtoValidator>();

            services.AddScoped<ICountryService, CountryService>();

            services.AddScoped<ICityService, CityService>();


            return services;

        }
    }
}
