using System.Collections.Generic;
using System.Threading.Tasks;
using ModelLayer;
using NUnit.Framework;
using Moq;
using Repositories;
using Utils;
using ViewModels;

namespace ViewModelsTestProject
{
    [TestFixture]
    public class CompanyListViewModelTests
    {

        MockRepository repository = new MockRepository(MockBehavior.Strict);

        [Test]
        public void CreateViewModelTest()
        {
            // Arrange
            var repo = repository.Create<IRepository>();
            var writer = repository.Create<IWriter>();
            var reader = repository.Create<IReader>();

            // Act
            var vm = new CompanyListViewModel(repo.Object, reader.Object, writer.Object);

            // Assert
            Assert.NotNull(vm);
            Assert.IsTrue(vm.SelectedCompany == null);
            Assert.IsTrue(vm.AllCompanies == null);
        }

        [Test]
        public void DeleteCommandTestWithoutSelectedCompany()
        {
            // Arrange
            var repo = repository.Create<IRepository>();
            var writer = repository.Create<IWriter>();
            var reader = repository.Create<IReader>();
            var company = new Company();
            var vm = new CompanyListViewModel(repo.Object, reader.Object, writer.Object);

            // Act

            // Assert
            Assert.False(vm.DeleteCommand.CanExecute(company));
        }

        [Test]
        public void DeleteCommandTestWithSelectedCompany()
        {
            // Arrange
            var repo = repository.Create<IRepository>();
            var writer = repository.Create<IWriter>();
            var reader = repository.Create<IReader>();
            var company = new Company();
            var vm = new CompanyListViewModel(repo.Object, reader.Object, writer.Object);

            // Act
            vm.SelectedCompany = company;

            // Assert
            Assert.That(vm.DeleteCommand.CanExecute(company));
        }

        [Test]
        public void SendCompanyCommandTest()
        {
            // Arrange
            var repo = repository.Create<IRepository>();
            var writer = repository.Create<IWriter>();
            var reader = repository.Create<IReader>();
            var company = new Company();
            var vm = new CompanyListViewModel(repo.Object, reader.Object, writer.Object);

            // Act

            // Assert
            vm.SendCompanyCommand.Execute(company);
            repository.VerifyAll();
        }

        [TestCase(3)]
        [TestCase(5)]
        public async Task AllCompanies_AfterConstruction_IsNotNull(int nrCompanies)
        {
            var listOfCompanies = new List<Company>();
            for (int i = 0; i < nrCompanies; i++)
            {
                var comp = new Company()
                {
                    Name = $"Test {i}",
                    Id = i
                };

                listOfCompanies.Add(comp);
            }

            // Arrange
            var repo = repository.Create<IRepository>();
            var writer = repository.Create<IWriter>();
            var reader = repository.Create<IReader>();
            
            repo.Setup(r => r.GetAll(reader.Object)).ReturnsAsync(listOfCompanies);
            var vm = new CompanyListViewModel(repo.Object, reader.Object, writer.Object);
            
            // Assert
            Assert.That(vm.AllCompanies, Is.Not.Null);
            Assert.That(vm.AllCompanies.Count, Is.EqualTo(nrCompanies));
            repository.VerifyAll();
        }
    }
}
