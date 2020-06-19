using NUnit.Framework;
using OpenQA.Selenium;
using SeleniunAdvancedTest.Pages.PracticeForm;
using System;

namespace SeleniunAdvancedTest.Tests.GoogleSearch 
{
    [TestFixture]
    class SearchSoftUniPage : BaseVariables
    {
        private PracticeFormPage _practiceFormPage;

        [SetUp]
        public void Setup()
        {
            Initialize();
            Driver.Url = "https://softuni.bg/";
            _practiceFormPage = new PracticeFormPage(Driver);
        }

        [Test]
        public void OpenSoftUni_GoToQA_Automation()
        {
            IWebElement courses = Driver.FindElement(By.XPath(@"//*[@id='header-nav']/div[1]/ul/li[2]/a/span"));
            courses.Click();

            IWebElement qaCourseLink = Driver.FindElement(By.LinkText("QA Automation - май 2020"));
            qaCourseLink.Click();

            IWebElement header = Driver.FindElement(By.XPath(@"/html/body/div[2]/header/h1"));
           
            var IsExists = isElementPresent(By.TagName("h1"));
           
            _practiceFormPage.AssertIsTrue(IsExists);
            _practiceFormPage.AssertCheckErrorText(header, "QA Automation - май 2020");
        }

        public Boolean isElementPresent(By by)
        {
            try
            {
                Wait.Until<IWebElement>(d => d.FindElement(by));
                return true;
            }
            catch (NoSuchElementException e)
            {
                return false;
            }
        }

        [TearDown]
        public void TearDown()
        {
            Driver.Quit();
        }
    }
}
