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
        [Obsolete]
        public void Setup()
        {
            _driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            _driver.Manage().Window.Maximize();
            _driver.Url = "http://www.demoqa.com/droppable";
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            _builder = new Actions(_driver);
        }

        [Test]
        [Obsolete]
        public void DragAndDrop_Element_CenterTarget_targetChangeColor()
        {
            IWebElement sourceBox = _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("draggable")));
            IWebElement targetBox = _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("droppable")));

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
        public void DragAndDrop_Element_targetMoveOutOfArea()
        {
            IWebElement sourceBox = _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("draggable")));

            var PosXBefore = sourceBox.Location.X;
            var PosYBefore = sourceBox.Location.Y;

            IWebElement workArea = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@class='col-12 mt-4 col-md-6']")));

            var workAreaPosXBefore = workArea.Location.X;
            var workAreaPosYBefore = workArea.Location.Y;

            int x = PosXBefore * (-1);
            int y = PosYBefore * (-1);

            _builder
                .ClickAndHold(sourceBox)
                .MoveByOffset(x, y)
                .Perform();

            var PosXAfter = sourceBox.Location.X;
            var PosYAfter = sourceBox.Location.Y;

            Assert.IsTrue(PosXAfter < workAreaPosXBefore);
            Assert.IsTrue(PosYAfter < workAreaPosYBefore);
        }

        [Test]
        public void DragAndDrop_Element_CenterTarget_NotReverted()
        {
            IWebElement tabMenuAccepted = _driver.FindElement(By.XPath("//*[@id='droppableExample-tab-revertable']"));
            tabMenuAccepted.Click();

            IWebElement sourceBox = _driver.FindElement(By.Id("notRevertable"));
            IWebElement targetBox = _driver.FindElement(By.XPath("//*[@class='revertable-drop-container']//*[@id='droppable']"));

            var colorBefore = targetBox.GetCssValue("color");

            _builder
                .ClickAndHold(sourceBox)
                .MoveToElement(targetBox)
                .Perform();

            var colorAfter = targetBox.GetAttribute("background-color");

            Assert.AreNotEqual(colorAfter, colorBefore);
        }

      
        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}
