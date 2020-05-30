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
        [Obsolete]
        public void Setup()
        {
            _driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            _driver.Manage().Window.Maximize();
            _driver.Url = "http://www.demoqa.com/resizable";
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            _builder = new Actions(_driver);
        }

        [Test]
        [Obsolete]
        public void Resizable_ExpectedElementToBeBigger()
        {
            IWebElement _sourceBox = _driver.FindElement(By.Id("resizableBoxWithRestriction"));

            int sourceBoxWidthBefore = _sourceBox.Size.Width;
            int sourceBoxHeightBefore = _sourceBox.Size.Height;

            IWebElement resizePoint = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='resizableBoxWithRestriction']//span")));
            resizePoint.Click();

            _builder
                .Click(resizePoint)
                .ClickAndHold(resizePoint)
                .MoveByOffset(50, 50)
                .Perform();

            int sourceBoxWidthAfter = _sourceBox.Size.Width;
            int sourceBoxHeightAfter = _sourceBox.Size.Height;

            Assert.AreNotEqual(sourceBoxWidthBefore, sourceBoxWidthAfter);
            Assert.AreNotEqual(sourceBoxHeightBefore, sourceBoxHeightAfter);
        }

        [Test]
        [Obsolete]
        public void Resizable_ExpectedElementWithMinimunSize()
        {
            IWebElement _sourceBox = _driver.FindElement(By.Id("resizableBoxWithRestriction"));

            int sourceBoxWidthBefore = _sourceBox.Size.Width;
            int sourceBoxHeightBefore = _sourceBox.Size.Height;

            IWebElement resizePoint = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='resizableBoxWithRestriction']//span")));
            _builder
                .Click(resizePoint)
                .ClickAndHold(resizePoint)
                .MoveByOffset(sourceBoxWidthBefore * (-1), sourceBoxHeightBefore * (-1))
                .Perform();

            int sourceBoxWidthAfter = _sourceBox.Size.Width;
            int sourceBoxHeightAfter = _sourceBox.Size.Height;
            
            Assert.AreEqual(150, sourceBoxWidthAfter);
            Assert.AreEqual(150, sourceBoxHeightAfter);
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}
