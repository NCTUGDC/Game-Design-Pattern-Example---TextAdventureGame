using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextAdventureGame.Library.General.StoryElements;

namespace TextAdventureGame.Library.UnitTest
{
    [TestClass]
    public class SentenceTest
    {
        [TestMethod]
        public void LoadLineTest()
        {
            Sentence s = new Sentence(0, "test");
            string line1 = "test 1", line2 = "test 2", line3 = "test 3";
            s.AddLine(line1);
            s.AddLine(line2);
            s.AddLine(line3);

            Assert.AreEqual(s.CurrentLine, "");
            Assert.AreEqual(s.ToNextLine(), true);
            Assert.AreEqual(s.CurrentLine, line1);
        }

        [TestMethod]
        public void ToNextLineAndIsEndTest()
        {
            Sentence s = new Sentence(0, "test");
            string line1 = "test 1", line2 = "test 2", line3 = "test 3";

            Assert.AreEqual(s.IsEnd, true);

            s.AddLine(line1);
            s.AddLine(line2);
            s.AddLine(line3);

            Assert.AreEqual(s.IsEnd, false);
            Assert.AreEqual(s.ToNextLine(), true);

            Assert.AreEqual(s.IsEnd, false);
            Assert.AreEqual(s.ToNextLine(), true);

            Assert.AreEqual(s.IsEnd, false);
            Assert.AreEqual(s.ToNextLine(), true);

            Assert.AreEqual(s.CurrentLine, line3);

            Assert.AreEqual(s.IsEnd, true);
            Assert.AreEqual(s.ToNextLine(), false);
        }

        [TestMethod]
        public void JumpToLineTest()
        {
            Sentence s = new Sentence(0, "test");
            string line1 = "test 1", line2 = "test 2", line3 = "test 3";
            s.AddLine(line1);
            s.AddLine(line2);
            s.AddLine(line3);

            Assert.AreEqual(s.JumpToLine(0), true);
            Assert.AreEqual(s.CurrentLine, line1);

            Assert.AreEqual(s.JumpToLine(1), true);
            Assert.AreEqual(s.CurrentLine, line2);

            Assert.AreEqual(s.JumpToLine(5), false);
            Assert.AreEqual(s.CurrentLine, line2);

            Assert.AreEqual(s.JumpToLine(-5), false);
            Assert.AreEqual(s.CurrentLine, line2);
        }
    }
}
