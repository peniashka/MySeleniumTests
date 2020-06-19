using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace SeleniunAdvancedTest.Pages
{
    public class BasePage
    {
        public BasePage(IWebDriver driver)
        {
            Driver = driver;
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
        }

        public IWebDriver Driver { get; }

        public WebDriverWait Wait { get; }

        public IWebElement ScrollTo(IWebElement element)
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
            return element;
        }

        public void WaitForLoad(int timeoutSec = 15)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            WebDriverWait wait = new WebDriverWait(Driver, new TimeSpan(0, 0, timeoutSec));
            wait.Until(wd => js.ExecuteScript("return document.readyState").ToString() == "complete");
        }


        public void CreateAccount(string email)
        {
            IWebElement buttonSignIn = Driver.FindElement(By.XPath("//*[@class = 'login']"));
            buttonSignIn.Click();

            //Random randomGenerator = new Random();
            //int randomInt = randomGenerator.Next(1000);
            //_email = "username" + randomInt + "@xxx.com";

            this.WaitForLoad();
            IWebElement fieldEmailAddress = Wait.Until<IWebElement>(d => d.FindElement(By.XPath("/html/body/div/div[2]/div/div[3]/div/div/div[1]/form/div/div[2]/input")));
            fieldEmailAddress.SendKeys(email);

            IWebElement buttonCreateAccount = Driver.FindElement(By.Id("SubmitCreate"));
            buttonCreateAccount.Click();

        }
    }
}
