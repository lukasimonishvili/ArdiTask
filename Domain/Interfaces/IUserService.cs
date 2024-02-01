using Domain.DTO;

namespace Domain.Interfaces
{
    public interface IUserService
    {
        string AddUser(UserDTO user);
        string AddInsuranceToUser(string userId, string InsuranceId);
        UserGetDTO GetUserById(int userId);
        string UpdateUser(UserUpdaetDTO user);
        string DeleteInsuranceForUser(string UserId, string InsuranceId);
        string DeleteUser(int UsertId);
    }
}
