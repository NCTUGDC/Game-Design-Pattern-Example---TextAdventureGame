using MsgPack.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace TextAdventureGame.Library.General.StoryElements
{
    public class Chapter
    {
        [MessagePackMember(id: 0, Name = "ChapterID")]
        public int ChapterID { get; private set; }

        [MessagePackMember(id: 1, Name = "ChapterName")]
        public string ChapterName { get; private set; }

        [MessagePackMember(id: 2, Name = "sections")]
        private List<Section> sections;

        [MessagePackMember(id: 3, Name = "currentSectionIndex")]
        private int currentSectionIndex;

        [MessagePackRuntimeCollectionItemType]
        [MessagePackMember(id: 4, Name = "triggerConditions")]
        private List<PlotTriggerCondition> triggerConditions;

        public bool IsEnd { get { return currentSectionIndex == sections.Count - 1; } }
        public Section CurrentSection { get { return (currentSectionIndex >= 0) ? sections[currentSectionIndex] : null; } }
        public int SectionCount { get { return sections.Count; } }
        public IEnumerable<Section> Sections { get { return sections; } }
        public IEnumerable<PlotTriggerCondition> TriggerConditions { get { return triggerConditions; } }

        [MessagePackDeserializationConstructor]
        public Chapter() { }
        public Chapter(int chapterID, string chapterName)
        {
            ChapterID = chapterID;
            ChapterName = chapterName;
            sections = new List<Section>();
            triggerConditions = new List<PlotTriggerCondition>();
            currentSectionIndex = -1;
        }
        public bool ContainsSection(int sectionID)
        {
            return sections.Any(x => x.SectionID == sectionID);
        }
        public Section FindSection(int sectionID)
        {
            return sections.Find(x => x.SectionID == sectionID);
        }
        public bool AddSection(Section section)
        {
            if (section != null && !ContainsSection(section.SectionID))
            {
                sections.Add(section);
                return true;
            }
            else
            {
                return false;
            }
        }
        public int RemoveSection(int sectionID)
        {
            return sections.RemoveAll(x => x.SectionID == sectionID);
        }

        public bool ToNextSection()
        {
            if (IsEnd)
            {
                return false;
            }
            else
            {
                currentSectionIndex++;
                return true;
            }
        }
        public bool JumpToSection(int sectionID)
        {
            if (sections.Any(x => x.SectionID == sectionID))
            {
                currentSectionIndex = sections.FindIndex(x => x.SectionID == sectionID);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void JumpToStart()
        {
            currentSectionIndex = -1;
        }

        public bool IsSufficientPlotTriggerConditions(List<IPlotTriggerConditionTarget> targets)
        {
            return triggerConditions.TrueForAll(x => x.IsEligible(targets));
        }
    }
}
