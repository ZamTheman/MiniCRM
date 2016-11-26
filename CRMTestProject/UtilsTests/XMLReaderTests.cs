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

            var list = reader.GetAll();

            Assert.NotNull(list);
        }
    }
}
