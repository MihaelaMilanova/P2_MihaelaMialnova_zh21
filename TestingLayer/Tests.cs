using DataLayer;
using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;

namespace TestingLayer;
[TestFixture]
    public class Tests
    {
    internal static MainDBContext dbContext;

    static Tests()
    {
        DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
        builder.UseInMemoryDatabase("TestDb");
        dbContext = new MainDBContext(builder.Options);
    }

    [OneTimeTearDown]
    public void Dispose()
    {
        dbContext.Dispose();
    }

    [Test]
        public void Test1()
        {
        int number = 42;
        Assert.That(number == 42, "Number is not 42!");
         }

        [Test]
        public void Test2()
        {
        int number = 10;
        Assert.That(number < 10, "Method does not return number less than 10!");
        }
}
