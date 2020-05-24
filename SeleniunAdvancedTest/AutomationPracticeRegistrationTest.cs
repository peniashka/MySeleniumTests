using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Reflection;

namespace SeleniunAdvancedTest
{
    [TestFixture]
    public class AutomationPracticeRegistrationTest
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;
        private string _email;

        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            _driver.Url = "http://automationpractice.com/index.php";
            _driver.Manage().Window.Maximize();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            Random randomGenerator = new Random();
            int randomInt = randomGenerator.Next(1000);
            _email = "username" + randomInt + "@xxx.com";

            IWebElement buttonSignIn = _wait.Until<IWebElement>(d => d.FindElement(By.XPath("/html/body/div/div[1]/header/div[2]/div/div/nav/div[1]/a")));
            buttonSignIn.Click();

            IWebElement fieldEmailAddress = _wait.Until<IWebElement>(d => d.FindElement(By.XPath("/html/body/div/div[2]/div/div[3]/div/div/div[1]/form/div/div[2]/input")));
            fieldEmailAddress.SendKeys(_email);

            IWebElement buttonCreateAccount = _wait.Until<IWebElement>(d => d.FindElement(By.XPath("/html/body/div/div[2]/div/div[3]/div/div/div[1]/form/div/div[3]/button/span")));
            buttonCreateAccount.Click();
        }

        [Obsolete]
        private void FillAllFields()
        {
            IWebElement firstName = _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("customer_firstname")));
            firstName.SendKeys("FirstName");

            IWebElement lastName = _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("customer_lastname")));
            lastName.SendKeys("LastName");

            IWebElement password = _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("passwd")));
            password.SendKeys("pass123");

            IWebElement address = _wait.Until(ExpectedConditions.ElementIsVisible(By.Name("address1")));
            address.SendKeys("str1, 3, 23");

            IWebElement city = _wait.Until(ExpectedConditions.ElementIsVisible(By.Name("city")));
            address.SendKeys("city1");

            IWebElement postcode = _wait.Until(ExpectedConditions.ElementIsVisible(By.Name("postcode")));
            address.SendKeys("12345");
        }

        [Test]
        [Obsolete]
        public void RegistrationFrom_CheckFieldFirstNamed_ExpactedErrorWhenEmpty()
        {
            FillAllFields();

            IWebElement firstName = _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("customer_firstname")));
            firstName.Clear();
             // firstName.SendKeys(String.Empty);

             IWebElement buttonRegister = _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("submitAccount")));
            buttonRegister.Click();

            IWebElement checkStatusText = _wait.Until<IWebElement>(d => d.FindElement(By.XPath("/ html / body / div / div[2] / div / div[3] / div / div / ol")));
            var textStatus = checkStatusText.Text;

            Assert.IsTrue(textStatus.Contains("firstname is required."));
        }

        [Test]
        [Obsolete]
        public void RegistrationFrom_CheckFieldLastNamed_ExpactedErrorWhenEmpty()
        {
            FillAllFields();

            IWebElement lastName = _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("customer_lastname")));
            lastName.Clear();

            IWebElement buttonRegister = _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("submitAccount")));
            buttonRegister.Click();

            IWebElement checkStatusText = _wait.Until<IWebElement>(d => d.FindElement(By.XPath("/ html / body / div / div[2] / div / div[3] / div / div / ol")));
            var textStatus = checkStatusText.Text;

            Assert.IsTrue(textStatus.Contains("lastname is required."));
        }

        [Test]
        [Obsolete]
        public void RegistrationFrom_CheckFieldEmail_IsCorrectEmail()
        {
            FillAllFields();

            IWebElement fieldEmailAddress = _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("email")));
            fieldEmailAddress.Clear();
            fieldEmailAddress.SendKeys("username111@com");

            IWebElement buttonRegister = _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("submitAccount")));
            buttonRegister.Click();

            IWebElement checkStatusText = _wait.Until<IWebElement>(d => d.FindElement(By.XPath("/ html / body / div / div[2] / div / div[3] / div / div / ol")));
            var textStatus = checkStatusText.Text;

            Assert.IsTrue(textStatus.Contains("email is invalid."));
        }

        [Test]
        [Obsolete]
        public void RegistrationFrom_CheckAddress_ExpactedErrorWhenEmpty()
        {
            FillAllFields();

            IWebElement address = _wait.Until(ExpectedConditions.ElementIsVisible(By.Name("address1")));
            address.Clear();

            IWebElement buttonRegister = _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("submitAccount")));
            buttonRegister.Click();

            IWebElement checkStatusText = _wait.Until<IWebElement>(d => d.FindElement(By.XPath("/ html / body / div / div[2] / div / div[3] / div / div / ol")));
            var textStatus = checkStatusText.Text;

            Assert.IsTrue(textStatus.Contains("address1 is required."));
        }

        [Test]
        [Obsolete]
        public void RegistrationFrom_CheckCity_ExpactedErrorWhenEmpty()
        {
            FillAllFields(); 
            
            IWebElement city = _wait.Until(ExpectedConditions.ElementIsVisible(By.Name("city")));
            city.SendKeys(String.Empty);

            IWebElement buttonRegister = _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("submitAccount")));
            buttonRegister.Click();

            IWebElement checkStatusText = _wait.Until<IWebElement>(d => d.FindElement(By.XPath("/ html / body / div / div[2] / div / div[3] / div / div / ol")));
            var textStatus = checkStatusText.Text;

            Assert.IsTrue(textStatus.Contains("city is required."));
        }


        [Test]
        [Obsolete]
        public void RegistrationFrom_CheckPassword_IsEmpty()
        {
            FillAllFields();

            IWebElement password = _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("passwd")));
            password.Clear();

            IWebElement buttonRegister = _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("submitAccount")));
            buttonRegister.Click();

            IWebElement checkStatusText = _wait.Until<IWebElement>(d => d.FindElement(By.XPath("/ html / body / div / div[2] / div / div[3] / div / div / ol")));
            var textStatus = checkStatusText.Text;

            Assert.IsTrue(textStatus.Contains("passwd is required."));
        }

        [Test]
        [Obsolete]
        public void RegistrationFrom_CheckPost_RequireFiveDigit()
        {
            FillAllFields();

            IWebElement postcode = _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("postcode")));
            postcode.SendKeys("1234a");

            var addressText = postcode.GetAttribute("value");
            int n;
            bool isNumeric = int.TryParse(addressText, out n);

            IWebElement buttonRegister = _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("submitAccount")));
            buttonRegister.Click();

            IWebElement checkStatusText = _wait.Until<IWebElement>(d => d.FindElement(By.XPath("/ html / body / div / div[2] / div / div[3] / div / div / ol")));
            var textStatus = checkStatusText.Text;

            if (addressText.Length != 5 || !isNumeric)
            {
                Assert.IsTrue(textStatus.Contains("The Zip/Postal code you've entered is invalid. It must follow this format: 00000"));
            }
        }

        [Test]
        [Obsolete]
        public void RegistrationFrom_CheckPassword_IsValid()
        {
            FillAllFields();

            IWebElement password = _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("passwd")));
            password.Clear();
            password.SendKeys("vvvv");

            IWebElement buttonRegister = _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("submitAccount")));
            buttonRegister.Click();

            IWebElement checkStatusText = _wait.Until<IWebElement>(d => d.FindElement(By.XPath("/ html / body / div / div[2] / div / div[3] / div / div / ol")));
            var textStatus = checkStatusText.Text;

            Assert.IsTrue(textStatus.Contains("passwd is invalid."));
        }

        [Test]
        [Obsolete]
        public void RegistrationFrom_CheckPhone_MustHaveAtLeastOnePhoneNumber()
        {
            FillAllFields();

            IWebElement homePhone = _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("phone")));
            homePhone.SendKeys(String.Empty);

            var homePhoneText = homePhone.GetAttribute("value");

            IWebElement mobilePhone = _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("phone_mobile")));
            mobilePhone.SendKeys(String.Empty);

            var mobilePhoneText = mobilePhone.GetAttribute("value");

            IWebElement buttonRegister = _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("submitAccount")));
            buttonRegister.Click();

            IWebElement checkStatusText = _wait.Until<IWebElement>(d => d.FindElement(By.XPath("/ html / body / div / div[2] / div / div[3] / div / div / ol")));
            var textStatus = checkStatusText.Text;


            bool homePhoneBool = String.IsNullOrEmpty(homePhoneText);
            bool mobilePhoneBool = String.IsNullOrEmpty(mobilePhoneText);

            if (!homePhoneBool | mobilePhoneBool)
            {
                Assert.IsTrue(textStatus.Contains("You must register at least one phone number."));
            }
            else
            {
                Assert.IsFalse(textStatus.Contains("You must register at least one phone number."));
            }

        }
        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}