using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IInsuranceRepository
    {
        void AddInsuranceToDb(Insurance insurance);
        IEnumerable<Insurance> GetAllInsurances();
        Insurance GetInsuranceById(int id);
        IEnumerable<Insurance> GetInsurancesByUserId(int userId);
        void UpdateInsurance(Insurance newInsurance);
        void DeleteInsurance(int id);
    }
}
