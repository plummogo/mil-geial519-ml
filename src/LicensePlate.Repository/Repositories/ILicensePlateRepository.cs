using LicensePlate.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LicensePlate.Repository.Repositories
{
    public interface ILicensePlateRepository
    {
        Task<List<Plate>> GetAll();
        Task<Plate> GetById(int id);
        Task<Plate> Insert(Plate plateToInsert);
        Task<Plate> Update(Plate plateToUpdate);
        Task<Plate> UpdateIsActive(int id);
        Task<int> Delete(int id);
    }
}