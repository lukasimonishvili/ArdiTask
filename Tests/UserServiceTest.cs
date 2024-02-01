using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Exceptions;
using Infrastructure.Services;
using Moq;

namespace Tests
{
    [TestClass]
    public class UserServiceTest
    {
        private Mock<IUserRepository> _MockUserRepository;

        [TestMethod]
        public void Add_User_To_DB_Success()
        {
            SetUpAddUserMockData();

            var userService = new UserService(_MockUserRepository.Object);
            var user = new UserDTO()
            {
                Email = "test@test.com",
                FirstName = "john",
                LastName = "Doe",
            };
            var result = userService.AddUser(user);

            Assert.AreEqual(result, "success");
        }

        [TestMethod]
        [ExpectedException(typeof(CustomValidateException))]
        public void Add_User_Null_Email_Fail()
        {
            SetUpAddUserMockData();
            var userService = new UserService(_MockUserRepository.Object);
            var user = new UserDTO()
            {
                Email = null,
                FirstName = "john",
                LastName = "Doe",
            };

            userService.AddUser(user);
        }

        [TestMethod]
        [ExpectedException(typeof(CustomValidateException))]
        public void Add_User_Empty_Email_Fail()
        {
            SetUpAddUserMockData();
            var userService = new UserService(_MockUserRepository.Object);
            var user = new UserDTO()
            {
                Email = "",
                FirstName = "john",
                LastName = "Doe",
            };

            userService.AddUser(user);
        }

        [TestMethod]
        [ExpectedException(typeof(CustomValidateException))]
        public void Add_User_Null_FirstName_Fail()
        {
            SetUpAddUserMockData();
            var userService = new UserService(_MockUserRepository.Object);
            var user = new UserDTO()
            {
                Email = "Test@test.com",
                FirstName = null,
                LastName = "Doe",
            };

            userService.AddUser(user);
        }

        [TestMethod]
        [ExpectedException(typeof(CustomValidateException))]
        public void Add_User_Empty_FirstName_Fail()
        {
            SetUpAddUserMockData();
            var userService = new UserService(_MockUserRepository.Object);
            var user = new UserDTO()
            {
                Email = "test@test.com",
                FirstName = "",
                LastName = "Doe",
            };

            userService.AddUser(user);
        }

        [TestMethod]
        [ExpectedException(typeof(CustomValidateException))]
        public void Add_User_Null_Lastname_Fail()
        {
            SetUpAddUserMockData();
            var userService = new UserService(_MockUserRepository.Object);
            var user = new UserDTO()
            {
                Email = "test@test.com",
                FirstName = "john",
                LastName = null,
            };

            userService.AddUser(user);
        }

        [TestMethod]
        [ExpectedException(typeof(CustomValidateException))]
        public void Add_User_Empty_Lastname_Fail()
        {
            SetUpAddUserMockData();
            var userService = new UserService(_MockUserRepository.Object);
            var user = new UserDTO()
            {
                Email = "test@test.com",
                FirstName = "john",
                LastName = "",
            };

            userService.AddUser(user);
        }


        [TestMethod]
        public void Add_Insurance_To_user_Success()
        {
            _MockUserRepository = new Mock<IUserRepository>();
            _MockUserRepository.Setup(mock => mock.AddInsuranceToUser(It.IsAny<int>(), It.IsAny<int>()));
            var userService = new UserService(_MockUserRepository.Object);

            var result = userService.AddInsuranceToUser("1", "1");

            Assert.AreEqual(result, "Insurance Added to user");
        }

        [TestMethod]
        [ExpectedException(typeof(DataNotFoundException))]
        public void Add_Insurance_To_user_Data_Not_Found_Fail()
        {
            _MockUserRepository = new Mock<IUserRepository>();
            _MockUserRepository.Setup(mock => mock.AddInsuranceToUser(It.IsAny<int>(), It.IsAny<int>())).Throws(new DataNotFoundException(""));
            var userService = new UserService(_MockUserRepository.Object);

            userService.AddInsuranceToUser("1", "1");
        }

        [TestMethod]
        [ExpectedException(typeof(DataExistsException))]
        public void Add_Insurance_To_user_Data_Exists_Fail()
        {
            _MockUserRepository = new Mock<IUserRepository>();
            _MockUserRepository.Setup(mock => mock.AddInsuranceToUser(It.IsAny<int>(), It.IsAny<int>())).Throws(new DataExistsException(""));
            var userService = new UserService(_MockUserRepository.Object);

            userService.AddInsuranceToUser("1", "1");
        }

