using Domain.DTO;

namespace Domain.Interfaces
{
    public interface IUserService
    {
        string AddUser(UserDTO user);
        string AddInsuranceToUser(string userId, string InsuranceId);
        UserGetDTO GetUserById(int userId);
        void UpdateUser(UserUpdaetDTO user);
        void DeleteInsuranceForUser(string UserId, string InsuranceId);
        void DeleteUser(int UsertId);
    }
}
