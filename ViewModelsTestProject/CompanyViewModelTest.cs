using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NUnit.Framework;
using ModelLayer;
using Moq;
using Repositories;
using Utils;
using ViewModels;

namespace ViewModelsTestProject
{
    [TestFixture]
    public class CompanyViewModelTests : TestBase<CompanyViewModel>
    {
        private Mock<IReader> mockReader;
        private Mock<IWriter> mockWriter;
        private Mock<IRepository> mockRepository;
        private Mock<IEntity> mockEntity;

        protected override void SetupConstructorRequiredMocks()
        {
            mockReader = CreateMock<IReader>();
            mockWriter = CreateMock<IWriter>();
            mockRepository = CreateMock<IRepository>();
            mockEntity = CreateMock<IEntity>();
        }

        protected override void SetupMocksAfterConstructions()
        {

        }

        [Test]
        public void CreateViewModelTest_Always_NotNull()
        {
            // Arrange

            // Act
            var vm = new CompanyViewModel(mockRepository.Object, mockReader.Object, mockWriter.Object);
            
            // Assert
            Assert.NotNull(vm);
        }

        [Test]
        public void DeleteCommandCanExecuteWithoutSelectedCompany_Always_False()
        {
            // Arrange
            var company = new Company();
            var vm = new CompanyViewModel(mockRepository.Object, mockReader.Object, mockWriter.Object);

            // Act

            // Assert
            Assert.False(vm.DeleteCommand.CanExecute(company));
        }

        [Test]
        public void DeleteCommandCanExecteWithoutSelectedCompany_Always_False()
        {
            // Arrange
            var vm = new CompanyViewModel(mockRepository.Object, mockReader.Object, mockWriter.Object);

            // Act

            // Assert
            Assert.That(vm.DeleteCommand.CanExecute(null), Is.False);
        }

        [Test]
        public void SaveCommandCanExectuteWhenSelectedCompanyIsNullAndAllFieldsAreEmpty_Always_Fasle()
        {
            // Arrange
            var vm = new CompanyViewModel(mockRepository.Object, mockReader.Object, mockWriter.Object);

            // Act
            vm.SelectedCompany = null;

            // Assert
            Assert.That(vm.SaveCompanyCommand.CanExecute(null), Is.False);
        }

        [Test]
        public void SaveCommandCanExectuteWhenSelectedCompanyIsNotNullButFieldsAreIdenticalToSelectedCompany_Always_Fasle()
        {
            // Arrange
            var vm = new CompanyViewModel(mockRepository.Object, mockReader.Object, mockWriter.Object);

            // Act
            vm.SelectedCompany = new Company() {Name = "MockName", City = "MockCity", Phone = "MockPhone", Street = "MockStreet"};
            vm.CompanyName = vm.SelectedCompany.Name;
            vm.CompanyCity = vm.SelectedCompany.City;
            vm.CompanyPhone = vm.SelectedCompany.Phone;
            vm.CompanyStreet = vm.SelectedCompany.Street;

            // Assert
            Assert.That(vm.SaveCompanyCommand.CanExecute(null), Is.False);
        }

        [Test]
        public void SaveCommandCanExectuteWhenSelectedCompanyIsNotNullButFieldsDifferentToSelectedCompany_Always_True()
        {
            // Arrange
            var vm = new CompanyViewModel(mockRepository.Object, mockReader.Object, mockWriter.Object);

            // Act
            vm.SelectedCompany = new Company() { Name = "MockName", City = "MockCity", Phone = "MockPhone", Street = "MockStreet" };
            vm.CompanyName = vm.SelectedCompany.Name;
            vm.CompanyCity = "Different then mockCity";
            vm.CompanyPhone = vm.SelectedCompany.Phone;
            vm.CompanyStreet = vm.SelectedCompany.Street;

            // Assert
            Assert.That(vm.SaveCompanyCommand.CanExecute(null), Is.True);
        }

        // 
        // Tested with a Selected Company
        //
        [Test]
        public void SaveCommandShouldCallMockRepositorySave_Always_Verified()
        {
            // Arrange
            var company = new Company() { Name = "MockName", City = "MockCity", Phone = "MockPhone", Street = "MockStreet" };
            var vm = new CompanyViewModel(mockRepository.Object, mockReader.Object, mockWriter.Object);
            mockRepository.Setup(m => m.Save(mockWriter.Object, It.IsAny<Company>())).Returns(Task.FromResult<int>(3));

            vm.SelectedCompany = company;
            vm.CompanyName = vm.SelectedCompany.Name;
            vm.CompanyCity = vm.SelectedCompany.Name;
            vm.CompanyPhone = vm.SelectedCompany.Phone;
            vm.CompanyStreet = vm.SelectedCompany.Street;

            // Act
            vm.SaveCompanyCommand.Execute(company);

            // Assert
            mockRepository.VerifyAll();
        }

        // 
        // Tested with Selected Company null
        //
        [Test]
        public void SaveCommandShouldCallMockRepositorySaveWithNullSelectedCompany_Always_Verified()
        {
            // Arrange
            var company = new Company() { Name = "MockName", City = "MockCity", Phone = "MockPhone", Street = "MockStreet" };
            var vm = new CompanyViewModel(mockRepository.Object, mockReader.Object, mockWriter.Object);
            mockRepository.Setup(m => m.Save(mockWriter.Object, It.IsAny<Company>())).Returns(Task.FromResult<int>(3));

            vm.SelectedCompany = null;
            vm.CompanyName = "MockName";

            // Act
            vm.SaveCompanyCommand.Execute(company);

            // Assert
            mockRepository.VerifyAll();
        }

        [Test]
        public void DeleteCommandCanExecteWitSelectedCompany_Always_True()
        {
            // Arrange
            var vm = new CompanyViewModel(mockRepository.Object, mockReader.Object, mockWriter.Object);

            // Act
            vm.SelectedEntity = mockEntity.Object;

            // Assert
            Assert.That(vm.DeleteCommand.CanExecute(null), Is.True);
        }

        [Test]
        public void EntitySelectedCommandCanExecture_Always_True()
        {
            // Arrange
            var vm = new CompanyViewModel(mockRepository.Object, mockReader.Object, mockWriter.Object);

            // Act

            // Assert
            Assert.That(vm.EntitySelectedCommand.CanExecute(null), Is.True);
        }

        [Test]
        public void EmployeeListActiveCommandTooggleCanExcute_Always_True()
        {
            // Arrange
            var vm = new CompanyViewModel(mockRepository.Object, mockReader.Object, mockWriter.Object);
            
            // Act

            // Assert
            Assert.That(vm.EmplyeeListActiveCommand.CanExecute(null), Is.True);
        }

        [Test]
        public void TodoListActiveCommandTooggleCanExcute_Always_True()
        {
            // Arrange
            var vm = new CompanyViewModel(mockRepository.Object, mockReader.Object, mockWriter.Object);

            // Act

            // Assert
            Assert.That(vm.TodoListActiveCommand.CanExecute(null), Is.True);
        }

        [Test]
        public void HistoryListActiveCommandTooggleCanExcute_Always_True()
        {
            // Arrange
            var vm = new CompanyViewModel(mockRepository.Object, mockReader.Object, mockWriter.Object);

            // Act

            // Assert
            Assert.That(vm.HistoryListActiveCommand.CanExecute(null), Is.True);
        }
    }
}
