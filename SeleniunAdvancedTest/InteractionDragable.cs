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
        public void DragElement_Ofset10_Expected_LocationToBeMoreWith10()
        {

            IWebElement dragableLink = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[1]/div[2]/div/div[1]/aside[1]/ul/li[5]/a")));
            dragableLink.Click();

            IWebElement sourceBox = _driver.FindElement(By.Id("draggable"));

            int sourceBoxXPosBefore = sourceBox.Location.X;
            int sourceBoxYPosBefore = sourceBox.Location.Y;

            _builder
             .ClickAndHold(sourceBox)
             .MoveByOffset(10, 10)
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

            IWebElement dragableLink = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[1]/div[2]/div/div[1]/aside[1]/ul/li[5]/a")));
            dragableLink.Click();

            IWebElement workArea = _driver.FindElement(By.Id("sidebar"));
            
            int areaXPos = workArea.Location.X;
            int areaYPos = workArea.Location.Y;

            IWebElement sourceBox = _driver.FindElement(By.Id("draggable"));

            int x = (100 + areaXPos) * (-1);
            int y = (150 + areaYPos) * (-1);

            _builder
              .ClickAndHold(sourceBox)
              .MoveByOffset(x, y)
              .Perform();

            int sourceBoxXPosAfter = sourceBox.Location.X;
            int sourceBoxYPosAfter = sourceBox.Location.Y;

            Assert.IsTrue(sourceBoxXPosAfter < areaXPos);
            Assert.IsTrue(sourceBoxYPosAfter < areaYPos);
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}
