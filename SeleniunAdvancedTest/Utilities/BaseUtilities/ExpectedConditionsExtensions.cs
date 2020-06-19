using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace SeleniunAdvancedTest.Utilities.BaseUtilities
{
    public static class ExpectedConditionsExtensions
    {
        public static void WaitForElementToChange(this IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));

        }
    }
}
