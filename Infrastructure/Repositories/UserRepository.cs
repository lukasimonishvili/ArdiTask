using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Exceptions;
using Infrastructure.Persistence;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataBaseContext _context = new();

        public void AddInsuranceToUser(int UserId, int InsuranceId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == UserId);
            if (user is null)
            {
                throw new DataNotFoundException($"User with id: {UserId} doesn`t exist in database");
            }

            var insurance = _context.Insurances.FirstOrDefault(i => i.Id == InsuranceId);
            if (insurance is null)
            {
                throw new DataNotFoundException($"insurance with id: {InsuranceId} doesn`t exist in database");
            }

            var userInsuranceExists = _context.UserInsurances.Where(u => u.UserId == UserId && u.InsuranceId == InsuranceId);
            if (userInsuranceExists.Count() > 0)
            {
                throw new DataExistsException("User already has this insurance");
            }

            var userInsurance = new UserInsurance();
            userInsurance.UserId = UserId;
            userInsurance.InsuranceId = InsuranceId;

            _context.UserInsurances.Add(userInsurance);
            _context.SaveChanges();


        }

        public void AddUserToDB(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void DeleteInsuraceForUser(int UserId, int InsuranceId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == UserId);
            if (user is null)
            {
                throw new DataNotFoundException($"user with id: {UserId} doesn`t exist in db");
            }

            var insurance = _context.Insurances.FirstOrDefault(i => i.Id == InsuranceId);
            if (insurance is null)
            {
                throw new DataNotFoundException($"insurance with id: {InsuranceId} doesn`t exist in db");
            }

            var userInsurance = _context.UserInsurances.FirstOrDefault(x => x.UserId == UserId && x.InsuranceId == InsuranceId);
            if (userInsurance is null)
            {
                throw new DataNotFoundException("User doesn`t have this isnurance");
            }
            _context.UserInsurances.Remove(userInsurance);
            _context.SaveChanges();
        }

        public void DeleteUser(int UserId)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == UserId);
            if (user is null)
            {
                throw new DataNotFoundException($"user with id: {UserId} doesn`t exist in db");
            }

            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public UserGetDTO GetuerById(int UserId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == UserId);

            if (user is null)
            {
                throw new DataNotFoundException($"user with id: {UserId} doesn`t exist in db");
            }
            var adaptuser = user.Adapt<UserGetDTO>();

            var userWithInsurances = _context.Users
                    .Include(u => u.UserInsurances)
                    .ThenInclude(ui => ui.Insurance)
                    .FirstOrDefault(u => u.Id == UserId);

            if (userWithInsurances != null)
            {
                var insurances = userWithInsurances.UserInsurances
                    .Select(ui => ui.Insurance).ToList().Adapt<List<InsuranceGetDTO>>();
                adaptuser.Insurances = insurances;
            }

            return adaptuser;
        }

        public void UpdateUser(User newUser)
        {
            var oldUser = _context.Users.FirstOrDefault(u => u.Id == newUser.Id);
            if (oldUser is null)
            {
                throw new DataNotFoundException($"user with id: {newUser.Id} doesn`t exists in DB");
            }

            _context.Entry(oldUser).CurrentValues.SetValues(newUser);
            _context.SaveChanges();
        }
    }
}
