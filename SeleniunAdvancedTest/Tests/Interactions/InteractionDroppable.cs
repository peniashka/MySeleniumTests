using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace SeleniunAdvancedTest
{
    [TestFixture]
    class InteractionDroppable : BaseVariables
    {
        [SetUp]
        [Obsolete]
        public void Setup()
        {
            Initialize();
            Driver.Url = "http://www.demoqa.com/droppable";
        }

        [Test]
        [Obsolete]
        public void DragAndDrop_Element_CenterTarget_targetChangeColor()
        {

            IWebElement dragBox = Driver.FindElement(By.Id("draggable"));
            IWebElement dropBox = Driver.FindElement(By.Id("droppable"));

            var colorBefore = dropBox.GetCssValue("background-color");

            Builder
                .ClickAndHold(dragBox)
                .MoveToElement(dropBox)
                .Perform();

            var colorAfter = dropBox.GetAttribute("background-color");

            Assert.AreNotEqual(colorAfter, colorBefore);
        }

        [Test]
        [Obsolete]
        public void DragAndDrop_Element_targetMoveOutOfArea()
        {
            IWebElement dragBox = Driver.FindElement(By.Id("draggable"));

            var PosXBefore = dragBox.Location.X;
            var PosYBefore = dragBox.Location.Y;

            IWebElement workArea = Driver.FindElement(By.XPath("//*[@class='col-12 mt-4 col-md-6']"));

            var workAreaPosXBefore = workArea.Location.X;
            var workAreaPosYBefore = workArea.Location.Y;

            int x = PosXBefore * (-1);
            int y = PosYBefore * (-1);

            Builder
                .ClickAndHold(dragBox)
                .MoveByOffset(x, y)
                .Perform();

            var PosXAfter = dragBox.Location.X;
            var PosYAfter = dragBox.Location.Y;

            Assert.IsTrue(PosXAfter < workAreaPosXBefore);
            Assert.IsTrue(PosYAfter < workAreaPosYBefore);
        }

        [Test]
        public void DragAndDrop_Element_CenterTarget_NotReverted()
        {
            IWebElement tabMenuAccepted = Driver.FindElement(By.XPath("//*[@id='droppableExample-tab-revertable']"));
            tabMenuAccepted.Click();

            IWebElement sourceBox = Driver.FindElement(By.Id("notRevertable"));
            IWebElement targetBox = Driver.FindElement(By.XPath("//*[@class='revertable-drop-container']//*[@id='droppable']"));

            var colorBefore = targetBox.GetCssValue("color");

            Builder
                .ClickAndHold(sourceBox)
                .MoveToElement(targetBox)
                .Perform();

            var colorAfter = targetBox.GetAttribute("background-color");

            Assert.AreNotEqual(colorAfter, colorBefore);
        }

      
        [TearDown]
        public void TearDown()
        {
            Driver.Quit();
        }
    }
}
