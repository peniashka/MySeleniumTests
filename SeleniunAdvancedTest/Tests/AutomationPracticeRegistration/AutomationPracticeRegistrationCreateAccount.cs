using NUnit.Framework;
using OpenQA.Selenium;
using System;
using SeleniunAdvancedTest.Pages.PracticeForm;


namespace SeleniunAdvancedTest.Tests.AutomationPracticeRegistration
{
    [TestFixture]
    class AutomationPracticeRegistrationCreateAccount : BaseVariables
    {
        private string _email;
        private PracticeFormPage _practiceFormPage;

        [SetUp]
        public void Setup()
        {
            Initialize();
            Driver.Navigate().GoToUrl("http://automationpractice.com/index.php");
            _practiceFormPage = new PracticeFormPage(Driver);
        }

        [Test]
        public void SignIn_ByEmail_ExpactedSameMail()
        {
            _practiceFormPage.ButtonSignIn.Click();

            Random randomGenerator = new Random();
            int randomInt = randomGenerator.Next(1000);
            _email = "username" + randomInt + "@xxx.com";

            this.WaitForLoad();
            _practiceFormPage.FieldEmailAddress.SendKeys(_email);

            _practiceFormPage.ButtonCreateAccount.Click();

            this.WaitForLoad();
            IWebElement registrationMail = Driver.FindElement(By.Id("email"));
            ScrollTo(registrationMail);

             _practiceFormPage.AssertCheckAtributeValue(registrationMail, _email);
        }

        [TearDown]
        public void TearDown()
        {
            Driver.Quit();
        }
    }
}
