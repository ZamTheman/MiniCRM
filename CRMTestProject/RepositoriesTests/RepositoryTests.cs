using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ModelLayer;
using Repositories;
using Utils;
using Xunit;

namespace CRMTestProject.RepositoriesTests
{
    public class RepositoryTests
    {
        private List<Company> _mockCompanies;
        private Company _mockCompany;

        public RepositoryTests()
        {
            GenerateDummyData();
        }

        private void GenerateDummyData()
        {
            var listOfCompanies = new List<Company>();

            var company = new Company()
            {
                Name = "Test Company1",
                Id = 1,
                City = "Test City1",
                Street = "Test Street1",
                Phone = "555-158 28 28"
            };
            company.Employees.Add(new Employee()
            {
                Id = 1,
                Name = "Test Person 1",
                Phone = "555-158 28 29",
                Mobil = "555-258 28 29",
                EMail = "testperson1@fakemail.com",
                Position = "Test Position1"
            });
            company.Employees.Add(new Employee()
            {
                Id = 2,
                Name = "Test Person 2",
                Phone = "555-158 28 30",
                Mobil = "555-258 28 30",
                EMail = "testperson2@fakemail.com",
                Position = "Test Position2"
            });
            company.Histories.Add(new HistoryPost()
            {
                Id = 1,
                Date = new DateTime(2016 - 05 - 12),
                Post = "History post 1"
            });
            company.Histories.Add(new HistoryPost()
            {
                Id = 2,
                Date = new DateTime(2016 - 05 - 13),
                Post = "History post 2"
            });
            company.Todos.Add(new Todo()
            {
                Id = 1,
                Date = new DateTime(2016, 05, 14),
                Description = "Todo1"
            });
            company.Todos.Add(new Todo()
            {
                Id = 2,
                Date = new DateTime(2016, 05, 15),
                Description = "Todo2"
            });

            listOfCompanies.Add(company);

            company = new Company()
            {
                Name = "Test Company2",
                Id = 2,
                City = "Test City2",
                Street = "Test Street2",
                Phone = "555-158 28 28"
            };
            company.Employees.Add(new Employee()
            {
                Id = 1,
                Name = "Test Person 3",
                Phone = "555-158 28 32",
                Mobil = "555-258 28 32",
                EMail = "testperson3@fakemail.com",
                Position = "Test Position3"
            });
            company.Employees.Add(new Employee()
            {
                Id = 2,
                Name = "Test Person 4",
                Phone = "555-158 28 33",
                Mobil = "555-258 28 33",
                EMail = "testperson4@fakemail.com",
                Position = "Test Position4"
            });
            company.Histories.Add(new HistoryPost()
            {
                Id = 1,
                Date = new DateTime(2016 - 05 - 12),
                Post = "History post 3"
            });
            company.Histories.Add(new HistoryPost()
            {
                Id = 2,
                Date = new DateTime(2016 - 05 - 13),
                Post = "History post 4"
            });
            company.Todos.Add(new Todo()
            {
                Id = 1,
                Date = new DateTime(2016, 05, 14),
                Description = "Todo3"
            });
            company.Todos.Add(new Todo()
            {
                Id = 2,
                Date = new DateTime(2016, 05, 15),
                Description = "Todo4"
            });

            listOfCompanies.Add(company);

            _mockCompanies = listOfCompanies;
            _mockCompany = listOfCompanies[1];
        }
        
        [Fact]
        public void GetAllTest()
        {
            // Arrange
            var reader = new StubIReader();
            reader.GetAll(() => Task.FromResult<IList<Company>>(_mockCompanies));
            IRepository repository = new Repository();

            // Act
            var list = repository.GetAll(reader);

            // Assert
            Assert.NotNull(list);
            Assert.Equal(list.Result.Count, 2);
            Assert.Equal(list.Result[1].Name, _mockCompany.Name);
        }

        [Fact]
        public void DeleteCompanyTest()
        {
            //Arrange
            var writer = new StubIWriter();
            IRepository repository = new Repository();
            writer.DeleteSingleCompanyByIdAsync((int id) => Task.FromResult("Was called"));

            //Act
            var result = repository.DeleteCompany(writer, _mockCompany);

            //Assert
            Assert.NotNull(writer);
            Assert.Equal(result.IsCompleted, true);
        }

        [Fact]
        public void DeleteEntity()
        {
            //Arrange
            var writer = new StubIWriter();
            var entityToRemove = new StubIEntity();
            IRepository repository = new Repository();
            writer.DeleteSingleEntityByIdAsync((int id, IEntity entity) => Task.FromResult("Was called"));

            //Act
            var result = repository.DeleteEntity(writer, entityToRemove, _mockCompany);

            //Assert
            Assert.NotNull(writer);
            Assert.Equal(result.IsCompleted, true);
        }
    }
}
