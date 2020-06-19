using SeleniunAdvancedTest.Models;
using OpenQA.Selenium;

namespace SeleniunAdvancedTest.Pages.PracticeForm
{
    public partial class PracticeFormPage : BasePage
    {
        public PracticeFormPage(IWebDriver driver)
            : base(driver)
        {
        }

        public void FillForm(PracticeFormModel user)
        {
            Email.SendKeys(user.Email);
            FirstName.SendKeys(user.FirstName);
            LastName.SendKeys(user.LastName);
            Phone.SendKeys(user.Phone);
            Password.SendKeys(user.Password);
            Address.SendKeys(user.Address);
            City.SendKeys(user.City);
            PostCode.SendKeys(user.PostCode);

            ScrollTo(Submit).Click();
        }
    }
}
