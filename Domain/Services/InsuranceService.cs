using Application.Validators;
using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;
using Infrastructure.Exceptions;
using Mapster;

namespace Infrastructure.Services
{
    public class InsuranceService : IInsuranceService
    {
        private readonly IInsuranceRepository _insuranceRepository;

        public InsuranceService(IInsuranceRepository insuranceRepository)
        {
            _insuranceRepository = insuranceRepository;
        }

        public string AddInsurance(InsuranceDTO insurance)
        {
            var Validator = new InsuranceValidator().Validate(insurance);
            if (!Validator.IsValid)
            {
                var message = Validator.Errors.Count > 1 ? "More then 1 validation error detected" : Validator.Errors[0].ErrorMessage;
                throw new ValidationException(message);
            }

            var adaptInsurance = insurance.Adapt<Insurance>();
            _insuranceRepository.AddInsuranceToDb(adaptInsurance);
            return "success";
        }

        public void DeleteInsurance(int id)
        {
            _insuranceRepository.DeleteInsurance(id);
        }

        public List<InsuranceGetDTO> GetAllInsurances()
        {
            var insurances = _insuranceRepository.GetAllInsurances().ToList();
            var adaptInsurances = insurances.Adapt<List<InsuranceGetDTO>>();
            return adaptInsurances;
        }

        public InsuranceGetDTO GetInsuranceById(int id)
        {
            var insurance = _insuranceRepository.GetInsuranceById(id).Adapt<InsuranceGetDTO>();
            if (insurance is null)
            {
                throw new DataNotFoundException($"Insurance with id: {id} not found in system");
            }
            return insurance;
        }

        public List<InsuranceGetDTO> GetInsurancesByUserId(string userId)
        {
            try
            {
                var intUserId = Convert.ToInt32(userId);
                var insurances = _insuranceRepository.GetInsurancesByUserId(intUserId);

                if (insurances is null)
                {
                    return new List<InsuranceGetDTO>();
                }
                else
                {
                    var adaptInsurances = insurances.ToList().Adapt<List<InsuranceGetDTO>>();
                    return adaptInsurances;
                }
            }
            catch (DataNotFoundException ex)
            {
                throw new DataNotFoundException(ex.Message);
            }
            catch
            {
                throw new NotIntegerException("provided Id is not integer");
            }
        }

        public void UpdateInsurance(InsuranceUpdateDTO insurance)
        {
            var adaptedInsurance = insurance.Adapt<Insurance>();
            _insuranceRepository.UpdateInsurance(adaptedInsurance);
        }
    }
}
