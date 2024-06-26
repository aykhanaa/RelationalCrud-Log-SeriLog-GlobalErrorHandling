using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Repository.Repositories.Interfaces;
using Services.DTOs.Admin.Countries;
using Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CountryService> _logger;   


     public CountryService(ICountryRepository countryRepository, IMapper mapper, ILogger<CountryService> logger)
        {

            _countryRepository = countryRepository;
            _mapper = mapper;
            _logger = logger;
            
        }

        public async Task CreateAsync(CountryCreateDto model)
        {
            
            if (model == null)
            {
                throw new ArgumentNullException(); 
            }
                await _countryRepository.CreateAsync(_mapper.Map<Country>(model));
          
        }

        public async Task DeleteAsync(int? id)
        {
            if (id == null)
            {
                _logger.LogWarning("Id is null");
                throw new ArgumentNullException();
            }

            var existCountry = await _countryRepository.GetById((int)id);

            if (existCountry == null)
            {
                _logger.LogWarning("data not found");

                throw new NullReferenceException();

            }


            _countryRepository.DeleteAsync(existCountry);
        }

        public async  Task EditAsync(int? id, CountryEditDto model)
        {
            //if (id == null) throw new ArgumentNullException();

            ArgumentNullException.ThrowIfNull(nameof(id));

            var existCountry = await _countryRepository.GetById((int)id);

            if (existCountry == null) throw new NullReferenceException();

                _mapper.Map(model, existCountry);

            await _countryRepository.EditAsync(existCountry);
        }

        public async  Task<IEnumerable<CountryDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<CountryDto>>(await _countryRepository.GetAllAsync());
        }

        public async Task<CountryDto> GetByIdAsync(int? id)
        {
           if(id == null) throw new ArgumentNullException();

            var existCountry = await _countryRepository.GetById((int)id);

            if (existCountry == null) throw new NullReferenceException();

            return _mapper.Map<CountryDto>(existCountry);

        }
    }
}
