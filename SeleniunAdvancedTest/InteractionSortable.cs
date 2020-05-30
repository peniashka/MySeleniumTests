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
    class InteractionSortable
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
            _driver.Url = "http://www.demoqa.com/sortable";
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            _builder = new Actions(_driver);
        }

        [Test]
        [Obsolete]
        public void Sortable_MoveFirstElement_OnSecondPosition_ExpectedYpositionToBeGreaterWith50()
        {
            IWebElement sourceBox = _driver.FindElement(By.XPath("//*[@id='sortableContainer']//div[normalize-space(text())='One']"));
            IWebElement targetBox = _driver.FindElement(By.XPath("//*[@id='sortableContainer']//div[normalize-space(text())='Two']"));

            int sourcePosYBefore = sourceBox.Location.Y;
           
            _builder
                .Click(sourceBox)
                .ClickAndHold(sourceBox)
                .MoveToElement(targetBox)
                .MoveByOffset(0, 10)
                .Release()
                .Perform();

            sourceBox = _driver.FindElement(By.XPath("//*[@id='sortableContainer']//div[normalize-space(text())='One']"));

            int sourcePosYAfter = sourceBox.Location.Y;
            int expectedPosY = sourcePosYBefore + 50;

            Assert.AreEqual(expectedPosY, sourcePosYAfter);
        }

        [Test]
        [Obsolete]
        public void Sortable_MoveFirstElement_ExpactedXpositionToBeTheSame_AfterMovement()
        {
            IWebElement sourceBox = _driver.FindElement(By.XPath("//*[@id='sortableContainer']//div[normalize-space(text())='One']"));
            IWebElement targetBox = _driver.FindElement(By.XPath("//*[@id='sortableContainer']//div[normalize-space(text())='Six']"));

            int sourcePosXBefore = sourceBox.Location.X;

            _builder
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
        [Obsolete]
        public void Sortable_MoveFirstElement_OnSecondPosition()
        {
            IWebElement sourceBox = _driver.FindElement(By.XPath("//*[@id='sortableContainer']//div[normalize-space(text())='One']"));
            IWebElement targetBox = _driver.FindElement(By.XPath("//*[@id='sortableContainer']//div[normalize-space(text())='Two']"));

            _builder
                .Click(sourceBox)
                .ClickAndHold(sourceBox)
                .MoveToElement(targetBox)
                .MoveByOffset(0, 10)
                .Release()
                .Perform();

            var oneNewText = _wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#demo-tabpane-list > div > div:nth-child(2)"))).Text;
            Assert.AreEqual("One", oneNewText);
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}