        [TestMethod]
        [ExpectedException(typeof(NotIntegerException))]
        public void Add_Insurance_To_user_Not_Integer_Ids_Fail()
        {
            _MockUserRepository = new Mock<IUserRepository>();
            _MockUserRepository.Setup(mock => mock.AddInsuranceToUser(It.IsAny<int>(), It.IsAny<int>())).Throws(new NotIntegerException(""));
            var userService = new UserService(_MockUserRepository.Object);

            userService.AddInsuranceToUser("1", "1");
        }

        [TestMethod]
        public void Get_User_by_Id_Success()
        {
            _MockUserRepository = new Mock<IUserRepository>();
            _MockUserRepository.Setup(mock => mock.GetuerById(It.IsAny<int>())).Returns(new UserGetDTO());
            var userService = new UserService(_MockUserRepository.Object);

            var result = userService.GetUserById(1);
            Assert.IsInstanceOfType(result, typeof(UserGetDTO));
        }

        [TestMethod]
        [ExpectedException(typeof(DataNotFoundException))]
        public void Get_User_By_Id_Data_Not_Found_Fail()
        {
            _MockUserRepository = new Mock<IUserRepository>();
            _MockUserRepository.Setup(mock => mock.GetuerById(It.IsAny<int>())).Throws(new DataNotFoundException(""));
            var userService = new UserService(_MockUserRepository.Object);

            userService.GetUserById(1);
        }

        [TestMethod]
        public void Update_User_Success()
        {
            _MockUserRepository = new Mock<IUserRepository>();
            _MockUserRepository.Setup(mock => mock.UpdateUser(It.IsAny<User>()));
            var userService = new UserService(_MockUserRepository.Object);

            var result = userService.UpdateUser(new UserUpdaetDTO());

            Assert.AreEqual(result, "success");
        }

        [TestMethod]
        [ExpectedException(typeof(DataNotFoundException))]
        public void Update_User_Data_Not_Found_Fail()
        {
            _MockUserRepository = new Mock<IUserRepository>();
            _MockUserRepository.Setup(mock => mock.UpdateUser(It.IsAny<User>())).Throws(new DataNotFoundException(""));
            var userService = new UserService(_MockUserRepository.Object);

            userService.UpdateUser(new UserUpdaetDTO());
        }

        [TestMethod]
        public void Delete_Insurance_For_User_Success()
        {
            _MockUserRepository = new Mock<IUserRepository>();
            _MockUserRepository.Setup(mock => mock.DeleteInsuraceForUser(It.IsAny<int>(), It.IsAny<int>()));
            var userService = new UserService(_MockUserRepository.Object);

            var result = userService.DeleteInsuranceForUser("1", "1");

            Assert.AreEqual(result, "success");
        }

        [TestMethod]
        [ExpectedException(typeof(DataNotFoundException))]
        public void Delete_Insurance_For_User_Data_Not_Found_Fail()
        {
            _MockUserRepository = new Mock<IUserRepository>();
            _MockUserRepository.Setup(mock => mock.DeleteInsuraceForUser(It.IsAny<int>(), It.IsAny<int>())).Throws(new DataNotFoundException(""));
            var userService = new UserService(_MockUserRepository.Object);

            userService.DeleteInsuranceForUser("1", "1");
        }

        [TestMethod]
        [ExpectedException(typeof(NotIntegerException))]
        public void Delete_Insurance_For_User_Not_Integer_Ids_Fail()
        {
            _MockUserRepository = new Mock<IUserRepository>();
            _MockUserRepository.Setup(mock => mock.DeleteInsuraceForUser(It.IsAny<int>(), It.IsAny<int>())).Throws(new NotIntegerException(""));
            var userService = new UserService(_MockUserRepository.Object);

            userService.DeleteInsuranceForUser("abc", "abc");
        }

        [TestMethod]
        public void Delete_User_Success()
        {
            _MockUserRepository = new Mock<IUserRepository>();
            _MockUserRepository.Setup(mock => mock.DeleteUser(It.IsAny<int>()));
            var userService = new UserService(_MockUserRepository.Object);

            var result = userService.DeleteUser(1);

            Assert.AreEqual(result, "success");
        }

        [TestMethod]
        [ExpectedException(typeof(DataNotFoundException))]
        public void Delete_User_Data_Not_Found_Fail()
        {
            _MockUserRepository = new Mock<IUserRepository>();
            _MockUserRepository.Setup(mock => mock.DeleteUser(It.IsAny<int>())).Throws(new DataNotFoundException(""));
            var userService = new UserService(_MockUserRepository.Object);

            userService.DeleteUser(1);
        }

        public void SetUpAddUserMockData()
        {
            _MockUserRepository = new Mock<IUserRepository>();
            _MockUserRepository.Setup(mock => mock.AddUserToDB(It.IsAny<User>()));
        }
    }
}
