
using Domain.DTO;

namespace Domain.Interfaces
{
    public interface IInsuranceService
    {
        string AddInsurance(InsuranceDTO insurance);
        List<InsuranceGetDTO> GetAllInsurances();
        List<InsuranceGetDTO> GetInsurancesByUserId(string userId);
        InsuranceGetDTO GetInsuranceById(int id);
        string UpdateInsurance(InsuranceUpdateDTO insurance);
        string DeleteInsurance(int id);
    }
}
