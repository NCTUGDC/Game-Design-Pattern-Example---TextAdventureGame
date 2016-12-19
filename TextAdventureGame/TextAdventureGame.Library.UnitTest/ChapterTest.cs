using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextAdventureGame.Library.General.StoryElements;
using System.Collections.Generic;

namespace TextAdventureGame.Library.UnitTest
{
    [TestClass]
    public class ChapterTest
    {
        [TestMethod]
        public void IsSufficientPlotTriggerConditionsTest()
        {
            Chapter chapter = new Chapter(0, "test");

            //Assert.AreEqual(chapter.IsSufficientPlotTriggerConditions(), false);
            Assert.AreEqual(chapter.IsSufficientPlotTriggerConditions(), true);
        }
    }
}
