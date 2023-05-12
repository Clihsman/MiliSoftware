using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace UnitTestWpfUI
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            /*
            SqlLiteDatabase database = new SqlLiteDatabase();
            database.Open();
            database.ExecuteNomQuery("SELECT * FROM Customers");
            database.Close();
            */
            Assert.AreEqual(true, false);
            Console.WriteLine("Metodo no funciona");
        }
    }
}
