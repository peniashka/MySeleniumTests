using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SeleniunAdvancedTest
{
    [TestFixture]
    class InteractionSortable : BaseVariables
    {

        [SetUp]
        [Obsolete]
         public void Setup()
        {
            Initialize();
            Driver.Url = "http://www.demoqa.com/sortable";
        }

        [Test]
        [Obsolete]
        public void Sortable_MoveFirstElement_OnSecondPosition_ExpectedYpositionToBeGreaterWith50()
        {
            IWebElement sourceBox = Driver.FindElement(By.XPath("//*[@id='sortableContainer']//div[normalize-space(text())='One']"));
            IWebElement targetBox = Driver.FindElement(By.XPath("//*[@id='sortableContainer']//div[normalize-space(text())='Two']"));

            int sourcePosYBefore = sourceBox.Location.Y;
           
            Builder
                .Click(sourceBox)
                .ClickAndHold(sourceBox)
                .MoveToElement(targetBox)
                .MoveByOffset(0, 10)
                .Release()
                .Perform();

            sourceBox = Driver.FindElement(By.XPath("//*[@id='sortableContainer']//div[normalize-space(text())='One']"));

            int sourcePosYAfter = sourceBox.Location.Y;
            int expectedPosY = sourcePosYBefore + 50;

            Assert.AreEqual(expectedPosY, sourcePosYAfter);
        }

        [Test]
        [Obsolete]
        public void Sortable_MoveFirstElement_ExpactedXpositionToBeTheSame_AfterMovement()
        {
            IWebElement sourceBox = Driver.FindElement(By.XPath("//*[@id='sortableContainer']//div[normalize-space(text())='One']"));
            IWebElement targetBox = Driver.FindElement(By.XPath("//*[@id='sortableContainer']//div[normalize-space(text())='Six']"));

            int sourcePosXBefore = sourceBox.Location.X;

            Builder
                .Click(sourceBox)
                .ClickAndHold(sourceBox)
                .MoveToElement(targetBox)
                .MoveByOffset(0, 10)
                .Release()
                .Perform();

            int sourcePosXAfter = sourceBox.Location.X;

            Assert.AreEqual(sourcePosXBefore, sourcePosXAfter);
        }

        [Test]
        public void AllBoxesAreOrdered_When_DragSingleBox()
        {
            var container = Driver.FindElement(By.CssSelector("#demo-tabpane-list > div"));
            var listOfOptions = Driver.FindElements(By.XPath("//div[@id='demo-tabpane-list']//div[contains(@class, 'list-group-item')]"));

            Builder.DragAndDropToOffset(listOfOptions[4], 100, 50).Perform();

            Assert.IsTrue(listOfOptions.All(o => o.Location.X == container.Location.X));
        }

        [Test]
        [Obsolete]
        public void Sortable_MoveFirstElement_OnSecondPosition()
        {
            IWebElement sourceBox = Driver.FindElement(By.XPath("//*[@id='sortableContainer']//div[normalize-space(text())='One']"));
            IWebElement targetBox = Driver.FindElement(By.XPath("//*[@id='sortableContainer']//div[normalize-space(text())='Two']"));

            Builder
                .Click(sourceBox)
                .ClickAndHold(sourceBox)
                .MoveToElement(targetBox)
                .MoveByOffset(0, 10)
                .Release()
                .Perform();

            var oneNewText = Driver.FindElement(By.CssSelector("#demo-tabpane-list > div > div:nth-child(2)")).Text;
            Assert.AreEqual("One", oneNewText);
        }

        [TearDown]
        public void TearDown()
        {
            Driver.Quit();
        }
    }
}
