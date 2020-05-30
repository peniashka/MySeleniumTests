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
        private Actions _builder;

        [SetUp]
        [Obsolete]
        public void Setup()
        {
            _driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            _driver.Manage().Window.Maximize();
            _driver.Url = "http://www.demoqa.com/dragabble";
            _builder = new Actions(_driver);
        }

        [Test]
        public void DragElement_Ofset10_Expected_LocationToBeMoreWith10()
        {
            IWebElement sourceBox = _driver.FindElement(By.Id("dragBox"));

            int sourceBoxXPosBefore = sourceBox.Location.X;
            int sourceBoxYPosBefore = sourceBox.Location.Y;
            int x = 10;
            int y = 10;

            _builder
               .ClickAndHold(sourceBox)
               .MoveByOffset(x, y)
               .Release()
               .Build()
               .Perform();

            int sourceBoxXPosAfter = sourceBox.Location.X;
            int sourceBoxYPosAfter = sourceBox.Location.Y;
            int expectedX = sourceBoxXPosBefore + 10;
            int expectedY = sourceBoxYPosBefore + 10;

            Assert.AreEqual(expectedX, sourceBoxXPosAfter);
            Assert.AreEqual(expectedY, sourceBoxYPosAfter);
        }

        [Test]
        public void DragAndDrop_Element_ChangePosX()
        {
            IWebElement tabClick = _driver.FindElement(By.Id("draggableExample-tab-axisRestriction"));
            tabClick.Click();

            IWebElement sourceBox = _driver.FindElement(By.Id("restrictedX")); 

            int sourceBoxXPosBefore = sourceBox.Location.X;
            int sourceBoxYPosBefore = sourceBox.Location.Y;

            _builder
              .ClickAndHold(sourceBox)
              .MoveByOffset(10, 0)
              .Release()
              .Build()
              .Perform();

            int sourceBoxXPosAfter = sourceBox.Location.X;
            int sourceBoxYPosAfter = sourceBox.Location.Y;

            Assert.IsTrue(sourceBoxXPosBefore < sourceBoxXPosAfter);
            Assert.IsTrue(sourceBoxYPosAfter == sourceBoxYPosBefore);
        }

        [Test]
        public void DragAndDrop_Element_ChangePosY()
        {
            IWebElement tabClick = _driver.FindElement(By.Id("draggableExample-tab-axisRestriction"));
            tabClick.Click();

            IWebElement sourceBox = _driver.FindElement(By.Id("restrictedY"));

            int sourceBoxXPosBefore = sourceBox.Location.X;
            int sourceBoxYPosBefore = sourceBox.Location.Y;

            _builder
              .ClickAndHold(sourceBox)
              .MoveByOffset(0, 10)
              .Release()
              .Build()
              .Perform();

            int sourceBoxXPosAfter = sourceBox.Location.X;
            int sourceBoxYPosAfter = sourceBox.Location.Y;

            Assert.IsTrue(sourceBoxXPosBefore == sourceBoxXPosAfter);
            Assert.IsTrue(sourceBoxYPosBefore < sourceBoxYPosAfter);
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}
