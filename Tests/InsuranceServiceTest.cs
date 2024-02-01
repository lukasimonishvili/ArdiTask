using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Exceptions;
using Infrastructure.Services;
using Moq;

namespace Tests
{
    [TestClass]
    public class InsuranceServiceTest
    {
        private Mock<IInsuranceRepository> _MockInsuraceRepository;

        [TestMethod]
        public void Add_Insurance_Success()
        {
            SetupAddInsuranceData();

            var inusranceService = new InsuranceService(_MockInsuraceRepository.Object);
            var isnurance = new InsuranceDTO()
            {
                Title = "Test",
                Description = "Test",
                Price = 1
            };
            var response = inusranceService.AddInsurance(isnurance);

            Assert.AreEqual("success", response);
        }

        [TestMethod]
        [ExpectedException(typeof(CustomValidateException))]
        public void Add_Insurance_Null_Title_Fail()
        {
            SetupAddInsuranceData();
            var inusranceService = new InsuranceService(_MockInsuraceRepository.Object);
            var isnurance = new InsuranceDTO()
            {
                Title = null,
                Description = "Test",
                Price = 1
            };
            inusranceService.AddInsurance(isnurance);
        }

        [TestMethod]
        [ExpectedException(typeof(CustomValidateException))]
        public void Add_Insurance_Empty_String_Title_Fail()
        {
            SetupAddInsuranceData();
            var inusranceService = new InsuranceService(_MockInsuraceRepository.Object);
            var isnurance = new InsuranceDTO()
            {
                Title = "",
                Description = "Test",
                Price = 1
            };
            inusranceService.AddInsurance(isnurance);
        }

        [TestMethod]
        [ExpectedException(typeof(CustomValidateException))]
        public void Add_Insurance_Null_Description_Fail()
        {
            SetupAddInsuranceData();
            var inusranceService = new InsuranceService(_MockInsuraceRepository.Object);
            var isnurance = new InsuranceDTO()
            {
                Title = "Test",
                Description = null,
                Price = 1
            };
            inusranceService.AddInsurance(isnurance);
        }

        [TestMethod]
        [ExpectedException(typeof(CustomValidateException))]
        public void Add_Insurance_Empty_String_Description_Fail()
        {
            SetupAddInsuranceData();
            var inusranceService = new InsuranceService(_MockInsuraceRepository.Object);
            var isnurance = new InsuranceDTO()
            {
                Title = "Test",
                Description = "",
                Price = 1
            };
            inusranceService.AddInsurance(isnurance);
        }

        [TestMethod]
        [ExpectedException(typeof(CustomValidateException))]
        public void Add_Insurance_Null_Price_Fail()
        {
            SetupAddInsuranceData();
            var inusranceService = new InsuranceService(_MockInsuraceRepository.Object);
            var isnurance = new InsuranceDTO()
            {
                Title = "Test",
                Description = "Test",
            };
            inusranceService.AddInsurance(isnurance);
        }

        [TestMethod]
        [ExpectedException(typeof(CustomValidateException))]
        public void Add_Insurance_Price_Zero_Or_Less_Fail()
        {
            SetupAddInsuranceData();
            var inusranceService = new InsuranceService(_MockInsuraceRepository.Object);
            var isnurance = new InsuranceDTO()
            {
                Title = "Test",
                Description = "Test",
                Price = 0
            };
            inusranceService.AddInsurance(isnurance);
        }

        [TestMethod]
        public void Get_All_Insurances_Success()
        {
            SetUpGetAllInsuranceData();
            var inusranceService = new InsuranceService(_MockInsuraceRepository.Object);

            var result = inusranceService.GetAllInsurances();

            Assert.IsInstanceOfType(result, typeof(List<InsuranceGetDTO>));
        }

        [TestMethod]
        public void Get_Insurances_By_User_Id_Success()
        {
            _MockInsuraceRepository = new Mock<IInsuranceRepository>();
            _MockInsuraceRepository.Setup(mock => mock.GetInsurancesByUserId(It.IsAny<int>())).Returns(Enumerable.Empty<Insurance>);
            var inusranceService = new InsuranceService(_MockInsuraceRepository.Object);

            var result = inusranceService.GetInsurancesByUserId("1");

            Assert.IsInstanceOfType(result, typeof(List<InsuranceGetDTO>));
        }


