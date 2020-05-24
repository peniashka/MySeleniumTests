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
        public void Setup()
        {
            _driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            _driver.Url = "https://demoqa.com/";
            _driver.Manage().Window.Maximize();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            _builder = new Actions(_driver);
        }

        [Test]
        [Obsolete]
        public void SelectItems_ExpactedToSelectAllOfThem()
        {

            IWebElement selectableLink = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[1]/div[2]/div/div[1]/aside[1]/ul/li[2]/a")));
            selectableLink.Click();

            List<string> listItemBefore = new List<string>();

            List<IWebElement> elementsListBefore = _driver.FindElements(By.XPath("/ html / body / div[1] / div[2] / div / div[2] / div[2] / ol/li")).ToList();
            foreach (IWebElement element in elementsListBefore)
            {
                String value = element.Text;
                listItemBefore.Add(value);

            }

            IWebElement sourceBox = _driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/div[2]/div[2]/ol/li[1]"));
            IWebElement targetBox = _driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/div[2]/div[2]/ol/li[7]"));
   
            _builder
                .Click(sourceBox)
                .ClickAndHold(sourceBox)
                .MoveToElement(targetBox)
                .Perform();

            string targetElementText = targetBox.Text;
            int countSelectedElememts = 0;

            if (targetElementText == "Item 7")
            {
               countSelectedElememts = 7;
            }
          
             Assert.AreEqual(listItemBefore.Count(), countSelectedElememts);
        }

        [Test]
        [Obsolete]
        public void SelectItem_ExpactedToChangeColor()
        {

            IWebElement selectableLink = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[1]/div[2]/div/div[1]/aside[1]/ul/li[2]/a")));
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
