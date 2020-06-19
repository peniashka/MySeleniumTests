using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace SeleniunAdvancedTest 
{
    [TestFixture]
    class InteractionResizable : BaseVariables
    {
        [SetUp]
        [Obsolete]
        public void Setup()
        {
            Initialize();
            Driver.Url = "http://www.demoqa.com/resizable";
        }

        [Test]
        [Obsolete]
        public void Resizable_ExpectedElementToBeBigger()
        {
            IWebElement _sourceBox = Driver.FindElement(By.Id("resizableBoxWithRestriction"));

            int sourceBoxWidthBefore = _sourceBox.Size.Width;
            int sourceBoxHeightBefore = _sourceBox.Size.Height;

            IWebElement resizePoint = Driver.FindElement(By.XPath("//*[@id='resizableBoxWithRestriction']//span"));
            resizePoint.Click();

            Builder
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
            IWebElement _sourceBox = Driver.FindElement(By.Id("resizableBoxWithRestriction"));

            int sourceBoxWidthBefore = _sourceBox.Size.Width;
            int sourceBoxHeightBefore = _sourceBox.Size.Height;

            IWebElement resizePoint = Driver.FindElement(By.XPath("//*[@id='resizableBoxWithRestriction']//span"));
            
            Builder
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
            Driver.Quit();
        }
    }
}
