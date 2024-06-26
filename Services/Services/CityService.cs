using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.Repositories.Interfaces;
using Services.DTOs.Admin.Cities;
using Services.Helpers.Exceptions;
using Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public  class CityService  : ICityService    
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;
        private readonly ICountryRepository _countryRepository;
        private readonly ILogger _logger;


        public CityService(ICityRepository cityRepository, IMapper mapper, ICountryRepository countryRepository, ILogger<CityService> logger)
        {

            _cityRepository = cityRepository;
            _mapper = mapper;
            _countryRepository = countryRepository;
            _logger = logger;
        }

        public async  Task CreateAsync(CityCreateDto model)
        {
            var existCountry = await _countryRepository.GetById(model.CountryId);

            if(existCountry == null)
            {
                _logger.LogWarning($"Country is not found - {model.CountryId + "-" + DateTime.Now.ToString() }");
                throw new NotFoundException($"Id - {model.CountryId }  country notfound");

            }

            await _cityRepository.CreateAsync(_mapper.Map<City>(model));
        }

        public async  Task<IEnumerable<CityDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<CityDto>>(await _cityRepository.GetAllWithCountryAsync());
        }

        public async  Task<CityDto> GetByIdAsync(int id)
        {
            var data = _cityRepository.FindBy(m => m.Id == id , m => m.Country);

            return  _mapper.Map<CityDto>(data.FirstOrDefault());
        }

        public async  Task<CityDto> GetByNameAsync(string name)
        {
            if (name == null) throw new ArgumentNullException("name");  

            var data = _cityRepository.FindBy(m => m.Name == name, m => m.Country);

            return _mapper.Map<CityDto>(data.FirstOrDefault());
        }
    }
}
