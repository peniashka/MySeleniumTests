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
    class InteractionResizable
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
        public void Resizable_ExpectedElementToBeBigger()
        {

            IWebElement resizableLink = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[1]/div[2]/div/div[1]/aside[1]/ul/li[3]/a")));
            resizableLink.Click();

            IWebElement sourceBox = _driver.FindElement(By.Id("resizable"));
           
            int sourceBoxWidthBefore = sourceBox.Size.Width;
            int sourceBoxHeightBefore = sourceBox.Size.Height;

            IWebElement resizePoint = _driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/div[2]/div[2]/div/div[3]"));
          
            _builder
                .Click(resizePoint)
                .ClickAndHold(resizePoint)
                .MoveByOffset(300, 250)
                .Perform();

            int sourceBoxWidthAfter = sourceBox.Size.Width;
            int sourceBoxHeightAfter = sourceBox.Size.Height;

            Assert.AreNotEqual(sourceBoxWidthBefore, sourceBoxWidthAfter);
            Assert.AreNotEqual(sourceBoxHeightBefore, sourceBoxHeightAfter);
        }

        [Test]
        [Obsolete]
        public void Resizable_ExpectedElementWithMinimunSize()
        {

            IWebElement resizableLink = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[1]/div[2]/div/div[1]/aside[1]/ul/li[3]/a")));
            resizableLink.Click();

            IWebElement sourceBox = _driver.FindElement(By.Id("resizable"));
           
            int sourceBoxWidthBefore = sourceBox.Size.Width;
            int sourceBoxHeightBefore = sourceBox.Size.Height;

            IWebElement resizePoint = _driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/div[2]/div[2]/div/div[3]"));

            _builder
                .Click(resizePoint)
                .ClickAndHold(resizePoint)
                .MoveByOffset(sourceBoxWidthBefore * (-1), sourceBoxHeightBefore * (-1))
                .Perform();

            int sourceBoxWidthAfter = sourceBox.Size.Width;
            int sourceBoxHeightAfter = sourceBox.Size.Height;
            
            Assert.AreEqual(18, sourceBoxWidthAfter);
            Assert.AreEqual(18, sourceBoxHeightAfter);
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}
