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
            _driver.Manage().Window.Maximize();
            _driver.Url = "http://www.demoqa.com/selectable";
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            _builder = new Actions(_driver);
        }

        [Test]
        [Obsolete]
        public void SelectItem_ExpactedToChangeColor()
        {

            IWebElement sourceBox = _driver.FindElement(By.XPath("//*[@id='verticalListContainer']//li[normalize-space(text())='Cras justo odio']"));
            var colorBefore = sourceBox.GetCssValue("color");

            _builder
                .Click(sourceBox)
                .Perform();

            var colorAfter = sourceBox.GetCssValue("color");

            Assert.AreNotEqual(colorAfter, colorBefore);
        }

        [Test]
        [Obsolete]
        public void SelectItems_ExpactedToSelectAllOfThem()
        {
            IWebElement tab = _driver.FindElement(By.XPath("//*[@id='demo-tab-grid']"));
            tab.Click();

            IWebElement element1 = _driver.FindElement(By.XPath("//*[@id='row1']//li[normalize-space(text()) = 'One']"));
            IWebElement element2 = _driver.FindElement(By.XPath("//*[@id='row1']//li[normalize-space(text()) = 'Two']"));
            IWebElement element3 = _driver.FindElement(By.XPath("//*[@id='row1']//li[normalize-space(text()) = 'Three']"));
            IWebElement element4 = _driver.FindElement(By.XPath("//*[@id='row2']//li[normalize-space(text()) = 'Four']"));
            IWebElement element5 = _driver.FindElement(By.XPath("//*[@id='row2']//li[normalize-space(text()) = 'Five']"));
            IWebElement element6 = _driver.FindElement(By.XPath("//*[@id='row2']//li[normalize-space(text()) = 'Six']"));
            IWebElement element7 = _driver.FindElement(By.XPath("//*[@id='row3']//li[normalize-space(text()) = 'Seven']"));
            IWebElement element8 = _driver.FindElement(By.XPath("//*[@id='row3']//li[normalize-space(text()) = 'Eight']"));
            IWebElement element9 = _driver.FindElement(By.XPath("//*[@id='row3']//li[normalize-space(text()) = 'Nine']"));
            
            var targetBoxListBefore = _driver.FindElements(By.XPath("//*[@id='gridContainer']//li"));
            int countElement = targetBoxListBefore.Count;

            _builder
                .Click(element1)
                .KeyDown(Keys.Control)
                .Click(element2)
                .Click(element3)
                .Click(element4)
                .Click(element5)
                .Click(element6)
                .Click(element7)
                .Click(element8)
                .Click(element9)
                .KeyUp(Keys.Control)
                .Release()
                .Perform();

            int countSelectedElememts = 0;
            var targetBoxListAfter = _driver.FindElements(By.XPath("//*[@id='gridContainer']//li"));

            foreach (IWebElement button in targetBoxListAfter)
            {
                var targetColor = button.GetCssValue("color");

                if (targetColor == "rgba(255, 255, 255, 1)")
                {
                    countSelectedElememts += 1;
                }
            }

            Assert.AreEqual(countElement, countSelectedElememts);
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}
