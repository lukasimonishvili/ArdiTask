using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Exceptions;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class InsuranceRepository : IInsuranceRepository
    {
        private readonly DataBaseContext _context = new();

        public string AddInsuranceToDb(Insurance insurance)
        {
            _context.Insurances.Add(insurance);
            _context.SaveChanges();
            return "Success";
        }

        public void DeleteInsurance(int id)
        {
            var insurance = _context.Insurances.FirstOrDefault(x => x.Id == id);
            if (insurance is null)
            {
                throw new DataNotFoundException($"isnurance with id: {id} doesn`t exist in db ");
            }

            _context.Insurances.Remove(insurance);
            _context.SaveChanges();
        }

        public IEnumerable<Insurance> GetAllInsurances()
        {
            return _context.Insurances;
        }

        public Insurance GetInsuranceById(int id)
        {
            var insurance = _context.Insurances.FirstOrDefault(x => x.Id == id);
            return insurance;
        }

        public IEnumerable<Insurance> GetInsurancesByUserId(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user is null)
            {
                throw new DataNotFoundException($"usre with id: {userId} doesn`t exist in database");
            }
            var userWithInsurances = _context.Users
                    .Include(u => u.UserInsurances)
                    .ThenInclude(ui => ui.Insurance)
                    .FirstOrDefault(u => u.Id == userId);

            if (userWithInsurances != null)
            {
                var insurances = userWithInsurances.UserInsurances
                    .Select(ui => ui.Insurance);
                return insurances;
            }

            return null;
        }

        public void UpdateInsurance(Insurance newInsurance)
        {
            var oldInsurance = _context.Insurances.FirstOrDefault(i => i.Id == newInsurance.Id);
            if (oldInsurance is null)
            {
                throw new DataNotFoundException($"insurance with id: {newInsurance.Id} doesn`t exists in db");
            }

            _context.Entry(oldInsurance).CurrentValues.SetValues(newInsurance);
            _context.SaveChanges();
        }
    }
}
