using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Agenda.WebApp.Tests.TestsWithOutFixture
{
    public class LogisTest
    {
        private IWebDriver driver;

        public LogisTest()
        {
            driver = new ChromeDriver(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            driver.Navigate().GoToUrl("http://localhost:4200/login");
        }

        [Fact]
        public void ShouldGoHomeWhenValidLogin()
        {
            driver.FindElement(By.Id("username")).SendKeys("teste1");
            driver.FindElement(By.Id("password")).SendKeys("123456");
            driver.FindElement(By.Id("loginbtn")).Click();

            Thread.Sleep(1000);
            Assert.Contains("Agenda Application", driver.PageSource);
        }

        [Fact]
        public void ShouldGiveErroWhenInvalidLogin()
        {
            driver.FindElement(By.Id("username")).SendKeys("teste1");
            driver.FindElement(By.Id("password")).SendKeys("123");
            driver.FindElement(By.Id("loginbtn")).Click();

            Thread.Sleep(1000);
            Assert.Contains("Usuário ou Senha inválidos", driver.PageSource);
        }
    }
}
