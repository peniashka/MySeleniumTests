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
    class InteractionSelectable : BaseVariables
    {

        [SetUp]
        [Obsolete]
        public void Setup()
        {
           Initialize(); 
           Driver.Url = "http://www.demoqa.com/selectable";
        }

        [Test]
        [Obsolete]
        public void SelectItem_ExpactedToChangeColor()
        {

            IWebElement sourceBox = Driver.FindElement(By.XPath("//*[@id='verticalListContainer']//li[normalize-space(text())='Cras justo odio']"));
            var colorBefore = sourceBox.GetCssValue("color");

            Builder
                .Click(sourceBox)
                .Perform();

            var colorAfter = sourceBox.GetCssValue("color");

            Assert.AreNotEqual(colorAfter, colorBefore);
        }

        [Test]
        [Obsolete]
        public void SelectItems_ExpactedToSelectAllOfThem()
        {
            IWebElement tab = Driver.FindElement(By.XPath("//*[@id='demo-tab-grid']"));
            tab.Click();

            IWebElement element1 = Driver.FindElement(By.XPath("//*[@id='row1']//li[normalize-space(text()) = 'One']"));
            IWebElement element2 = Driver.FindElement(By.XPath("//*[@id='row1']//li[normalize-space(text()) = 'Two']"));
            IWebElement element3 = Driver.FindElement(By.XPath("//*[@id='row1']//li[normalize-space(text()) = 'Three']"));
            IWebElement element4 = Driver.FindElement(By.XPath("//*[@id='row2']//li[normalize-space(text()) = 'Four']"));
            IWebElement element5 = Driver.FindElement(By.XPath("//*[@id='row2']//li[normalize-space(text()) = 'Five']"));
            IWebElement element6 = Driver.FindElement(By.XPath("//*[@id='row2']//li[normalize-space(text()) = 'Six']"));
            IWebElement element7 = Driver.FindElement(By.XPath("//*[@id='row3']//li[normalize-space(text()) = 'Seven']"));
            IWebElement element8 = Driver.FindElement(By.XPath("//*[@id='row3']//li[normalize-space(text()) = 'Eight']"));
            IWebElement element9 = Driver.FindElement(By.XPath("//*[@id='row3']//li[normalize-space(text()) = 'Nine']"));
            
            var targetBoxListBefore = Driver.FindElements(By.XPath("//*[@id='gridContainer']//li"));
            int countElement = targetBoxListBefore.Count;

            Builder
                .Click(element1)
                .KeyDown(Keys.Control)
                .Click(element2)
                .Click(element3)
                .Click(element4)
                .Click(element5)
                .Click(element6)
                .Click(element7)
                .Click(element8)
                .Click(element9)
                .KeyUp(Keys.Control)
                .Release()
                .Perform();

            int countSelectedElememts = 0;
            var targetBoxListAfter = Driver.FindElements(By.XPath("//*[@id='gridContainer']//li"));

            foreach (IWebElement button in targetBoxListAfter)
            {
                var targetColor = button.GetCssValue("color");

                if (targetColor == "rgba(255, 255, 255, 1)")
                {
                    countSelectedElememts += 1;
                }
            }

            Assert.AreEqual(countElement, countSelectedElememts);
        }

        [TearDown]
        public void TearDown()
        {
            Driver.Quit();
        }
    }
}
