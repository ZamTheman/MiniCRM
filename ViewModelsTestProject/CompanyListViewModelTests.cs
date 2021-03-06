﻿using System.Collections.Generic;
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
    public class CompanyListViewModelTests : TestBase<CompanyListViewModel>
    {
        private Mock<IReader> mockReader;
        private Mock<IWriter> mockWriter;
        private Mock<IRepository> mockRepository;

        protected override void SetupConstructorRequiredMocks()
        {
            mockReader = CreateMock<IReader>();
            mockWriter = CreateMock<IWriter>();
            mockRepository = CreateMock<IRepository>();
        }

        protected override void SetupMocksAfterConstructions()
        {   
        }

        [Test]
        public void CreateViewModelTest()
        {
            // Arrange

            // Act
            var vm = new CompanyListViewModel(mockRepository.Object, mockReader.Object, mockWriter.Object);

            // Assert
            Assert.NotNull(vm);
            Assert.IsTrue(vm.SelectedCompany == null);
        }

        [Test]
        public void DeleteCommandTestWithoutSelectedCompanyCanExecute_Always_False()
        {
            // Arrange
            var company = new Company();
            var vm = new CompanyListViewModel(mockRepository.Object, mockReader.Object, mockWriter.Object);

            // Act

            // Assert
            Assert.False(vm.DeleteCommand.CanExecute(company));
        }

        [Test]
        public void DeleteCommandTestWithSelectedCompanyCanExecute_Always_True()
        {
            // Arrange
            var company = new Company();
            var vm = new CompanyListViewModel(mockRepository.Object, mockReader.Object, mockWriter.Object);

            // Act
            vm.SelectedCompany = company;

            // Assert
            Assert.That(vm.DeleteCommand.CanExecute(company));
        }

        [Test]
        public void AddCommandTest_Always_SetsSelectedCompanyToNull()
        {
            // Arrange
            var vm = new CompanyListViewModel(mockRepository.Object, mockReader.Object, mockWriter.Object);

            // Act
            vm.AddCommand.Execute(null);

            // Assert
            Assert.That(vm.SelectedCompany, Is.Null);
        }

        [Test]
        public void SendCompanyCommandTest()
        {
            // Arrange
            var company = new Company();
            var vm = new CompanyListViewModel(mockRepository.Object, mockReader.Object, mockWriter.Object);

            // Act

            // Assert
            vm.SendCompanyCommand.Execute(company);
        }

        [TestCase(3)]
        [TestCase(5)]
        public void AllCompanies_AfterConstruction_IsNotNull(int nrCompanies)
        {
            var listOfCompanies = new List<ICompany>();
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
            mockRepository.Setup(r => r.GetAll(mockReader.Object)).ReturnsAsync(listOfCompanies);
            var vm = new CompanyListViewModel(mockRepository.Object, mockReader.Object, mockWriter.Object);
            
            // Assert
            Assert.That(vm.AllCompanies, Is.Not.Null);
            Assert.That(vm.AllCompanies.Count, Is.EqualTo(nrCompanies));
            mockRepository.VerifyAll();
        }
    }
}
