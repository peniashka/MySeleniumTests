using OpenQA.Selenium;

namespace SeleniunAdvancedTest.Pages.PracticeForm
{
    public partial class PracticeFormPage : BasePage
    {
        public IWebElement Email => Driver.FindElement(By.Id("email"));

        public IWebElement FirstName => Driver.FindElement(By.Id("customer_firstname"));

        public IWebElement LastName => Driver.FindElement(By.Id("customer_lastname"));

        public IWebElement Phone => Driver.FindElement(By.Id("phone_mobile"));

        public IWebElement Password => Driver.FindElement(By.Id("passwd"));

        public IWebElement Address => Driver.FindElement(By.Name("address1"));

        public IWebElement City => Driver.FindElement(By.Name("city"));

        public IWebElement PostCode => Driver.FindElement(By.Name("postcode"));

        public IWebElement Submit => Driver.FindElement(By.Id("submitAccount"));

        public IWebElement ButtonSignIn => Driver.FindElement(By.XPath("//*[@class = 'login']"));

        public IWebElement FieldEmailAddress => Driver.FindElement(By.XPath("//*[@id = 'email_create']"));

        public IWebElement ButtonCreateAccount => Driver.FindElement(By.Id("SubmitCreate"));

        public IWebElement WorningTextFiled => Driver.FindElement(By.XPath("//*[@class = 'alert alert-danger']"));
    }
}
