using TextAdventureGame.Library.General.StoryElements;

namespace TextAdventureGame.Library.General
{
    public class Game
    {
        public Story MainStory { get; private set; }

        public Game()
        {
            MainStory = new Story(0, "測試故事");
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
                                    MainStory.StoryName,
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
                MainStory.AddChapter(chapter);
            }
        }
    }
}
