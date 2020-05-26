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
            _driver.Url = "http://www.demoqa.com/droppable";
            _driver.Manage().Window.Maximize();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(100));
            _builder = new Actions(_driver);
        }

        [Obsolete]
        private void ScroolPage()
        {
            IWebElement page = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html")));
            page.SendKeys(Keys.PageDown);
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

            IWebElement workArea = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div/div/div/div[2]/div[2]")));

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
        [Obsolete]
        public void DragAndDrop_Element_CenterTarget_NotAcepted()
        {
            IWebElement tab = _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div/div/div/div[2]/div[2]/div[1]/nav")));
            tab.Click();

            IWebElement tabMenu = _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div/div/div/div[2]/div[2]/div[1]/nav/a[2]")));
            tabMenu.Click();

            IWebElement sourceBox = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div/div/div/div[2]/div[2]/div[1]/div/div[2]/div/div[1]/div[2]")));
            IWebElement targetBox = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div/div/div/div[2]/div[2]/div[1]/div/div[2]/div/div[2]")));

            var colorBefore = targetBox.GetCssValue("color");

            _builder
                .ClickAndHold(sourceBox)
                .MoveToElement(targetBox)
                .Perform();

            var colorAfter = targetBox.GetAttribute("background-color");

            Assert.IsNotNull(colorBefore);
            Assert.IsNull(colorAfter);
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}
