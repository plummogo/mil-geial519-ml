using LicensePlate.Domain;
using LicensePlate.Repository.Context;
using LicensePlate.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace LicensePlate.Repository
{
    public class LicensePlateRepository : ILicensePlateRepository
    {
        private readonly LicensePlateContext _dbContext;
        private readonly ILogger<ILicensePlateRepository> _logger;

        public LicensePlateRepository(ILogger<ILicensePlateRepository> logger)
        {
            _dbContext = new LicensePlateContext();
            _logger = logger;
        }

        public async Task<int> Delete(int id)
        {
            try
            {
                _logger.LogInformation($"{MethodInfo.GetCurrentMethod().Name} has been started");

                var foundPlate = await GetPlateById(id);

                _dbContext.Remove(foundPlate);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation($"{MethodInfo.GetCurrentMethod().Name} has been finished");

                return id;
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

                var plates = await _dbContext.LicensePlates.ToListAsync();

                _logger.LogInformation($"{MethodInfo.GetCurrentMethod().Name} has been finished");

                return plates;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{MethodInfo.GetCurrentMethod().Name} throws error: {ex.Message}");

                throw new Exception(ex.Message);
            }
        }

        public async Task<Plate> GetById(int id)
        {
            try
            {
                _logger.LogInformation($"{MethodInfo.GetCurrentMethod().Name} has been started");

                var foundPlate = await GetPlateById(id);

                _logger.LogInformation($"{MethodInfo.GetCurrentMethod().Name} has been finished");

                return foundPlate;

            }
            catch (Exception ex)
            {
                _logger.LogError($"{MethodInfo.GetCurrentMethod().Name} throws error: {ex.Message}");

                throw new Exception(ex.Message);
            }
        }

        public async Task<Plate> Insert(Plate plateToInsert)
        {
            try
            {
                _logger.LogInformation($"{MethodInfo.GetCurrentMethod().Name} has been started");

                _dbContext.LicensePlates.Add(plateToInsert);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation($"{MethodInfo.GetCurrentMethod().Name} has been finished");

                return plateToInsert;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{MethodInfo.GetCurrentMethod().Name} throws error: {ex.Message}");

                throw new Exception(ex.Message);
            }
        }

        public async Task<Plate> Update(Plate plateToUpdate)
        {
            try
            {
                _logger.LogInformation($"{MethodInfo.GetCurrentMethod().Name} has been started");

                var originalPlate = await GetPlateById(plateToUpdate.Id);

                _dbContext.Entry(originalPlate).CurrentValues.SetValues(plateToUpdate);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation($"{MethodInfo.GetCurrentMethod().Name} has been finished");

                return plateToUpdate;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{MethodInfo.GetCurrentMethod().Name} throws error: {ex.Message}");

                throw new Exception(ex.Message);
            }
        }

        public async Task<Plate> UpdateIsActive(int id)
        {
            try
            {
                _logger.LogInformation($"{MethodInfo.GetCurrentMethod().Name} has been started");

                var originalPlate = await GetPlateById(id);

                originalPlate.IsActive = true;
                originalPlate.UpdatedDate = DateTime.UtcNow;
                _dbContext.Entry(originalPlate).State = EntityState.Modified;
                
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation($"{MethodInfo.GetCurrentMethod().Name} has been finished");

                return originalPlate;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{MethodInfo.GetCurrentMethod().Name} throws error: {ex.Message}");

                throw new Exception(ex.Message);
            }
        }

        private async Task<Plate> GetPlateById(int id)
        {
            var foundPlate = await _dbContext.LicensePlates.FirstOrDefaultAsync(p => p.Id == id);

            if (foundPlate == null)
            {
                var errorMessage = $"{MethodInfo.GetCurrentMethod().Name} throws error: Selected id: {id} was not found!";

                _logger.LogError(errorMessage);

                throw new NullReferenceException(errorMessage);
            }

            return foundPlate;
        }
    }
}
