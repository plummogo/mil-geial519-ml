using LicensePlate.Domain;
using LicensePlate.Repository.Repositories;
using LicensePlate.Service.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace LicensePlate.Service
{
    public class LicensePlateService : ILicensePlateService
    {
        private readonly ILicensePlateRepository _licensePlateRepository;
        private readonly ILogger<ILicensePlateRepository> _logger;

        public LicensePlateService(ILicensePlateRepository licensePlateRepository, ILogger<ILicensePlateRepository> logger)
        {
            _licensePlateRepository = licensePlateRepository;
            _logger = logger;
        }

        public async Task<int> DeletePlateById(int id)
        {
            try
            {
                _logger.LogInformation($"{MethodInfo.GetCurrentMethod().Name} has been started");

                CheckParamNullOrEmpty(id);

                var response = await _licensePlateRepository.Delete(id);

                _logger.LogInformation($"{MethodInfo.GetCurrentMethod().Name} has been finished");

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{MethodInfo.GetCurrentMethod().Name} throws error: {ex.Message}");

                throw new Exception(ex.Message);
            }
        }

        public async Task<Plate> GenerateLicensePlate(Plate plate)
        {
            try
            {
                _logger.LogInformation($"{MethodInfo.GetCurrentMethod().Name} has been started");

                CheckParamNullOrEmpty(plate);

                var response = await _licensePlateRepository.Insert(plate);

                _logger.LogInformation($"{MethodInfo.GetCurrentMethod().Name} has been finished");

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{MethodInfo.GetCurrentMethod().Name} throws error: {ex.Message}");

                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Plate>> GetAll()
        {
            try
            {
                _logger.LogInformation($"{MethodInfo.GetCurrentMethod().Name} has been started");

                var response = await _licensePlateRepository.GetAll();

                _logger.LogInformation($"{MethodInfo.GetCurrentMethod().Name} has been finished");

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{MethodInfo.GetCurrentMethod().Name} throws error: {ex.Message}");

                throw new Exception(ex.Message);
            }
        }

        public async Task<Plate> GetLicensePlateById(int id)
        {
            try
            {
                _logger.LogInformation($"{MethodInfo.GetCurrentMethod().Name} has been started");

                CheckParamNullOrEmpty(id);

                var response = await _licensePlateRepository.GetById(id);

                _logger.LogInformation($"{MethodInfo.GetCurrentMethod().Name} has been finished");

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{MethodInfo.GetCurrentMethod().Name} throws error: {ex.Message}");

                throw new Exception(ex.Message);
            }
        }

        public async Task<Plate> UpdateLicensePlate(Plate plate)
        {
            try
            {
                _logger.LogInformation($"{MethodInfo.GetCurrentMethod().Name} has been started");

                CheckParamNullOrEmpty(plate);

                var response = await _licensePlateRepository.Update(plate);

                _logger.LogInformation($"{MethodInfo.GetCurrentMethod().Name} has been finished");

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{MethodInfo.GetCurrentMethod().Name} throws error: {ex.Message}");

                throw new Exception(ex.Message);
            }
        }

        public async Task<Plate> UpdateLicensePlateIsActiveById(int id)
        {
            try
            {
                _logger.LogInformation($"{MethodInfo.GetCurrentMethod().Name} has been started");

                CheckParamNullOrEmpty(id);

                var response = await _licensePlateRepository.UpdateIsActive(id);

                _logger.LogInformation($"{MethodInfo.GetCurrentMethod().Name} has been finished");

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{MethodInfo.GetCurrentMethod().Name} throws error: {ex.Message}");

                throw new Exception(ex.Message);
            }
        }

        private void CheckParamNullOrEmpty<T>(T value)
        {
            var isParamNullOrEmpty = typeof(T) == typeof(string) ? string.IsNullOrEmpty(value as string) : value == null || value.Equals(default(T));

            if (isParamNullOrEmpty)
            {
                var errorMessage = $"{MethodInfo.GetCurrentMethod().Name} throws error: Selected id: {value} was not found!";

                _logger.LogError(errorMessage);

                throw new NullReferenceException(errorMessage);
            }
        }
    }
}