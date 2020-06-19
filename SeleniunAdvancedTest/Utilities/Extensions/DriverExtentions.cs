using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace SeleniunAdvancedTest
{
  public static  class DriverExtentions
    {
        public static void ScrollTo (this IWebDriver driver, IWebElement element)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

    }
}
