using LicensePlate.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LicensePlate.Service.Interfaces
{
    public interface ILicensePlateService
    {
        Task<List<Plate>> GetAll();
        Task<Plate> GetLicensePlateById(int id);
        Task<Plate> GenerateLicensePlate(Plate plate);
        Task<Plate> UpdateLicensePlate(Plate plate);
        Task<Plate> UpdateLicensePlateIsActiveById(int id);
        Task<int> DeletePlateById(int id);
    }
}