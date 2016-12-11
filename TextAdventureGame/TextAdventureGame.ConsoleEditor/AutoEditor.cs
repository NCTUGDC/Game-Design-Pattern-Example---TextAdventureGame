using System;
using System.Collections.Generic;
using TextAdventureGame.Library.General;
using TextAdventureGame.Library.General.NPCs;
using TextAdventureGame.Library.General.Effectors.AbilityConditionEffectors;
using TextAdventureGame.Library.General.Effectors.ConsumableEffectors;
using TextAdventureGame.Library.General.Effectors.EquipmentEffectors;
using TextAdventureGame.Library.General.Effectors.SkillEffectors;
using TextAdventureGame.Library.General.ItemElements;
using TextAdventureGame.Library.General.Protocols;
using TextAdventureGame.Library.General.StoreElements;
using TextAdventureGame.Library.General.StoryElements;
using TextAdventureGame.Library.General.WorldElements;

namespace TextAdventureGame.ConsoleEditor
{
    public class AutoEditor : EditorControlHandler
    {
        public override string ControlInformation
        {
            get
            {
                return "自動編輯器";
            }
        }

        protected override bool HandleCommand(string command, out int rollbackLayerCount)
        {
            if (!base.HandleCommand(command, out rollbackLayerCount))
            {
                bool canHandle = true;
                switch (command)
                {
                    case "exit":
                        ExitCommandTask(out rollbackLayerCount);
                        break;
                    case "setup":
                        SetupCommandTask();
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
        #region command tasks
        protected override void HelpCommandTask()
        {
            base.HelpCommandTask();
            Console.WriteLine("\t輸入exit離開編輯器");
            Console.WriteLine("\t輸入setup完成所有編輯並儲存");
        }
        protected override void ViewCommandTask()
        {
            HelpCommandTask();
        }
        private void ExitCommandTask(out int rollbackLayerCount)
        {
            rollbackLayerCount = 1;
        }
        private void SetupCommandTask()
        {
            SetupNPC();
            SetupSkill();
            SetupMonster();
            SetupItem();
            SetupStore();
            SetupScene();
            SetupStory();

            Console.WriteLine("全部完成!");
        }
        #endregion

        private void SetupNPC()
        {
            NPC_Factory factory = new NPC_Factory();

            factory.AddNPC(new NPC(1, "圖書管理員", "有什麼事嗎?"));
            factory.AddNPC(new NPC(2, "雷克爾", "我是年代記作家雷克爾。"));
            factory.AddNPC(new NPC(3, "牧師", "造物主會守護我們的。"));
            factory.AddNPC(new NPC(4, "安妮", "最近的蔬菜又漲價了。"));
            factory.AddNPC(new NPC(5, "查理", "我是歐里手藝最好的石匠。"));
            factory.AddNPC(new NPC(6, "石碑", "(莊嚴的石碑)"));
            factory.AddNPC(new NPC(7, "圖書館的書架", "(書架上有各式各樣的書籍)"));
            factory.AddNPC(new NPC(8, "陌生的男子", "... ...。"));
            factory.AddNPC(new NPC(9, "愛希兒", "... ...。"));
            factory.AddNPC(new NPC(10, "尼克", "聽說在歐里森林深處有一座湖泊。"));
            factory.AddNPC(new NPC(11, "培庸", "去茶館總是可以聽到許多故事。"));
            factory.AddNPC(new Seller(12, "水果店老闆", "這裡的水果是從南邊城鎮運送過來的。", 1));
            factory.AddNPC(new NPC(13, "蒙亞", "今年八成會豐收。"));
            factory.AddNPC(new NPC(14, "守衛", "(值班中)。"));
            factory.AddNPC(new Seller(15, "茶店老闆", "偷偷告訴你，其實我這裡還有賣酒。", 2));
            factory.AddNPC(new Seller(16, "兌幣員", "金幣兌銀幣1比20，銀幣兌銅幣1比40。", 3));
            factory.AddNPC(new Seller(17, "裝備行老闆", "我這裡除了劍之外還有魔杖。", 4));
            factory.AddNPC(new Seller(18, "藥水店老闆", "藥水是戰鬥必備的物品。", 5));
            factory.AddNPC(new NPC(19, "休息用的草堆", "(休息一下)"));

            NPC_Factory.SaveFactory("NPC_Factory", factory);
            Console.WriteLine("NPC設置完成!");
        }
        private void SetupSkill()
        {
            SkillFactory factory = new SkillFactory();

            Skill 硬化利刃 = new Skill(1, "硬化利刃", 10, false);
            硬化利刃.AddAbilityConditionEffector(new LevelConditionEffector(3));
            硬化利刃.AddAbilityConditionEffector(new PowerConditionEffector(8));
            硬化利刃.AddSkillEffector(new PhysicalAttackSkillEffector(20));
            factory.AddSkill(硬化利刃);

            Skill 落石術 = new Skill(2, "落石術", 10, false);
            落石術.AddAbilityConditionEffector(new LevelConditionEffector(3));
            落石術.AddAbilityConditionEffector(new MagicConditionEffector(8));
            落石術.AddSkillEffector(new MagicalAttackSkillEffector(20));
            factory.AddSkill(落石術);

            SkillFactory.SaveFactory("SkillFactory", factory);
            Console.WriteLine("Skill設置完成!");
        }
        private void SetupMonster()
        {
            MonsterFactory factory = new MonsterFactory();

            factory.AddMonster(new Monster(1, "蜘蛛", new AbilityFactors
            {
                Level = 1,
                MaxHP = 10,
                MaxSP = 0,
                Power = 3,
                Magic = 1,
                Agile = 1,
                Sensibility = 1
            }, 5, new List<int>(), new Dictionary<int, int>()));

            factory.AddMonster(new Monster(2, "小鳥", new AbilityFactors
            {
                Level = 2,
                MaxHP = 20,
                MaxSP = 5,
                Power = 3,
                Magic = 2,
                Agile = 2,
                Sensibility = 2
            }, 15, new List<int> { 33 }, new Dictionary<int, int> { { 33, 20 } }));

            factory.AddMonster(new Monster(3, "小妖精", new AbilityFactors
            {
                Level = 4,
                MaxHP = 35,
                MaxSP = 35,
                Power = 6,
                Magic = 3,
                Agile = 3,
                Sensibility = 3
            }, 30, new List<int> { 34 }, new Dictionary<int, int> { { 34, 40 } }));

            factory.AddMonster(new Monster(4, "地精", new AbilityFactors
            {
                Level = 6,
                MaxHP = 50,
                MaxSP = 50,
                Power = 4,
                Magic = 4,
                Agile = 5,
                Sensibility = 8
            }, 50, new List<int> { 28 }, new Dictionary<int, int> { { 28, 25 } }));

            factory.AddMonster(new Monster(5, "土蜘蛛(雷思瑞)", new AbilityFactors
            {
                Level = 8,
                MaxHP = 80,
                MaxSP = 60,
                Power = 12,
                Magic = 2,
                Agile = 7,
                Sensibility = 6
            }, 100, new List<int> { 1, 4, 3 }, new Dictionary<int, int> { { 1, 70 }, { 4, 40 }, { 3, 15 } }));

            factory.AddMonster(new Monster(6, "水鈴蛇(雷思瑞)", new AbilityFactors
            {
                Level = 12,
                MaxHP = 120,
                MaxSP = 200,
                Power = 3,
                Magic = 15,
                Agile = 12,
                Sensibility = 9
            }, 180, new List<int> { 10, 9, 13, 14 }, new Dictionary<int, int> { { 10, 100 }, { 9, 60 }, { 13, 30 }, { 14, 15 } }));

            factory.AddMonster(new Monster(7, "火蜥蜴(雷思瑞)", new AbilityFactors
            {
                Level = 15,
                MaxHP = 200,
                MaxSP = 100,
                Power = 18,
                Magic = 2,
                Agile = 14,
                Sensibility = 11
            }, 250, new List<int> { 17, 19 }, new Dictionary<int, int> { { 17, 50 }, { 19, 20 } }));

            factory.AddMonster(new Monster(8, "樹精(雷思瑞)", new AbilityFactors
            {
                Level = 18,
                MaxHP = 300,
                MaxSP = 350,
                Power = 20,
                Magic = 20,
                Agile = 5,
                Sensibility = 12
            }, 400, new List<int> { 28, 26 }, new Dictionary<int, int> { { 28, 50 }, { 26, 20 } }));

            factory.AddMonster(new Monster(9, "獨角獸(雷思瑞)", new AbilityFactors
            {
                Level = 25,
                MaxHP = 500,
                MaxSP = 600,
                Power = 17,
                Magic = 19,
                Agile = 18,
                Sensibility = 24
            }, 1000, new List<int> { 38, 36, 37, 39 }, new Dictionary<int, int> { { 38, 93 }, { 36, 33 }, { 37, 13 }, { 39, 3 } }));

            MonsterFactory.SaveFactory("MonsterFactory", factory);
            Console.WriteLine("Monster設置完成!");
        }
        private void SetupItem()
        {
            ItemFactory factory = new ItemFactory();
            ItemFactory.SaveFactory("ItemFactory", factory);
            Console.WriteLine("Item設置完成!");
        }
        private void SetupStore()
        {
            StoreFactory factory = new StoreFactory();

            Store 水果店 = new Store(1, "水果店");
            水果店.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> {new TradeItemInformation { itemID = 8, count = 20 } }, 
                rewards: new List<TradeItemInformation> { new TradeItemInformation { itemID = 9, count = 1 } })
            );
            水果店.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 8, count = 10 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { itemID = 10, count = 1 } })
            );
            factory.AddStore(水果店);

