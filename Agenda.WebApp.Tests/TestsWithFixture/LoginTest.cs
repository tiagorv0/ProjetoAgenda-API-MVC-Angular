using Agenda.WebApp.Tests.TestsWithFixture.Fixture;
using Agenda.WebApp.Tests.TestsWithFixture.Helper;
using Agenda.WebApp.Tests.TestsWithFixture.PageObjects;
using OpenQA.Selenium;

namespace Agenda.WebApp.Tests.TestsWithFixture
{
    [Collection(TestHelper.CollectionName)]
    public class LoginTest
    {
        private IWebDriver driver;
        private LoginPO loginPO;

        public LoginTest(TestFixture fixture)
        {
            driver = fixture.Driver;
            loginPO = new LoginPO(driver);
            loginPO.Visit();
        }

        [Fact]
        public void ShouldGoHomeWhenValidLogin()
        {
            loginPO.FillForm("teste1", "123456");
            loginPO.SubmitForm();

            Thread.Sleep(1000);
            Assert.Contains("Agenda Application", driver.PageSource);
        }

        [Fact]
        public void ShouldGiveErroWhenInvalidLogin()
        {
            loginPO.FillForm("teste1", "123");
            loginPO.SubmitForm();

            Thread.Sleep(1000);
            Assert.Contains("Usuário ou Senha inválidos", driver.PageSource);
        }

    }
}

