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
            IWebElement menuCources = Driver.FindElement(By.XPath("/html/body/div[1]/div[1]/header/nav/div[1]/ul/li[2]/a"));
            menuCources.Click();

            IWebElement opencources = Driver.FindElement(By.XPath("/html/body/div[1]/div[1]/header/nav/div[1]/ul/li[2]/div/div/div[2]/div[2]/div/div[2]/div[2]/div[1]/i"));
            opencources.Click();

            IWebElement courses = Driver.FindElement(By.XPath(@"//*[@class='col-md-8 open-courses-wrapper open-courses-background']/div[1]/div[2]/div[2]/div[2]/ul/li[7]"));
            courses.Click();

            IWebElement header = Driver.FindElement(By.XPath("//*[@class= 'text-center']"));
           
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
