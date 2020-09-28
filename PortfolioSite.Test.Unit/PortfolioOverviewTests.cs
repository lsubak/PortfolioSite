using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;

namespace PortfolioSite.Test.Unit
{
    class PortfolioOverviewTests
    {
        string PortfolioUrl = "https://localhost:44319";
        IWebDriver Driver;

        [SetUp]
        public void StartBrowser()
        {
            Driver = new ChromeDriver();
            Driver.Manage().Window.Maximize();
        }

        [Test]
        public void AllPortfolioThumbs_AreDisplayed()
        {
            Driver.Url = PortfolioUrl;

            var thumbImgIds = new List<string>
            {
                "thumb-img-adcouncil1",
                "thumb-img-adcouncil2",
                "thumb-img-adcouncil3",
                "thumb-img-arthistory1",
                "thumb-img-arthistory2",
                "thumb-img-arthistory3",
                "thumb-img-arthistory4",
                "thumb-img-earthday",
                "thumb-img-ellaenchanted",
                "thumb-img-helvetica",
                "thumb-img-oldspice1",
                "thumb-img-oldspice2",
                "thumb-img-saulbass",
                "thumb-img-squishybear",
                "thumb-img-teahouse",
                "thumb-img-westside1",
                "thumb-img-westside2",
                "thumb-img-witch"
            };

            foreach (var thumbId in thumbImgIds)
            {
                var element = Driver.FindElement(By.Id(thumbId));
                Assert.IsTrue(element.Displayed);
            }

            var allThumbImages = Driver.FindElements(By.ClassName("portfolio-image"));
            Assert.AreEqual(thumbImgIds.Count, allThumbImages.Count);
        }

        [Test]
        public void ClickingFirstPortfolioThumb_ActivatesCorrectOverlay()
        {
            Driver.Url = PortfolioUrl;

            var thumbImage = Driver.FindElement(By.Id("thumb-img-squishybear"));

            thumbImage.Click();

            var modalImage = Driver.FindElement(By.Id("modal-img-squishybear"));
            var modalDesc = Driver.FindElement(By.Id("desc-squishybear"));

            var modalOverlay = Driver.FindElement(By.CssSelector(".portfolio-image-modal"));
            var modalLeftButton = Driver.FindElement(By.CssSelector(".modal-left-button"));
            var modalRightButton = Driver.FindElement(By.CssSelector(".modal-right-button"));
            var modalCloseButton = Driver.FindElement(By.CssSelector(".modal-close-button"));

            Assert.IsTrue(modalImage.Displayed);
            Assert.IsTrue(modalDesc.Displayed);
            Assert.IsTrue(modalOverlay.Displayed);
            Assert.IsFalse(modalLeftButton.Displayed);
            Assert.IsTrue(modalRightButton.Displayed);
            Assert.IsTrue(modalCloseButton.Displayed);
        }

        [TestCase("thumb-img-adcouncil1", "modal-img-adcouncil1", "desc-adcouncil1")]
        [TestCase("thumb-img-adcouncil2", "modal-img-adcouncil2", "desc-adcouncil2")]
        [TestCase("thumb-img-adcouncil3", "modal-img-adcouncil3", "desc-adcouncil3")]
        [TestCase("thumb-img-arthistory1", "modal-img-arthistory1", "desc-arthistory1")]
        [TestCase("thumb-img-arthistory2", "modal-img-arthistory2", "desc-arthistory2")]
        [TestCase("thumb-img-arthistory3", "modal-img-arthistory3", "desc-arthistory3")]
        [TestCase("thumb-img-arthistory4", "modal-img-arthistory4", "desc-arthistory4")]
        [TestCase("thumb-img-earthday", "modal-img-earthday", "desc-earthday")]
        [TestCase("thumb-img-ellaenchanted", "modal-img-ellaenchanted", "desc-ellaenchanted")]
        [TestCase("thumb-img-oldspice1", "modal-img-oldspice1", "desc-oldspice1")]
        [TestCase("thumb-img-oldspice2", "modal-img-oldspice2", "desc-oldspice2")]
        [TestCase("thumb-img-saulbass", "modal-img-saulbass", "desc-saulbass")]
        [TestCase("thumb-img-teahouse", "modal-img-teahouse", "desc-teahouse")]
        [TestCase("thumb-img-westside1", "modal-img-westside1", "desc-westside1")]
        [TestCase("thumb-img-westside2", "modal-img-westside2", "desc-westside2")]
        [TestCase("thumb-img-witch", "modal-img-witch", "desc-witch")]
        public void ClickingMiddlePortfolioThumbs_ActivatesCorrectOverlay(string thumbImgId, string modalImgId, string descId)
        {
            Driver.Url = PortfolioUrl;

            var thumbImage = Driver.FindElement(By.Id(thumbImgId));

            thumbImage.Click();

            var modalImage = Driver.FindElement(By.Id(modalImgId));
            var modalDesc = Driver.FindElement(By.Id(descId));

            var modalOverlay = Driver.FindElement(By.CssSelector(".portfolio-image-modal"));
            var modalLeftButton = Driver.FindElement(By.CssSelector(".modal-left-button"));
            var modalRightButton = Driver.FindElement(By.CssSelector(".modal-right-button"));
            var modalCloseButton = Driver.FindElement(By.CssSelector(".modal-close-button"));

            Assert.IsTrue(modalImage.Displayed);
            Assert.IsTrue(modalDesc.Displayed);
            Assert.IsTrue(modalOverlay.Displayed);
            Assert.IsTrue(modalLeftButton.Displayed);
            Assert.IsTrue(modalRightButton.Displayed);
            Assert.IsTrue(modalCloseButton.Displayed);
        }

        [Test]
        public void ClickingLastPortfolioThumb_ActivatesCorrectOverlay()
        {
            Driver.Url = PortfolioUrl;

            var thumbImage = Driver.FindElement(By.Id("thumb-img-helvetica"));

            thumbImage.Click();

            var modalImage = Driver.FindElement(By.Id("modal-img-helvetica"));
            var modalDesc = Driver.FindElement(By.Id("desc-helvetica"));

            var modalOverlay = Driver.FindElement(By.CssSelector(".portfolio-image-modal"));
            var modalLeftButton = Driver.FindElement(By.CssSelector(".modal-left-button"));
            var modalRightButton = Driver.FindElement(By.CssSelector(".modal-right-button"));
            var modalCloseButton = Driver.FindElement(By.CssSelector(".modal-close-button"));

            Assert.IsTrue(modalImage.Displayed);
            Assert.IsTrue(modalDesc.Displayed);
            Assert.IsTrue(modalOverlay.Displayed);
            Assert.IsTrue(modalLeftButton.Displayed);
            Assert.IsFalse(modalRightButton.Displayed);
            Assert.IsTrue(modalCloseButton.Displayed);
        }

        [TearDown]
        public void CloseBrowser()
        {
            Driver.Quit();
        }
    }
}
