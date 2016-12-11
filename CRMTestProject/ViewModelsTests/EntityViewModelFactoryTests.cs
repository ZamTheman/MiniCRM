using System;
using ModelLayer;
using ViewModels;
using Xunit;

namespace CRMTestProject.ViewModelsTests
{
    public class EntityViewModelFactoryTests
    {
        [Fact]
        public void GetEntityViewModelTest()
        {
            var factory = new EntityViewModelFactory();
            IEntity todo = new Todo()
            {
                Date = new DateTime(2016-10-12),
                Description = "Test description"
            };
            IEntity todo2 = new Todo()
            {
                Date = new DateTime(2016-10-13),
                Description = "Test description2"
            };
            IEntity employee = new Employee()
            {
                Name = "Lisa",
                Phone = "021-354131"
            };

            var vm1 = factory.GetEntityViewModel("Todo", todo);
            var vm2 = factory.GetEntityViewModel("Employee", employee);
            var vm3 = factory.GetEntityViewModel("Todo", todo2);

            Assert.NotNull(vm1);
            Assert.NotNull(vm2);
            Assert.NotSame(vm1, vm2);
            Assert.NotSame(vm1, vm3);
        }
    }
}
