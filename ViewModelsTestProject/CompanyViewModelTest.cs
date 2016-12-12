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
        public void DeleteCommandTestWithoutSelectedCompany()
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
        public void EmployeeListActiveCommandTooggleCanExcute_Always_Ture()
        {
            // Arrange
            var vm = new CompanyViewModel(mockRepository.Object, mockReader.Object, mockWriter.Object);
            
            // Act

            // Assert
            Assert.That(vm.EmplyeeListActiveCommand.CanExecute(null), Is.True);
        }

        [Test]
        public void TodoListActiveCommandTooggleCanExcute_Always_Ture()
        {
            // Arrange
            var vm = new CompanyViewModel(mockRepository.Object, mockReader.Object, mockWriter.Object);

            // Act

            // Assert
            Assert.That(vm.TodoListActiveCommand.CanExecute(null), Is.True);
        }

        [Test]
        public void HistoryListActiveCommandTooggleCanExcute_Always_Ture()
        {
            // Arrange
            var vm = new CompanyViewModel(mockRepository.Object, mockReader.Object, mockWriter.Object);

            // Act

            // Assert
            Assert.That(vm.HistoryListActiveCommand.CanExecute(null), Is.True);
        }
    }
}
