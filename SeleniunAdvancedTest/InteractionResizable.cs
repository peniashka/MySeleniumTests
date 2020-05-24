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
            _driver.Url = "http://www.demoqa.com/";
            _driver.Manage().Window.Maximize();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            _builder = new Actions(_driver);

            IWebElement menu = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div/div/div/div[2]/div/div[1]")));
            menu.Click();
        }


        [Test]
        [Obsolete]
        public void Resizable_ExpectedElementToBeBigger()
        {

            IWebElement resizableLink = _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("item-2")));
            resizableLink.Click();

            IWebElement sourceBox = _driver.FindElement(By.Id("resizableBoxWithRestriction"));
           
            int sourceBoxWidthBefore = sourceBox.Size.Width;
            int sourceBoxHeightBefore = sourceBox.Size.Height;

            IWebElement resizePoint = _driver.FindElement(By.XPath("/html/body/div/div/div/div[2]/div[2]/div[1]/div[1]/div/span"));
          
            _builder
                .Click(resizePoint)
                .ClickAndHold(resizePoint)
                .MoveByOffset(50, 50)
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

            IWebElement resizableLink = _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("item-2")));
            resizableLink.Click();

            IWebElement sourceBox = _driver.FindElement(By.Id("resizableBoxWithRestriction"));

            int sourceBoxWidthBefore = sourceBox.Size.Width;
            int sourceBoxHeightBefore = sourceBox.Size.Height;

            IWebElement resizePoint = _driver.FindElement(By.XPath("/html/body/div/div/div/div[2]/div[2]/div[1]/div[1]/div/span"));

            _builder
                .Click(resizePoint)
                .ClickAndHold(resizePoint)
                .MoveByOffset(sourceBoxWidthBefore * (-1), sourceBoxHeightBefore * (-1))
                .Perform();

            int sourceBoxWidthAfter = sourceBox.Size.Width;
            int sourceBoxHeightAfter = sourceBox.Size.Height;
            
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
