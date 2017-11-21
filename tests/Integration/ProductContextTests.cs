using Microsoft.VisualStudio.TestTools.UnitTesting;
using Api.Data;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationTests
{
    [TestClass]
    public class ProductContextTests
    {
        private string mongoConnection = "mongodb://localhost:27017";

        [TestMethod]
        public async Task Given_Two_Valid_Products_Name_When_Geting_Products_By_Name_Then_Return_Products_Objects()
        {
            var sut = new ProductContext(mongoConnection);

            var result = await sut.GetAllByNames(new[] { "a", "Batata" });

            Assert.IsNotNull(result);
        }

        public async Task Given_Two_Valid_Products_Name_When_Geting_Products_By_Name_Then_Ignore_Case_And_Return_Products_Objects()
        {
            var sut = new ProductContext(mongoConnection);

            var result = await sut.GetAllByNames(new[] { "A", "BaTaTA" });

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
        }
    }
}
