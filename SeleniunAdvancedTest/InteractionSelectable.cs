using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SeleniunAdvancedTest
{
    [TestFixture]
    class InteractionSortable
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;
        private Actions _builder;

        [SetUp]
        [Obsolete]
        public void Setup()
        {
            _driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            _driver.Url = "http://www.demoqa.com/";
            _driver.Manage().Window.Maximize();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            _builder = new Actions(_driver);

            IWebElement menu = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div/div/div/div[2]/div/div[1]")));
            menu.Click();
        }


        [Test]
        [Obsolete]
        public void SelectItems_ExpactedToSelectAllOfThem()
        {

            IWebElement selectableLink = _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("item-1")));
            selectableLink.Click();

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

            IWebElement sourceBox = _driver.FindElement(By.XPath("/html/body/div/div/div/div[2]/div[2]/div[1]/div/div[2]/div/div[1]/li[1]"));
            IWebElement targetBox = _driver.FindElement(By.XPath("/html/body/div/div/div/div[2]/div[2]/div[1]/div/div[2]/div/div[3]/li[3]"));
   
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

            IWebElement selectableLink = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div/div/div/div[2]/div[1]/div/div/div[1]/div/ul/li[2]/span")));
            selectableLink.Click();

            IWebElement sourceBox = _driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/div[2]/div[2]/ol/li[3]"));
           
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
