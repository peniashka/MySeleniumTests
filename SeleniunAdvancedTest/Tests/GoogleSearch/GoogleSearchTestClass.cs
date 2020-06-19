using NUnit.Framework;
using OpenQA.Selenium;
using SeleniunAdvancedTest.Pages.PracticeForm;

namespace SeleniunAdvancedTest.Tests.GoogleSearch
{
    [TestFixture]
    class GoogleSearchTestClass : BaseVariables
    {
        private PracticeFormPage _practiceFormPage;

        [SetUp]
        public void Setup()
        {
            Initialize();
            Driver.Url = "http://www.google.com";
            _practiceFormPage = new PracticeFormPage(Driver);
        }

        [Test]
        public void SearchToolSelenium_ClickFirstResult_ExpectedToBe_SeleniumWebBrowserAutomation()
        {

            IWebElement searchField = Driver.FindElement(By.XPath("//*[@class = 'gLFyf gsfi']"));
            searchField.SendKeys("selenium");
            searchField.SendKeys(Keys.Enter);

            this.WaitForLoad();
            IWebElement searchFirstResult = Wait.Until<IWebElement>(d => d.FindElement(By.XPath("/html/body/div[6]/div[2]/div[9]/div[1]/div[2]/div/div[2]/div[2]/div/div/div[1]/div/div[1]/a/h3")));

            _practiceFormPage.AssertCheckPageTitle(searchFirstResult);
        }

        [TearDown]
        public void TearDown()
        {
            Driver.Quit();
        }
    }
}
