using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Reflection;

namespace SeleniunAdvancedTest
{
    [TestFixture]
    class InteractionSelectable
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;
        private Actions _builder;

        [SetUp]
        [Obsolete]
        public void Setup()
        {
            _driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            _driver.Url = "http://www.demoqa.com/selectable";
            _driver.Manage().Window.Maximize();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(100));
            _builder = new Actions(_driver);
        }

        [Obsolete]
        private void ScroolPage()
        {
            IWebElement page = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html")));
            page.SendKeys(Keys.PageDown);
        }

        [Test]
        [Obsolete]
        public void SelectItems_ExpactedToSelectAllOfThem()
        {
            IWebElement tab = _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/ html / body / div / div / div / div[2] / div[2] / div[1] / nav")));
            tab.Click();

            IWebElement grid = _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("demo-tab-grid")));
            grid.Click();

            IWebElement elementsListBefore = _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("gridContainer")));
            string valueT = elementsListBefore.Text;
            string resultT = valueT.Substring(valueT.Length - 4, 4);
            int listItemBefore = 0;

            if (resultT == "Nine")
            {
                listItemBefore = 9;
            }

            IWebElement sourceBox = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div/div/div/div[2]/div[2]/div[1]/div/div[2]/div/div[1]/li[1]")));
            IWebElement targetBox = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div/div/div/div[2]/div[2]/div[1]/div/div[2]/div/div[3]/li[3]")));
   
            _builder
                .Click(sourceBox)
                .ClickAndHold(sourceBox)
                .MoveToElement(targetBox)
                .Perform();

            string targetElementText = targetBox.Text;
            int countSelectedElememts = 0;

            if (targetElementText == "Nine")
            {
               countSelectedElememts = 9;
            }
          
             Assert.AreEqual(listItemBefore, countSelectedElememts);
        }

        [Test]
        [Obsolete]
        public void SelectItem_ExpactedToChangeColor()
        {

            IWebElement sourceBox = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div/div/div/div[2]/div[2]/div[1]/div/div[1]/ul/li[1]")));
           
            var colorBefore = sourceBox.GetCssValue("color");

            _builder
                .Click(sourceBox)
                .Perform();

            var colorAfter = sourceBox.GetCssValue("color");

            Assert.AreNotEqual(colorAfter, colorBefore);
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}