            Store 茶店 = new Store(2, "茶店");
            茶店.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 1 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { itemID = 11, count = 1 } })
            );
            茶店.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 11 }, new TradeItemInformation { itemID = 8, count = 50 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { itemID = 12, count = 1 } })
            );
            factory.AddStore(茶店);

            Store 兌幣站 = new Store(3, "兌幣站");
            兌幣站.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 6, count = 1 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 40 } })
            );
            兌幣站.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 40 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { itemID = 6, count = 1 } })
            );
            兌幣站.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 1 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { itemID = 8, count = 100 } })
            );
            兌幣站.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 8, count = 100 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 1 } })
            );
            factory.AddStore(兌幣站);

            Store 裝備店 = new Store(4, "裝備店");
            裝備店.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 20 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { itemID = 22, count = 1 } })
            );
            裝備店.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 35 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { itemID = 23, count = 1 } })
            );
            裝備店.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 60 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { itemID = 24, count = 1 } })
            );
            裝備店.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 25 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { itemID = 25, count = 1 } })
            );
            裝備店.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 45 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { itemID = 26, count = 1 } })
            );
            裝備店.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 80 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { itemID = 27, count = 1 } })
            );

            裝備店.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 10 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { itemID = 28, count = 1 } })
            );
            裝備店.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 25 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { itemID = 29, count = 1 } })
            );
            裝備店.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 40 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { itemID = 30, count = 1 } })
            );
            裝備店.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 8 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { itemID = 31, count = 1 } })
            );
            裝備店.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 12 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { itemID = 32, count = 1 } })
            );
            裝備店.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 20 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { itemID = 33, count = 1 } })
            );

            裝備店.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 30 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { itemID = 34, count = 1 } })
            );
            裝備店.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 55 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { itemID = 35, count = 1 } })
            );
            裝備店.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 80 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { itemID = 36, count = 1 } })
            );
            裝備店.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 10 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { itemID = 37, count = 1 } })
            );
            裝備店.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 20 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { itemID = 38, count = 1 } })
            );
            裝備店.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 30 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { itemID = 39, count = 1 } })
            );

            裝備店.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 12 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { itemID = 40, count = 1 } })
            );
            裝備店.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 40 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { itemID = 41, count = 28 } })
            );
            裝備店.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 50 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { itemID = 42, count = 1 } })
            );
            裝備店.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 2 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { itemID = 43, count = 1 } })
            );
            裝備店.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 5 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { itemID = 44, count = 1 } })
            );
            裝備店.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 20 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { itemID = 45, count = 1 } })
            );
            factory.AddStore(裝備店);

            Store 藥水店 = new Store(5, "藥水店");
            藥水店.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 3 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { count = 14, itemID = 1 } })
            );
            藥水店.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 6 }, new TradeItemInformation { itemID = 8, count = 50 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { count = 15, itemID = 1 } })
            );
            藥水店.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 12 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { count = 16, itemID = 1 } })
            );
            藥水店.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 24 }, new TradeItemInformation { itemID = 8, count = 50 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { count = 17, itemID = 1 } })
            );
            藥水店.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 5 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { count = 18, itemID = 1 } })
            );
            藥水店.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 10 }, new TradeItemInformation { itemID = 8, count = 50 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { count = 19, itemID = 1 } })
            );
            藥水店.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 20 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { count = 20, itemID = 1 } })
            );
            藥水店.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 40 }, new TradeItemInformation { itemID = 8, count = 50 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { count = 21, itemID = 1 } })
            );
            factory.AddStore(藥水店);

            StoreFactory.SaveFactory("StoreFactory", factory);
            Console.WriteLine("Store設置完成!");
        }
        private void SetupScene()
        {
            World world = new World();
            World.SaveWorld("World", world);
            Console.WriteLine("Scene設置完成!");
        }
        private void SetupStory()
        {
            Story story = new Story();
            Story.SaveStory("MainStory", story);
            Console.WriteLine("Story設置完成!");
        }
    }
}