        [TestMethod]
        [ExpectedException(typeof(NotIntegerException))]
        public void Get_Insurances_By_User_Id_Not_Integer_Id_Fail()
        {
            _MockInsuraceRepository = new Mock<IInsuranceRepository>();
            _MockInsuraceRepository.Setup(mock => mock.GetInsurancesByUserId(It.IsAny<int>())).Returns(Enumerable.Empty<Insurance>);
            var inusranceService = new InsuranceService(_MockInsuraceRepository.Object);

            inusranceService.GetInsurancesByUserId("stringId");
        }

        [TestMethod]
        [ExpectedException(typeof(DataNotFoundException))]
        public void Get_Insurances_By_User_Id_User_Not_Found_Fail()
        {
            _MockInsuraceRepository = new Mock<IInsuranceRepository>();
            _MockInsuraceRepository.Setup(mock => mock.GetInsurancesByUserId(It.IsAny<int>())).Throws(new DataNotFoundException("usre with id: 1 doesn`t exist in database"));
            var inusranceService = new InsuranceService(_MockInsuraceRepository.Object);

            inusranceService.GetInsurancesByUserId("1");
        }

        [TestMethod]
        public void Get_Insurance_By_Id_Success()
        {
            _MockInsuraceRepository = new Mock<IInsuranceRepository>();
            _MockInsuraceRepository.Setup(mock => mock.GetInsuranceById(It.IsAny<int>())).Returns(new Insurance());
            var inusranceService = new InsuranceService(_MockInsuraceRepository.Object);

            var result = inusranceService.GetInsuranceById(1);

            Assert.IsInstanceOfType(result, typeof(InsuranceGetDTO));
        }

        [TestMethod]
        [ExpectedException(typeof(DataNotFoundException))]
        public void Get_Insurance_By_Id_Data_Not_Found_Fail()
        {
            _MockInsuraceRepository = new Mock<IInsuranceRepository>();
            _MockInsuraceRepository.Setup(mock => mock.GetInsuranceById(It.IsAny<int>()));
            var inusranceService = new InsuranceService(_MockInsuraceRepository.Object);

            inusranceService.GetInsuranceById(1);
        }

        [TestMethod]
        public void Update_Insurance_Success()
        {
            _MockInsuraceRepository = new Mock<IInsuranceRepository>();
            _MockInsuraceRepository.Setup(mock => mock.UpdateInsurance(It.IsAny<Insurance>()));
            var inusranceService = new InsuranceService(_MockInsuraceRepository.Object);

            var result = inusranceService.UpdateInsurance(new InsuranceUpdateDTO());

            Assert.AreEqual(result, "success");
        }

        [TestMethod]
        [ExpectedException(typeof(DataNotFoundException))]
        public void Update_insurance_Data_Not_Found_Fail()
        {
            _MockInsuraceRepository = new Mock<IInsuranceRepository>();
            _MockInsuraceRepository.Setup(mock => mock.UpdateInsurance(It.IsAny<Insurance>())).Throws(new DataNotFoundException("insurance with id: 1 doesn`t exists in db"));
            var inusranceService = new InsuranceService(_MockInsuraceRepository.Object);

            inusranceService.UpdateInsurance(new InsuranceUpdateDTO() { Id = 1 });
        }

        [TestMethod]
        public void Delete_Insurance_Success()
        {
            _MockInsuraceRepository = new Mock<IInsuranceRepository>();
            _MockInsuraceRepository.Setup(mock => mock.DeleteInsurance(It.IsAny<int>()));
            var inusranceService = new InsuranceService(_MockInsuraceRepository.Object);

            var result = inusranceService.DeleteInsurance(1);

            Assert.AreEqual(result, "success");
        }

        [TestMethod]
        [ExpectedException(typeof(DataNotFoundException))]
        public void Delete_Insurance_Data_Not_Found_Fail()
        {
            _MockInsuraceRepository = new Mock<IInsuranceRepository>();
            _MockInsuraceRepository.Setup(mock => mock.DeleteInsurance(It.IsAny<int>())).Throws(new DataNotFoundException(""));
            var inusranceService = new InsuranceService(_MockInsuraceRepository.Object);

            inusranceService.DeleteInsurance(1);
        }

        private void SetupAddInsuranceData()
        {
            _MockInsuraceRepository = new Mock<IInsuranceRepository>();
            _MockInsuraceRepository.Setup(mock => mock.AddInsuranceToDb(It.IsAny<Insurance>())).Returns("success");
        }

        private void SetUpGetAllInsuranceData()
        {
            _MockInsuraceRepository = new Mock<IInsuranceRepository>();
            _MockInsuraceRepository.Setup(mock => mock.GetAllInsurances()).Returns(Enumerable.Empty<Insurance>);
        }
    }
}
