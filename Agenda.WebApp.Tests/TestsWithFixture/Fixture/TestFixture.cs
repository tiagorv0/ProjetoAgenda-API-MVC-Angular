using Agenda.WebApp.Tests.TestsWithFixture.Helper;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Agenda.WebApp.Tests.TestsWithFixture.Fixture
{
    public class TestFixture : IDisposable
    {
        public IWebDriver Driver { get; private set; }

        public TestFixture()
        {
            Driver = new ChromeDriver(TestHelper.PathTest);
            Driver.Manage().Window.Size = new System.Drawing.Size(1920, 1080);
        }

        public void Dispose()
        {
            Driver.Quit();
        }
    }
}
