using Domain.DTO;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        void AddUserToDB(User user);
        void AddInsuranceToUser(int UserId, int InsuranceId);
        UserGetDTO GetuerById(int UserId);
        void UpdateUser(User newUser);
        void DeleteInsuraceForUser(int UserId, int InsuranceId);
        void DeleteUser(int UserId);
    }
}
