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
    class InteractionDroppable
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
        public void DragAndDrop_Element_CenterTarget_targetChangeColor()
        {

            IWebElement droppableLink = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[1]/div[2]/div/div[1]/aside[1]/ul/li[4]/a")));
            droppableLink.Click();

            IWebElement sourceBox = _driver.FindElement(By.Id("draggable"));
            IWebElement targetBox = _driver.FindElement(By.Id("droppable"));

            var colorBefore = targetBox.GetCssValue("color");

            _builder
                .ClickAndHold(sourceBox)
                .MoveToElement(targetBox)
                .Perform();

            var colorAfter = targetBox.GetAttribute("background-color");

            Assert.AreNotEqual(colorAfter, colorBefore);

        }

        [Test]
        [Obsolete]
        public void DragAndDrop_Element_MoveOutOfArea()
        {

            IWebElement droppableLink = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[1]/div[2]/div/div[1]/aside[1]/ul/li[4]/a")));
            droppableLink.Click();
            
            IWebElement workArea = _driver.FindElement(By.Id("sidebar"));
            
            int areaXPos = workArea.Location.X;
            int areaYPos = workArea.Location.Y;

            IWebElement sourceBox = _driver.FindElement(By.Id("draggable"));

            int x = (100 + areaXPos) * (-1);
            int y = (100 + areaYPos) * (-1);

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
