using IO.Swagger.Attributes;
using LicensePlate.Domain;
using LicensePlate.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace IO.Swagger.Controllers
{
    /// <summary>
    /// LicensePlateController responsible for handling CRUD endpoints
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LicensePlateApi : ControllerBase
    {
        private readonly ILogger<LicensePlateApi> _logger;
        private readonly ILicensePlateService _licensePlateService;
        
        public LicensePlateApi(ILicensePlateService licensePlateService, ILogger<LicensePlateApi> logger)
        {
            _licensePlateService = licensePlateService;
            _logger = logger;
        }

        /// <summary>
        /// Get plates
        /// </summary>
        /// <remarks>Responsible for getting all plates from database</remarks>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Invalid status value</response>
        [HttpGet]
        [Route("/api/v3/licenseplate/plates")]
        [ValidateModelState]
        [SwaggerOperation("GetLicensePlates")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<Plate>), description: "successful operation")]
        public async Task<IActionResult> GetLicensePlates()
        {
            try
            {
                _logger.LogInformation($"{MethodInfo.GetCurrentMethod().Name} has been started");
                
                var response = await _licensePlateService.GetAll();

                _logger.LogInformation($"{MethodInfo.GetCurrentMethod().Name} has been finished");
                
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{MethodInfo.GetCurrentMethod().Name} throws error: {ex.Message}");
                
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get plate
        /// </summary>
        /// <remarks>Responsible for getting a plate by id from database</remarks>
        /// <param name="id">Fetch Plate by id</param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Invalid status value</response>
        [HttpGet]
        [Route("/api/v3/licenseplate/{id}")]
        [ValidateModelState]
        [SwaggerOperation("GetPlateById")]
        [SwaggerResponse(statusCode: 200, type: typeof(Plate), description: "successful operation")]
        public async Task<IActionResult> GetLicensePlateById(int id)
        {
            try
            {
                _logger.LogInformation($"{MethodInfo.GetCurrentMethod().Name} has been started");

                var response = await _licensePlateService.GetLicensePlateById(id);

                _logger.LogInformation($"{MethodInfo.GetCurrentMethod().Name} has been finished");

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{MethodInfo.GetCurrentMethod().Name} throws error: {ex.Message}");

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Create plate
        /// </summary>
        /// <remarks>Responsible for creating a plate to database</remarks>
        /// <param name="plate">Plate model to create</param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Invalid status value</response>
        [HttpPost]
        [Route("/api/v3/licenseplate")]
        [ValidateModelState]
        [SwaggerOperation("AddPlate")]
        [SwaggerResponse(statusCode: 200, type: typeof(Plate), description: "Successful operation")]
        public async Task<IActionResult> GenerateLicensePlate([FromBody] Plate plate)
        {
            try
            {
                _logger.LogInformation($"{MethodInfo.GetCurrentMethod().Name} has been started");
                
                var response = await _licensePlateService.GenerateLicensePlate(plate);
                
                _logger.LogInformation($"{MethodInfo.GetCurrentMethod().Name} has been finished");

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{MethodInfo.GetCurrentMethod().Name} throws error: {ex.Message}");

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Update plate
        /// </summary>
        /// <remarks>Responsible for updating a plate by model. Affected all property </remarks>
        /// <param name="updateId">Plate id to update</param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Invalid status value</response>
        [HttpPut]
        [Route("/api/v3/licenseplate/update")]
        [ValidateModelState]
        [SwaggerOperation("UpdatePlate")]
        [SwaggerResponse(statusCode: 200, type: typeof(Plate), description: "Successful operation")]
        public async Task<IActionResult> UpdateLicensePlate([FromBody] Plate plate)
        {
            try
            {
                _logger.LogInformation($"{MethodInfo.GetCurrentMethod().Name} has been started");
                
                var response = await _licensePlateService.UpdateLicensePlate(plate);
                
                _logger.LogInformation($"{MethodInfo.GetCurrentMethod().Name} has been finished");

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{MethodInfo.GetCurrentMethod().Name} throws error: {ex.Message}");

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Update specific plate
        /// </summary>
        /// <remarks>Responsible for updating a plate by id and set isActive to true</remarks>
        /// <param name="updateId">Selected plate id</param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Invalid status value</response>
        [HttpPatch]
        [Route("/api/v3/licenseplate/update/{updateId}")]
        [ValidateModelState]
        [SwaggerOperation("UpdatePlateIsActiveById")]
        [SwaggerResponse(statusCode: 200, type: typeof(Plate), description: "Successful operation")]
        public async Task<IActionResult> UpdateLicensePlateIsActiveById(int updateId)
        {
            try
            {
                _logger.LogInformation($"{MethodInfo.GetCurrentMethod().Name} has been started");

                var response = await _licensePlateService.UpdateLicensePlateIsActiveById(updateId);

                _logger.LogInformation($"{MethodInfo.GetCurrentMethod().Name} has been finished");

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{MethodInfo.GetCurrentMethod().Name} throws error: {ex.Message}");

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete plate
        /// </summary>
        /// <remarks>Responsible for deleting a plate by id from database</remarks>
        /// <param name="deleteId">Plate id to delete</param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Invalid status value</response>
        [HttpDelete]
        [Route("/api/v3/licenseplate/delete/{deleteId}")]
        [ValidateModelState]
        [SwaggerOperation("DeletePlate")]
        public async Task<IActionResult> DeletePlateById(int deleteId)
        {
            try
            {
                _logger.LogInformation($"{MethodInfo.GetCurrentMethod().Name} has been started");

                var response = await _licensePlateService.DeletePlateById(deleteId);

                _logger.LogInformation($"{MethodInfo.GetCurrentMethod().Name} has been finished");
                
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{MethodInfo.GetCurrentMethod().Name} throws error: {ex.Message}");

                throw new Exception(ex.Message);
            }
        }
    }
}