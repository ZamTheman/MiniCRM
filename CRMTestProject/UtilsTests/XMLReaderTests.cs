using System.Collections.Generic;
using System.Threading.Tasks;
using NoBSCRM.Models;
using NoBSCRM.Utils;
using Xunit;

namespace CRMTestProject.UtilsTests
{
    public class XMLReaderTests
    {
        [Fact]
        public void GetAllTest()
        {
            var reader = new XMLReader();

            var list = reader.GetAll(); // Not using the same local storage as normal app. Find a workaround

            Assert.NotNull(list);
        }

    }
}
