using OpenQA.Selenium;

namespace Agenda.WebApp.Tests.TestsWithFixture.PageObjects
{
    public class LoginPO
    {
        private IWebDriver driver;
        private By inputUsername;
        private By inputPassword;
        private By loginbtn;

        public LoginPO(IWebDriver driver)
        {
            this.driver = driver;
            inputUsername = By.Id("username");
            inputPassword = By.Id("password");
            loginbtn = By.Id("loginbtn");
        }

        public void Visit() => driver.Navigate().GoToUrl("http://localhost:4200/login");

        public void SubmitForm() => driver.FindElement(loginbtn).Click();

        public void FillForm(string username, string password)
        {
            driver.FindElement(inputUsername).SendKeys(username);
            driver.FindElement(inputPassword).SendKeys(password);
        }
    }
}
