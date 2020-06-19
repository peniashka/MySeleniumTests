using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace SeleniunAdvancedTest 
{
    [TestFixture]
    class InteractionDragable : BaseVariables
    {
       
        [SetUp]
        [Obsolete]
        public void Setup()
        {
            Initialize();
            Driver.Url = "http://www.demoqa.com/dragabble";
        }

        [Test]
        public void DragElement_Ofset10_Expected_LocationToBeMoreWith10()
        {
            IWebElement sourceBox = Driver.FindElement(By.Id("dragBox"));

            int sourceBoxXPosBefore = sourceBox.Location.X;
            int sourceBoxYPosBefore = sourceBox.Location.Y;
            int x = 10;
            int y = 10;

            Builder
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
            IWebElement tabClick = Driver.FindElement(By.Id("draggableExample-tab-axisRestriction"));
            tabClick.Click();

            IWebElement sourceBox = Driver.FindElement(By.Id("restrictedX")); 

            int sourceBoxXPosBefore = sourceBox.Location.X;
            int sourceBoxYPosBefore = sourceBox.Location.Y;

            Builder
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
            IWebElement tabClick = Driver.FindElement(By.Id("draggableExample-tab-axisRestriction"));
            tabClick.Click();

            IWebElement sourceBox = Driver.FindElement(By.Id("restrictedY"));

            int sourceBoxXPosBefore = sourceBox.Location.X;
            int sourceBoxYPosBefore = sourceBox.Location.Y;

            Builder
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
            Driver.Quit();
        }
    }
}
