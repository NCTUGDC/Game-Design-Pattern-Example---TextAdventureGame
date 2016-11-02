using System;
using System.Linq;
using TextAdventureGame.Library.General.StoryElements;
using TextAdventureGame.Library.General.StoryElements.PlotTriggerConditions;
using TextAdventureGame.Library.General.StoryElements.PlotTriggerEndEvents.InputStringEvents;

namespace TextAdventureGame.ConsoleEditor.StoryEditorElements
{
    public abstract class PlotTriggerContentControlHandler : EditorControlHandler
    {
        private PlotTriggerElement triggerElement;

        protected PlotTriggerContentControlHandler(PlotTriggerElement triggerElement)
        {
            this.triggerElement = triggerElement;
        }

        protected override bool HandleCommand(string command, out int rollbackLayerCount)
        {
            if (!base.HandleCommand(command, out rollbackLayerCount))
            {
                bool canHandle = true;
                switch (command)
                {
                    case "add condition":
                        AddConditionCommandTask();
                        break;
                    case "remove condition":
                        RemoveConditionCommandTask();
                        break;
                    case "add end event":
                        AddEndEventCommandTask();
                        break;
                    case "remove end event":
                        RemoveEndEventCommandTask();
                        break;
                    default:
                        canHandle = false;
                        break;
                }
                return canHandle;
            }
            else
            {
                return true;
            }
        }
        protected override void HelpCommandTask()
        {
            base.HelpCommandTask();
            Console.WriteLine("\t輸入view檢視劇情資訊");
            Console.WriteLine("\t輸入add condition增加觸發條件");
            Console.WriteLine("\t輸入remove condition移除觸發條件");
            Console.WriteLine("\t輸入add end event增加結束事件");
            Console.WriteLine("\t輸入remove end event移除結束事件");
        }

        #region command tasks
        protected override void ViewCommandTask()
        {
            if(triggerElement.TriggerConditions.Any())
            {
                Console.WriteLine("觸發條件:");
                foreach (var condition in triggerElement.TriggerConditions)
                {
                    Console.WriteLine("\t{0}", condition.ConditionInformation);
                }
            }
            if (triggerElement.TriggerEndEvents.Any())
            {
                Console.WriteLine("結束事件:");
                foreach (var endEvent in triggerElement.TriggerEndEvents)
                {
                    Console.WriteLine("\t{0}", endEvent.EventInformation);
                }
            }
        }
        private void AddConditionCommandTask()
        {
            Console.Write("請輸入要加入的觸發條件ID(輸入cancel取消): ");
            int conditionID = 0;
            string inputString = Console.ReadLine();
            if (inputString != "cancel")
            {
                while (inputString != "cancel" && !int.TryParse(inputString, out conditionID))
                {
                    Console.Write("不合法的輸入 請輸入要加入的觸發條件ID(整數)(輸入cancel取消): ");
                    inputString = Console.ReadLine();
                }
                if (inputString != "cancel")
                {
                    Console.WriteLine("請輸入要加入的觸發條件種類：");
                    Console.WriteLine("\tenter scene 進入場景");
                    inputString = Console.ReadLine();
                    switch(inputString)
                    {
                        case "enter scene":
                            AddEnterSceneConditionTask(conditionID);
                            break;
                        default:
                            Console.WriteLine("加入失敗，條件種類不存在");
                            break;
                    }
                }
            }
        }
        private void RemoveConditionCommandTask()
        {
            Console.Write("請輸入要刪除的觸發條件ID(輸入cancel取消): ");
            string inputString = Console.ReadLine();
            int conditionID = 0;
            while (inputString != "cancel" && !int.TryParse(inputString, out conditionID))
            {
                Console.WriteLine("讀取失敗! 請輸入要刪除的觸發條件ID(輸入cancel取消)");
                inputString = Console.ReadLine();
            }
            if (inputString != "cancel")
            {
                int removedCount = triggerElement.RemoveCondition(conditionID);
                Console.WriteLine("共刪除{0}個觸發條件", removedCount);
            }
        }
        private void AddEndEventCommandTask()
        {
            Console.Write("請輸入要加入的結束事件ID(輸入cancel取消): ");
            int endEventID = 0;
            string inputString = Console.ReadLine();
            if (inputString != "cancel")
            {
                while (inputString != "cancel" && !int.TryParse(inputString, out endEventID))
                {
                    Console.Write("不合法的輸入 請輸入要加入的結束事件ID(整數)(輸入cancel取消): ");
                    inputString = Console.ReadLine();
                }
                if (inputString != "cancel")
                {
                    Console.WriteLine("請輸入要加入的結束事件種類：");
                    Console.WriteLine("\tinput string 輸入字串");
                    inputString = Console.ReadLine();
                    switch (inputString)
                    {
                        case "input string":
                            AddInputStringEndEventTask(endEventID);
                            break;
                        default:
                            Console.WriteLine("加入失敗，事件種類不存在");
                            break;
                    }
                }
            }
        }
        private void RemoveEndEventCommandTask()
        {
            Console.Write("請輸入要刪除的結束事件ID(輸入cancel取消): ");
            string inputString = Console.ReadLine();
            int endEventID = 0;
            while (inputString != "cancel" && !int.TryParse(inputString, out endEventID))
            {
                Console.WriteLine("讀取失敗! 請輸入要刪除的結束事件ID(輸入cancel取消)");
                inputString = Console.ReadLine();
            }
            if (inputString != "cancel")
            {
                int removedCount = triggerElement.RemoveEndEvent(endEventID);
                Console.WriteLine("共刪除{0}個結束事件", removedCount);
            }
        }
        #endregion

        #region add condition tasks
        private void AddEnterSceneConditionTask(int conditionID)
        {
            Console.Write("請輸入場景ID(輸入cancel取消): ");
            int sceneID = 0;
            string inputString = Console.ReadLine();
            if (inputString != "cancel")
            {
                while (inputString != "cancel" && !int.TryParse(inputString, out sceneID))
                {
                    Console.Write("不合法的輸入 請輸入場景ID(整數)(輸入cancel取消): ");
                    inputString = Console.ReadLine();
                }
                if (inputString != "cancel")
                {
                    triggerElement.AddCondition(new EnterSceneCondition(conditionID, sceneID));
                }
            }
        }
        #endregion

        #region add end event tasks
        private void AddInputStringEndEventTask(int endEventID)
        {
            Console.WriteLine("請輸入 輸入字串 事件種類：");
            Console.WriteLine("\tplayer name 輸入玩家名稱");
            string inputString = Console.ReadLine();
            switch (inputString)
            {
                case "player name":
                    triggerElement.AddEndEvent(new InputPlayerNameEvent(endEventID));
                    break;
                default:
                    Console.WriteLine("加入失敗，事件種類不存在");
                    break;
            }
        }
        #endregion
    }
}
