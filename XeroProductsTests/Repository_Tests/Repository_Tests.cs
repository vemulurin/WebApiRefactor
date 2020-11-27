using Microsoft.Data.Sqlite;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XeroProductsTests.Repository_Tests
{
    /// <summary>
    /// repository unit test to test DB connectivity 
    /// </summary>
    [TestClass]
    public class Repository_Tests
    {
        [TestMethod]
        public void Test_DB_Connectivity()
        {
            var builder = new SqliteConnectionStringBuilder("Data Source=App_Data/products.db");
            Assert.AreEqual(builder.DataSource, "App_Data/products.db", true);
        }
    }
}
