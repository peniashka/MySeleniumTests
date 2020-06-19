using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Reflection;

namespace SeleniunAdvancedTest.Pages.PracticeForm
{
    public partial class PracticeFormPage : BasePage
    {
        public void AssertCheckErrorText(IWebElement element, string result)
        {
            this.WaitForLoad();
            var textStatus = element.Text;
            Assert.IsTrue(textStatus.Contains(result));
        }

        public void AssertIsTrue(bool result)
        {
            Assert.IsTrue(result);
        }

        public void AssertCheckAtributeValue(IWebElement element, string result)
        {
            this.WaitForLoad();
            var textStatus = element.GetAttribute("value");
            Assert.AreEqual(result, textStatus);
        }

        public void AssertCheckPageTitle(IWebElement element)
        {
            element.Click();
            var textTitle = Driver.Title;

            var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            driver.Navigate().GoToUrl("http://www.seleniumhq.org");

            var checkURL = driver.Url;
            var checkTitle = driver.Title;

            Assert.AreEqual(checkTitle, textTitle);

            var pageURL = Driver.Url;

            Assert.AreEqual(checkURL, pageURL);

            driver.Quit();
        }
    }
}
