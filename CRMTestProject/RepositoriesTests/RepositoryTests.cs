using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoBSCRM.Repositories;
using NoBSCRM.Utils;
using Xunit;

namespace CRMTestProject.RepositoriesTests
{
    public class RepositoryTests
    {
        [Fact]
        public void GetAllTest()
        {
            var reader = new XMLReader();

            IRepository repository = new Repository();

            var list = repository.GetAll(reader);

            Assert.NotNull(list);
        }
    }
}
