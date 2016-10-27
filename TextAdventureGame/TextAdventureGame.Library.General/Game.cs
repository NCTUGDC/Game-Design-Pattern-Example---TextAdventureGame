using TextAdventureGame.Library.General.PlotElements;

namespace TextAdventureGame.Library.General
{
    public class Game
    {
        public Plot MainPlot { get; private set; }

        public Game()
        {
            MainPlot = new Plot(0, "測試劇情");
            for (int chapterIndex = 0; chapterIndex < 3; chapterIndex++)
            {
                Chapter chapter = new Chapter(chapterIndex, string.Format("第{0}章", chapterIndex));
                for (int sectionIndex = 0; sectionIndex < 6; sectionIndex++)
                {
                    Section section = new Section(sectionIndex, sectionIndex.ToString() + "節");
                    for (int paragraphIndex = 0; paragraphIndex < 10; paragraphIndex++)
                    {
                        Paragraph paragraph = new Paragraph(paragraphIndex);
                        for (int sentenceIndex = 0; sentenceIndex < 2; sentenceIndex++)
                        {
                            Sentence sentence = new Sentence(sentenceIndex, "speaker" + sentenceIndex.ToString());
                            for (int lineIndex = 0; lineIndex < 3; lineIndex++)
                            {
                                sentence.AddLine(string.Format("{0} {1} 第{2}節 第{3}段 第{4}句 第{5}行",
                                    MainPlot.PlotName,
                                    chapter.ChapterName,
                                    sectionIndex,
                                    paragraphIndex,
                                    sentenceIndex,
                                    lineIndex
                                    ));
                            }
                            paragraph.AddSentence(sentence);
                        }
                        section.AddParagraph(paragraph);
                    }
                    chapter.AddSection(section);
                }
                MainPlot.AddChapter(chapter);
            }
        }
    }
}
