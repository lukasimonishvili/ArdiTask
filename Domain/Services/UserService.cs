using Application.Validators;
using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Exceptions;
using Mapster;


namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string AddInsuranceToUser(string userId, string InsuranceId)
        {
            try
            {
                var userInId = Convert.ToInt32(userId);
                var insuranceIntId = Convert.ToInt32(InsuranceId);
                _userRepository.AddInsuranceToUser(userInId, insuranceIntId);
                return "Insurance Added to user";
            }
            catch (DataNotFoundException ex)
            {
                throw new DataNotFoundException(ex.Message);
            }
            catch (DataExistsException ex)
            {
                throw new DataExistsException(ex.Message);
            }
            catch
            {
                throw new NotIntegerException("one of the provided id is not integer");
            }
        }

        public string AddUser(UserDTO user)
        {
            var Validator = new UserValidator().Validate(user);
            if (!Validator.IsValid)
            {
                var message = Validator.Errors.Count > 1 ? "More then 1 validation error detected" : Validator.Errors[0].ErrorMessage;
                throw new ValidateException(message);
            }
            var adaptUser = user.Adapt<User>();
            _userRepository.AddUserToDB(adaptUser);
            return "Success";
        }

        public void DeleteInsuranceForUser(string UserId, string InsuranceId)
        {
            try
            {
                var intUserId = Convert.ToInt32(UserId);
                var intInsuranceId = Convert.ToInt32(InsuranceId);

                _userRepository.DeleteInsuraceForUser(intUserId, intInsuranceId);
            }
            catch (DataNotFoundException ex)
            {
                throw new DataExistsException(ex.Message);
            }
            catch
            {
                throw new NotIntegerException("one of the provided ids is not integer");
            }
        }

        public void DeleteUser(int UsertId)
        {
            _userRepository.DeleteUser(UsertId);
        }

        public UserGetDTO GetUserById(int userId)
        {
            return _userRepository.GetuerById(userId);
        }

        public void UpdateUser(UserUpdaetDTO user)
        {
            var adaptUser = user.Adapt<User>();
            _userRepository.UpdateUser(adaptUser);
        }
    }
}
