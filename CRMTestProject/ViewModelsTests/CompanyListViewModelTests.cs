using System.Threading.Tasks;
using ModelLayer;
using Repositories;
using Utils;
using ViewModels;
using Xunit;

namespace CRMTestProject.ViewModelsTests
{
    public class CompanyListViewModelTests
    {
        [Fact]
        public void DeleteCommandTests()
        {
            // Arrange
            var reader = new StubIReader();
            var writer = new StubIWriter();
            var repository = new StubIRepository();
            var company = new StubICompany();
            var mv = new CompanyListViewModel(repository, reader, writer);
            repository.DeleteCompany((IWriter writer2, Company company2) => Task.CompletedTask);

            // Act
            mv.DeleteCommand.Execute(company);


        }

    }
}
