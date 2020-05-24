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
    class InteractionDragable
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
           // _driver.Manage().Window.Maximize();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            _builder = new Actions(_driver);

            IWebElement page = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/ html")));
            page.SendKeys(Keys.PageDown);

            IWebElement menu = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div/div/div/div[2]/div/div[1]")));
            menu.Click();
            
        }

        [Test]
        [Obsolete]
        public void DragElement_Ofset10_Expected_LocationToBeMoreWith10()
        {
            IWebElement page = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/ html")));
            page.SendKeys(Keys.PageDown);

            IWebElement dragableLink = _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("item-4")));
            dragableLink.Click();

            IWebElement sourceBox = _driver.FindElement(By.Id("dragBox"));

            int sourceBoxXPosBefore = sourceBox.Location.X;
            int sourceBoxYPosBefore = sourceBox.Location.Y;
            int x = 10;
            int y = 10;

            _builder
               .ClickAndHold(sourceBox)
               .MoveByOffset(x, y)
               .Perform();

            int sourceBoxXPosAfter = sourceBox.Location.X;
            int sourceBoxYPosAfter = sourceBox.Location.Y;
            int expectedX = sourceBoxXPosBefore + 10;
            int expectedY = sourceBoxYPosBefore + 10;

            Assert.AreEqual(expectedX, sourceBoxXPosAfter);
            Assert.AreEqual(expectedY, sourceBoxYPosAfter);
        }

        [Test]
        [Obsolete]
        public void DragAndDrop_Element_MoveOutOfArea()
        {

            IWebElement page = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/ html")));
            page.SendKeys(Keys.PageDown);

            IWebElement dragableLink = _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("item-4")));
            dragableLink.Click();

            IWebElement tabMenu = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/ html / body / div / div / div / div[2] / div[2] / div[1] / nav")));
            tabMenu.Click();

            IWebElement tabLink = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div/div/div/div[2]/div[2]/div[1]/nav/a[2]")));
            tabLink.Click();

            IWebElement sourceBox = _driver.FindElement(By.Id("restrictedX"));

            int sourceBoxXPosBefore = sourceBox.Location.X;
            int sourceBoxYPosBefore = sourceBox.Location.Y;

            _builder
              .ClickAndHold(sourceBox)
              .MoveByOffset(sourceBoxXPosBefore + 10, sourceBoxYPosBefore)
              .Perform();

            int sourceBoxXPosAfter = sourceBox.Location.X;
            int sourceBoxYPosAfter = sourceBox.Location.Y;

            Assert.IsTrue(sourceBoxXPosBefore < sourceBoxXPosAfter);
            Assert.IsTrue(sourceBoxYPosAfter == sourceBoxYPosBefore);
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}
