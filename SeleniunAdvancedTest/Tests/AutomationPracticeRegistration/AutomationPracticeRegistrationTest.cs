using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using SeleniunAdvancedTest.Models;
using SeleniunAdvancedTest.Pages.PracticeForm;
using SeleniunAdvancedTest.Factories;


namespace SeleniunAdvancedTest
{
    [TestFixture]
    public class AutomationPracticeRegistrationTest : BaseVariables
    {

        private PracticeFormPage _practiceFormPage;
        private PracticeFormModel _user;
        private string _email;

        [SetUp]
        public void Setup()
        {
            Initialize();
            Driver.Navigate().GoToUrl("http://automationpractice.com/index.php");
           
            _practiceFormPage = new PracticeFormPage(Driver);

            _practiceFormPage.ButtonSignIn.Click();

            Random randomGenerator = new Random();
            int randomInt = randomGenerator.Next(1000);
            _email = "username" + randomInt + "@xxx.com";

            this.WaitForLoad();
            _practiceFormPage.FieldEmailAddress.SendKeys(_email);

            _practiceFormPage.ButtonCreateAccount.Click();

            _user = PracticeFormFactory.Create();
            _user.Email = _email;

       }

        [Test]
        [Obsolete]
        public void RegistrationFrom_CheckFieldFirstNamed_ExpactedErrorWhenEmpty()
        {
            _user.FirstName = string.Empty;

            _practiceFormPage.FillForm(_user);

            _practiceFormPage.AssertCheckErrorText(_practiceFormPage.WorningTextFiled, "firstname is required.");
        }

        [Test]
        [Obsolete]
        public void RegistrationFrom_CheckFieldLastNamed_ExpactedErrorWhenEmpty()
        {

            _user.LastName = string.Empty;

            _practiceFormPage.FillForm(_user);

            _practiceFormPage.AssertCheckErrorText(_practiceFormPage.WorningTextFiled, "lastname is required.");
        }

        [Test]
        [Obsolete]
        public void RegistrationFrom_CheckFieldEmail_IsCorrectEmail()
        {
            this.WaitForLoad();

            _user.Email = "";

            _practiceFormPage.FillForm(_user);

            _user.Email = "test@.com";

            _practiceFormPage.FillForm(_user);

            _practiceFormPage.AssertCheckErrorText(_practiceFormPage.WorningTextFiled, "email is invalid.");
        }

        [Test]
        [Obsolete]
        public void RegistrationFrom_CheckAddress_ExpactedErrorWhenEmpty()
        {
            _user.Address = "";

            _practiceFormPage.FillForm(_user);

            _practiceFormPage.AssertCheckErrorText(_practiceFormPage.WorningTextFiled, "address1 is required.");
        }

        [Test]
        [Obsolete]
        public void RegistrationFrom_CheckCity_ExpactedErrorWhenEmpty()
        {
            _user.City = "";

            _practiceFormPage.FillForm(_user);

            _practiceFormPage.AssertCheckErrorText(_practiceFormPage.WorningTextFiled, "city is required.");
         }


        [Test]
        [Obsolete]
        public void RegistrationFrom_CheckPassword_IsEmpty()
        {
            _user.Password = "";

            _practiceFormPage.FillForm(_user);

            _practiceFormPage.AssertCheckErrorText(_practiceFormPage.WorningTextFiled, "passwd is required.");
        }

        [Test]
        [Obsolete]
        public void RegistrationFrom_CheckPost_RequireFiveDigit()
        {
            _user.PostCode = "1234a";

            _practiceFormPage.FillForm(_user);

            int n;
            bool isNumeric = int.TryParse(_user.Password, out n);

            if (_user.Password.Length != 5 || !isNumeric)
            {
                _practiceFormPage.AssertCheckErrorText(_practiceFormPage.WorningTextFiled, "The Zip/Postal code you've entered is invalid. It must follow this format: 00000");
            }
        }

        [Test]
        [Obsolete]
        public void RegistrationFrom_CheckPassword_IsValid()
        {
            _user.Password = "vvvv";

            _practiceFormPage.FillForm(_user);

            _practiceFormPage.AssertCheckErrorText(_practiceFormPage.WorningTextFiled, "passwd is invalid.");
        }

        [Test]
        [Obsolete]
        public void RegistrationFrom_CheckPhone_MustHaveAtLeastOnePhoneNumber()
        {
            _user.Phone = "";

            _practiceFormPage.FillForm(_user);

            _practiceFormPage.AssertCheckErrorText(_practiceFormPage.WorningTextFiled, "You must register at least one phone number.");
        }

        [TearDown]
        public void TearDown()
        {
            Driver.Quit();
        }
    }
}