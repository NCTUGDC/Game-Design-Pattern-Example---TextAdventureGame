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

            Skill 尖石切裂 = new Skill(3, "尖石切裂", 30, false);
            尖石切裂.AddAbilityConditionEffector(new LevelConditionEffector(8));
            尖石切裂.AddAbilityConditionEffector(new PowerConditionEffector(20));
            尖石切裂.AddSkillEffector(new PhysicalAttackSkillEffector(20));
            factory.AddSkill(尖石切裂);

            Skill 土壤軟化 = new Skill(4, "土壤軟化", 30, true);
            土壤軟化.AddAbilityConditionEffector(new LevelConditionEffector(7));
            土壤軟化.AddAbilityConditionEffector(new MagicConditionEffector(18));
            土壤軟化.AddSkillEffector(new TargetSpeedPointSkillEffector(3, -15));
            factory.AddSkill(土壤軟化);

            Skill 結晶巨劍 = new Skill(5, "結晶巨劍", 50, false);
            結晶巨劍.AddAbilityConditionEffector(new LevelConditionEffector(11));
            結晶巨劍.AddAbilityConditionEffector(new PowerConditionEffector(28));
            結晶巨劍.AddSkillEffector(new PhysicalAttackSkillEffector(80));
            factory.AddSkill(結晶巨劍);

            Skill 地之牙 = new Skill(6, "地之牙", 50, false);
            地之牙.AddAbilityConditionEffector(new LevelConditionEffector(11));
            地之牙.AddAbilityConditionEffector(new MagicConditionEffector(28));
            地之牙.AddSkillEffector(new MagicalAttackSkillEffector(80));
            factory.AddSkill(地之牙);

            Skill 大地要塞 = new Skill(7, "大地要塞", 150, false);
            大地要塞.AddAbilityConditionEffector(new LevelConditionEffector(16));
            大地要塞.AddAbilityConditionEffector(new PowerConditionEffector(35));
            大地要塞.AddSkillEffector(new CasterPhysicalDefencePointSkillEffector(3, 1000));
            factory.AddSkill(大地要塞);

            Skill 地裂結界 = new Skill(8, "地裂結界", 200, true);
            地裂結界.AddAbilityConditionEffector(new LevelConditionEffector(16));
            地裂結界.AddAbilityConditionEffector(new MagicConditionEffector(35));
            地裂結界.AddSkillEffector(new MagicalAttackSkillEffector(180));
            factory.AddSkill(地裂結界);

            Skill 循環修復 = new Skill(9, "循環修復", 5, false);
            循環修復.AddAbilityConditionEffector(new LevelConditionEffector(5));
            循環修復.AddAbilityConditionEffector(new PowerConditionEffector(2));
            循環修復.AddAbilityConditionEffector(new SensibilityConditionEffector(8));
            循環修復.AddSkillEffector(new CasterHP_Ratio_SkillEffector(0.2f));
            factory.AddSkill(循環修復);

            Skill 水流噴射 = new Skill(10, "水流噴射", 15, false);
            水流噴射.AddAbilityConditionEffector(new LevelConditionEffector(4));
            水流噴射.AddAbilityConditionEffector(new MagicConditionEffector(10));
            水流噴射.AddSkillEffector(new MagicalAttackSkillEffector(28));
            factory.AddSkill(水流噴射);

            Skill 凝霜劍 = new Skill(11, "凝霜劍", 25, false);
            凝霜劍.AddAbilityConditionEffector(new LevelConditionEffector(10));
            凝霜劍.AddAbilityConditionEffector(new PowerConditionEffector(18));
            凝霜劍.AddAbilityConditionEffector(new SensibilityConditionEffector(5));
            凝霜劍.AddSkillEffector(new PhysicalAttackSkillEffector(50));
            凝霜劍.AddSkillEffector(new TargetSpeedPointSkillEffector(2, -5));
            factory.AddSkill(凝霜劍);

            Skill 寒冰箭 = new Skill(12, "寒冰箭", 32, false);
            寒冰箭.AddAbilityConditionEffector(new LevelConditionEffector(9));
            寒冰箭.AddAbilityConditionEffector(new MagicConditionEffector(15));
            寒冰箭.AddAbilityConditionEffector(new SensibilityConditionEffector(5));
            寒冰箭.AddSkillEffector(new MagicalAttackSkillEffector(55));
            寒冰箭.AddSkillEffector(new TargetSpeedPointSkillEffector(2, -8));
            factory.AddSkill(寒冰箭);

            Skill 水精靈鎧甲 = new Skill(13, "水精靈鎧甲", 80, false);
            水精靈鎧甲.AddAbilityConditionEffector(new LevelConditionEffector(15));
            水精靈鎧甲.AddAbilityConditionEffector(new PowerConditionEffector(20));
            水精靈鎧甲.AddAbilityConditionEffector(new SensibilityConditionEffector(10));
            水精靈鎧甲.AddSkillEffector(new CasterMagicalDefencePointSkillEffector(3, 120));
            factory.AddSkill(水精靈鎧甲);

            Skill 水龍嘆息 = new Skill(14, "水龍嘆息", 80, false);
            水龍嘆息.AddAbilityConditionEffector(new LevelConditionEffector(15));
            水龍嘆息.AddAbilityConditionEffector(new MagicConditionEffector(32));
            水龍嘆息.AddSkillEffector(new MagicalAttackSkillEffector(150));
            factory.AddSkill(水龍嘆息);

            Skill 空間能量吸取 = new Skill(15, "空間能量吸取", 80, false);
            空間能量吸取.AddAbilityConditionEffector(new LevelConditionEffector(17));
            空間能量吸取.AddAbilityConditionEffector(new PowerConditionEffector(10));
            空間能量吸取.AddAbilityConditionEffector(new SensibilityConditionEffector(20));
            空間能量吸取.AddSkillEffector(new CasterSP_Ratio_SkillEffector(0.5f));
            factory.AddSkill(空間能量吸取);

            Skill 資訊震盪 = new Skill(16, "資訊震盪", 250, true);
            資訊震盪.AddAbilityConditionEffector(new LevelConditionEffector(18));
            資訊震盪.AddAbilityConditionEffector(new MagicConditionEffector(15));
            資訊震盪.AddAbilityConditionEffector(new SensibilityConditionEffector(20));
            資訊震盪.AddSkillEffector(new TargetStopActionSkillEffector(4));
            factory.AddSkill(資訊震盪);

            Skill 高溫 = new Skill(17, "高溫", 15, false);
            高溫.AddAbilityConditionEffector(new LevelConditionEffector(4));
            高溫.AddAbilityConditionEffector(new PowerConditionEffector(10));
            高溫.AddAbilityConditionEffector(new AgileConditionEffector(3));
            高溫.AddSkillEffector(new CasterPhysicalAttackPointSkillEffector(3, 15));
            factory.AddSkill(高溫);

            Skill 火焰放射 = new Skill(18, "火焰放射", 20, false);
            火焰放射.AddAbilityConditionEffector(new LevelConditionEffector(5));
            火焰放射.AddAbilityConditionEffector(new MagicConditionEffector(10));
            火焰放射.AddAbilityConditionEffector(new AgileConditionEffector(5));
            火焰放射.AddSkillEffector(new MagicalAttackSkillEffector(36));
            factory.AddSkill(火焰放射);

            Skill 烈焰劍 = new Skill(19, "烈焰劍", 30, false);
            烈焰劍.AddAbilityConditionEffector(new LevelConditionEffector(9));
            烈焰劍.AddAbilityConditionEffector(new PowerConditionEffector(20));
            烈焰劍.AddAbilityConditionEffector(new AgileConditionEffector(6));
            烈焰劍.AddSkillEffector(new PhysicalAttackSkillEffector(58));
            factory.AddSkill(烈焰劍);

            Skill 烈焰風暴 = new Skill(20, "烈焰風暴", 60, true);
            烈焰風暴.AddAbilityConditionEffector(new LevelConditionEffector(10));
            烈焰風暴.AddAbilityConditionEffector(new MagicConditionEffector(25));
            烈焰風暴.AddAbilityConditionEffector(new MagicConditionEffector(5));
            烈焰風暴.AddSkillEffector(new MagicalAttackSkillEffector(50));
            factory.AddSkill(烈焰風暴);

            Skill 火焰旋風 = new Skill(21, "火焰旋風", 100, true);
            火焰旋風.AddAbilityConditionEffector(new LevelConditionEffector(13));
            火焰旋風.AddAbilityConditionEffector(new PowerConditionEffector(30));
            火焰旋風.AddAbilityConditionEffector(new AgileConditionEffector(10));
            火焰旋風.AddSkillEffector(new PhysicalAttackSkillEffector(80));
            factory.AddSkill(火焰旋風);

            Skill 龍火噬盡 = new Skill(22, "龍火噬盡", 90, false);
            龍火噬盡.AddAbilityConditionEffector(new LevelConditionEffector(14));
            龍火噬盡.AddAbilityConditionEffector(new MagicConditionEffector(30));
            龍火噬盡.AddAbilityConditionEffector(new AgileConditionEffector(10));
            龍火噬盡.AddSkillEffector(new MagicalAttackSkillEffector(190));
            factory.AddSkill(龍火噬盡);

            Skill 能量溢出 = new Skill(23, "能量溢出", 200, false);
            能量溢出.AddAbilityConditionEffector(new LevelConditionEffector(19));
            能量溢出.AddAbilityConditionEffector(new PowerConditionEffector(35));
            能量溢出.AddAbilityConditionEffector(new AgileConditionEffector(20));
            能量溢出.AddSkillEffector(new CasterPhysicalAttackPointSkillEffector(5, 50));
            能量溢出.AddSkillEffector(new CasterSP_SkillEffector(100));
            能量溢出.AddSkillEffector(new CasterSpeedPointSkillEffector(5, 50));
            factory.AddSkill(能量溢出);

            Skill 能量爆發 = new Skill(24, "能量爆發", 300, true);
            能量爆發.AddAbilityConditionEffector(new LevelConditionEffector(17));
            能量爆發.AddAbilityConditionEffector(new MagicConditionEffector(40));
            能量爆發.AddAbilityConditionEffector(new AgileConditionEffector(12));
            能量爆發.AddSkillEffector(new MagicalAttackSkillEffector(250));
            factory.AddSkill(能量爆發);

            Skill 劍壓 = new Skill(25, "劍壓", 5, false);
            劍壓.AddAbilityConditionEffector(new LevelConditionEffector(2));
            劍壓.AddAbilityConditionEffector(new PowerConditionEffector(2));
            劍壓.AddAbilityConditionEffector(new AgileConditionEffector(5));
            劍壓.AddSkillEffector(new PhysicalAttackSkillEffector(15));
            factory.AddSkill(劍壓);

            Skill 風刃 = new Skill(26, "風刃", 5, false);
            風刃.AddAbilityConditionEffector(new LevelConditionEffector(2));
            風刃.AddAbilityConditionEffector(new MagicConditionEffector(6));
            風刃.AddSkillEffector(new MagicalAttackSkillEffector(15));
            factory.AddSkill(風刃);

            Skill 急速連切 = new Skill(27, "急速連切", 25, false);
            急速連切.AddAbilityConditionEffector(new LevelConditionEffector(7));
            急速連切.AddAbilityConditionEffector(new PowerConditionEffector(6));
            急速連切.AddAbilityConditionEffector(new AgileConditionEffector(15));
            急速連切.AddSkillEffector(new PhysicalAttackSkillEffector(45));
            factory.AddSkill(急速連切);

            Skill 樹根糾纏 = new Skill(28, "樹根糾纏", 30, false);
            樹根糾纏.AddAbilityConditionEffector(new LevelConditionEffector(6));
            樹根糾纏.AddAbilityConditionEffector(new MagicConditionEffector(12));
            樹根糾纏.AddAbilityConditionEffector(new SensibilityConditionEffector(15));
            樹根糾纏.AddSkillEffector(new MagicalAttackSkillEffector(20));
            樹根糾纏.AddSkillEffector(new TargetStopActionSkillEffector(1));
            factory.AddSkill(樹根糾纏);

            Skill 加速 = new Skill(29, "加速", 50, false);
            加速.AddAbilityConditionEffector(new LevelConditionEffector(12));
            加速.AddAbilityConditionEffector(new AgileConditionEffector(20));
            加速.AddSkillEffector(new CasterSpeedPointSkillEffector(3, 50));
            factory.AddSkill(加速);

            Skill 暴風 = new Skill(30, "暴風", 120, true);
            暴風.AddAbilityConditionEffector(new LevelConditionEffector(13));
            暴風.AddAbilityConditionEffector(new MagicConditionEffector(24));
            暴風.AddSkillEffector(new MagicalAttackSkillEffector(100));
            factory.AddSkill(暴風);

            Skill 自動導向 = new Skill(31, "自動導向", 50, false);
            自動導向.AddAbilityConditionEffector(new LevelConditionEffector(18));
            自動導向.AddAbilityConditionEffector(new AgileConditionEffector(35));
            自動導向.AddSkillEffector(new CasterAccuracyPointSkillEffector(5, 1000));
            factory.AddSkill(自動導向);

            Skill 時間停頓 = new Skill(32, "時間停頓", 150, false);
            時間停頓.AddAbilityConditionEffector(new LevelConditionEffector(19));
            時間停頓.AddAbilityConditionEffector(new AgileConditionEffector(20));
            時間停頓.AddAbilityConditionEffector(new SensibilityConditionEffector(20));
            時間停頓.AddSkillEffector(new CasterEvasionPointSkillEffector(3, 1000));
            factory.AddSkill(時間停頓);

            Skill 劈斬 = new Skill(33, "劈斬", 2, false);
            劈斬.AddAbilityConditionEffector(new LevelConditionEffector(1));
            劈斬.AddAbilityConditionEffector(new PowerConditionEffector(3));
            劈斬.AddSkillEffector(new PhysicalAttackSkillEffector(8));
            factory.AddSkill(劈斬);

            Skill 魔力衝擊 = new Skill(34, "魔力衝擊", 2, false);
            魔力衝擊.AddAbilityConditionEffector(new LevelConditionEffector(1));
            魔力衝擊.AddAbilityConditionEffector(new MagicConditionEffector(3));
            魔力衝擊.AddSkillEffector(new MagicalAttackSkillEffector(8));
            factory.AddSkill(魔力衝擊);

            Skill 金屬切割 = new Skill(35, "金屬切割", 16, false);
            金屬切割.AddAbilityConditionEffector(new LevelConditionEffector(6));
            金屬切割.AddAbilityConditionEffector(new PowerConditionEffector(8));
            金屬切割.AddAbilityConditionEffector(new SensibilityConditionEffector(8));
            金屬切割.AddSkillEffector(new PhysicalAttackSkillEffector(35));
            factory.AddSkill(金屬切割);

            Skill 魔力消除 = new Skill(36, "魔力消除", 50, true);
            魔力消除.AddAbilityConditionEffector(new LevelConditionEffector(8));
            魔力消除.AddAbilityConditionEffector(new MagicConditionEffector(10));
            魔力消除.AddAbilityConditionEffector(new SensibilityConditionEffector(12));
            魔力消除.AddSkillEffector(new MagicalSP_AttackSkillEffector(50));
            factory.AddSkill(魔力消除);

            Skill 乙太神劍 = new Skill(37, "乙太神劍", 100, false);
            乙太神劍.AddAbilityConditionEffector(new LevelConditionEffector(14));
            乙太神劍.AddAbilityConditionEffector(new PowerConditionEffector(15));
            乙太神劍.AddAbilityConditionEffector(new SensibilityConditionEffector(20));
            乙太神劍.AddSkillEffector(new PhysicalAttackSkillEffector(220));
            factory.AddSkill(乙太神劍);

            Skill 零化衝擊 = new Skill(38, "零化衝擊", 30, false);
            零化衝擊.AddAbilityConditionEffector(new LevelConditionEffector(12));
            零化衝擊.AddAbilityConditionEffector(new MagicConditionEffector(12));
            零化衝擊.AddAbilityConditionEffector(new SensibilityConditionEffector(14));
            零化衝擊.AddSkillEffector(new MagicalAttackSkillEffector(70));
            factory.AddSkill(零化衝擊);

            Skill 化境神劍 = new Skill(39, "化境神劍", 500, false);
            化境神劍.AddAbilityConditionEffector(new LevelConditionEffector(20));
            化境神劍.AddAbilityConditionEffector(new PowerConditionEffector(18));
            化境神劍.AddAbilityConditionEffector(new SensibilityConditionEffector(40));
            化境神劍.AddSkillEffector(new PhysicalAttackSkillEffector(1000));
            factory.AddSkill(化境神劍);

            Skill 虛無閃光 = new Skill(40, "虛無閃光", 300, true);
            虛無閃光.AddAbilityConditionEffector(new LevelConditionEffector(20));
            虛無閃光.AddAbilityConditionEffector(new MagicConditionEffector(18));
            虛無閃光.AddAbilityConditionEffector(new SensibilityConditionEffector(40));
            虛無閃光.AddSkillEffector(new MagicalAttackSkillEffector(500));
            factory.AddSkill(虛無閃光);

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
                HP = 10,
                MaxSP = 0,
                SP = 0,
                Power = 3,
                Magic = 1,
                Agile = 1,
                Sensibility = 1
            }, 5, new List<int>(), new Dictionary<int, int>()));

            factory.AddMonster(new Monster(2, "小鳥", new AbilityFactors
            {
                Level = 2,
                MaxHP = 20,
                HP = 20,
                MaxSP = 5,
                SP = 5,
                Power = 3,
                Magic = 2,
                Agile = 2,
                Sensibility = 2
            }, 15, new List<int> { 33 }, new Dictionary<int, int> { { 33, 20 } }));

            factory.AddMonster(new Monster(3, "小妖精", new AbilityFactors
            {
                Level = 4,
                MaxHP = 35,
                HP = 35,
                MaxSP = 35,
                SP = 35,
                Power = 6,
                Magic = 3,
                Agile = 3,
                Sensibility = 3
            }, 30, new List<int> { 34 }, new Dictionary<int, int> { { 34, 40 } }));

            factory.AddMonster(new Monster(4, "地精", new AbilityFactors
            {
                Level = 6,
                MaxHP = 50,
                HP = 50,
                MaxSP = 50,
                SP = 50,
                Power = 4,
                Magic = 4,
                Agile = 5,
                Sensibility = 8
            }, 50, new List<int> { 28 }, new Dictionary<int, int> { { 28, 25 } }));

            factory.AddMonster(new Monster(5, "土蜘蛛(雷思瑞)", new AbilityFactors
            {
                Level = 8,
                MaxHP = 80,
                HP = 80,
                MaxSP = 60,
                SP = 60,
                Power = 12,
                Magic = 2,
                Agile = 7,
                Sensibility = 6
            }, 100, new List<int> { 1, 4, 3 }, new Dictionary<int, int> { { 1, 70 }, { 4, 40 }, { 3, 15 } }));

            factory.AddMonster(new Monster(6, "水鈴蛇(雷思瑞)", new AbilityFactors
            {
                Level = 12,
                MaxHP = 120,
                HP = 120,
                MaxSP = 200,
                SP = 200,
                Power = 3,
                Magic = 15,
                Agile = 12,
                Sensibility = 9
            }, 180, new List<int> { 10, 9, 13, 14 }, new Dictionary<int, int> { { 10, 100 }, { 9, 60 }, { 13, 30 }, { 14, 15 } }));

            factory.AddMonster(new Monster(7, "火蜥蜴(雷思瑞)", new AbilityFactors
            {
                Level = 15,
                MaxHP = 200,
                HP = 200,
                MaxSP = 100,
                SP = 100,
                Power = 18,
                Magic = 2,
                Agile = 14,
                Sensibility = 11
            }, 250, new List<int> { 17, 19 }, new Dictionary<int, int> { { 17, 50 }, { 19, 20 } }));

            factory.AddMonster(new Monster(8, "樹精(雷思瑞)", new AbilityFactors
            {
                Level = 18,
                MaxHP = 300,
                HP = 300,
                MaxSP = 350,
                SP = 350,
                Power = 20,
                Magic = 20,
                Agile = 5,
                Sensibility = 12
            }, 400, new List<int> { 28, 26 }, new Dictionary<int, int> { { 28, 50 }, { 26, 20 } }));

            factory.AddMonster(new Monster(9, "獨角獸(雷思瑞)", new AbilityFactors
            {
                Level = 25,
                MaxHP = 500,
                HP = 500,
                MaxSP = 600,
                SP = 600,
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

            factory.AddItem(new Item(1, "圖書館的識別證"));
            factory.AddItem(new Item(2, "上週的記事"));
            factory.AddItem(new Item(3, "艾希恩龍典"));
            factory.AddItem(new Item(4, "特殊的紋章"));
            factory.AddItem(new Item(5, "軍備金兌換卡"));
            factory.AddItem(new Item(6, "金幣"));
            factory.AddItem(new Item(7, "銀幣"));
            factory.AddItem(new Item(8, "銅幣"));

            Consumable 蘋果 = new Consumable(9, "蘋果", 1);
            蘋果.AddEffector(new HPConsumableEffector(25));
            蘋果.AddEffector(new SPConsumableEffector(5));
            factory.AddItem(蘋果);
            Consumable 香蕉 = new Consumable(10, "香蕉", 2);
            香蕉.AddEffector(new HPConsumableEffector(40));
            factory.AddItem(香蕉);
            Consumable 青茶 = new Consumable(11, "青茶", 3);
            青茶.AddEffector(new SPConsumableEffector(50));
            factory.AddItem(青茶);
            Consumable 咖啡 = new Consumable(12, "咖啡", 4);
            咖啡.AddEffector(new SPConsumableEffector(80));
            factory.AddItem(咖啡);
            Consumable 特調提神飲料 = new Consumable(13, "特調提神飲料", 5);
            特調提神飲料.AddEffector(new SPConsumableEffector(120));
            factory.AddItem(特調提神飲料);
            Consumable 鮮紅藥水 = new Consumable(14, "鮮紅藥水", 6);
            鮮紅藥水.AddEffector(new HPConsumableEffector(150));
            factory.AddItem(鮮紅藥水);
            Consumable 大鮮紅藥水 = new Consumable(15, "大鮮紅藥水", 7);
            大鮮紅藥水.AddEffector(new HPConsumableEffector(250));
            factory.AddItem(大鮮紅藥水);
            Consumable 濃縮鮮紅藥水 = new Consumable(16, "濃縮鮮紅藥水", 8);
            濃縮鮮紅藥水.AddEffector(new HPConsumableEffector(400));
            factory.AddItem(濃縮鮮紅藥水);
            Consumable 小愛之泉 = new Consumable(17, "小愛之泉", 9);
            小愛之泉.AddEffector(new HPConsumableEffector(1000));
            factory.AddItem(小愛之泉);
            Consumable 湛藍藥水 = new Consumable(18, "湛藍藥水", 10);
            湛藍藥水.AddEffector(new SPConsumableEffector(60));
            factory.AddItem(湛藍藥水);
            Consumable 大湛藍藥水 = new Consumable(19, "大湛藍藥水", 11);
            大湛藍藥水.AddEffector(new SPConsumableEffector(100));
            factory.AddItem(大湛藍藥水);
            Consumable 濃縮湛藍藥水 = new Consumable(20, "濃縮湛藍藥水", 12);
            濃縮湛藍藥水.AddEffector(new SPConsumableEffector(150));
            factory.AddItem(濃縮湛藍藥水);
            Consumable 小夢之泉 = new Consumable(21, "小夢之泉", 6);
            小夢之泉.AddEffector(new SPConsumableEffector(300));
            factory.AddItem(小夢之泉);

            Equipment 銅劍 = new Equipment(22, "銅劍", 1, EquipmentType.Weapon);
            銅劍.AddEquipmentEffector(new PhysicalAttackPointEffector(5));
            銅劍.AddAbilityConditionEffector(new PowerConditionEffector(4));
            factory.AddItem(銅劍);
            Equipment 鐵劍 = new Equipment(23, "鐵劍", 2, EquipmentType.Weapon);
            鐵劍.AddEquipmentEffector(new PhysicalAttackPointEffector(10));
            鐵劍.AddAbilityConditionEffector(new PowerConditionEffector(10));
            factory.AddItem(鐵劍);
            Equipment 鋼劍 = new Equipment(24, "鋼劍", 3, EquipmentType.Weapon);
            鋼劍.AddEquipmentEffector(new PhysicalAttackPointEffector(15));
            鋼劍.AddAbilityConditionEffector(new PowerConditionEffector(20));
            factory.AddItem(鋼劍);
            Equipment 小妖精魔杖 = new Equipment(25, "小妖精魔杖", 4, EquipmentType.Weapon);
            小妖精魔杖.AddEquipmentEffector(new MagicalAttackPointEffector(5));
            小妖精魔杖.AddAbilityConditionEffector(new MagicConditionEffector(4));
            factory.AddItem(小妖精魔杖);
            Equipment 地精魔杖 = new Equipment(26, "地精魔杖", 5, EquipmentType.Weapon);
            地精魔杖.AddEquipmentEffector(new MagicalAttackPointEffector(10));
            地精魔杖.AddAbilityConditionEffector(new MagicConditionEffector(10));
            factory.AddItem(地精魔杖);
            Equipment 黑夜魔杖 = new Equipment(27, "黑夜魔杖", 6, EquipmentType.Weapon);
            黑夜魔杖.AddEquipmentEffector(new MagicalAttackPointEffector(15));
            黑夜魔杖.AddAbilityConditionEffector(new MagicConditionEffector(20));
            factory.AddItem(黑夜魔杖);

            Equipment 皮盔 = new Equipment(28, "皮盔", 7, EquipmentType.Head);
            皮盔.AddEquipmentEffector(new PhysicalDefencePointEffector(3));
            皮盔.AddEquipmentEffector(new MagicalDefencePointEffector(3));
            皮盔.AddAbilityConditionEffector(new PowerConditionEffector(1));
            factory.AddItem(皮盔);
            Equipment 銅盔 = new Equipment(29, "銅盔", 8, EquipmentType.Head);
            銅盔.AddEquipmentEffector(new PhysicalDefencePointEffector(10));
            銅盔.AddEquipmentEffector(new MagicalDefencePointEffector(8));
            銅盔.AddEquipmentEffector(new SpeedPointEffector(-3));
            銅盔.AddAbilityConditionEffector(new PowerConditionEffector(4));
            factory.AddItem(銅盔);
            Equipment 鐵盔 = new Equipment(30, "鐵盔", 9, EquipmentType.Head);
            鐵盔.AddEquipmentEffector(new PhysicalDefencePointEffector(15));
            鐵盔.AddEquipmentEffector(new MagicalDefencePointEffector(10));
            鐵盔.AddEquipmentEffector(new SpeedPointEffector(-5));
            鐵盔.AddAbilityConditionEffector(new PowerConditionEffector(10));
            factory.AddItem(鐵盔);
            Equipment 布帽 = new Equipment(31, "布帽", 10, EquipmentType.Head);
            布帽.AddEquipmentEffector(new PhysicalDefencePointEffector(1));
            布帽.AddEquipmentEffector(new MagicalDefencePointEffector(3));
            factory.AddItem(布帽);
            Equipment 精製棉帽 = new Equipment(32, "精製棉帽", 11, EquipmentType.Head);
            精製棉帽.AddEquipmentEffector(new PhysicalDefencePointEffector(2));
            精製棉帽.AddEquipmentEffector(new MagicalDefencePointEffector(5));
            factory.AddItem(精製棉帽);
            Equipment 基礎法帽 = new Equipment(33, "基礎法帽", 12, EquipmentType.Head);
            基礎法帽.AddEquipmentEffector(new PhysicalDefencePointEffector(2));
            基礎法帽.AddEquipmentEffector(new MagicalDefencePointEffector(12));
            基礎法帽.AddEquipmentEffector(new MagicalAttackPointEffector(2));
            基礎法帽.AddAbilityConditionEffector(new MagicConditionEffector(8));
            factory.AddItem(基礎法帽);


            Equipment 皮甲 = new Equipment(34, "皮甲", 13, EquipmentType.Body);
            皮甲.AddEquipmentEffector(new PhysicalDefencePointEffector(5));
            皮甲.AddEquipmentEffector(new MagicalDefencePointEffector(3));
            皮甲.AddAbilityConditionEffector(new PowerConditionEffector(4));
            factory.AddItem(皮甲);
            Equipment 銅甲 = new Equipment(35, "銅甲", 14, EquipmentType.Body);
            銅甲.AddEquipmentEffector(new PhysicalDefencePointEffector(20));
            銅甲.AddEquipmentEffector(new MagicalDefencePointEffector(12));
            銅甲.AddEquipmentEffector(new SpeedPointEffector(-5));
            銅甲.AddAbilityConditionEffector(new PowerConditionEffector(15));
            factory.AddItem(銅甲);
            Equipment 鐵甲 = new Equipment(36, "鐵甲", 15, EquipmentType.Body);
            鐵甲.AddEquipmentEffector(new PhysicalDefencePointEffector(35));
            鐵甲.AddEquipmentEffector(new MagicalDefencePointEffector(20));
            鐵甲.AddEquipmentEffector(new SpeedPointEffector(-10));
            鐵甲.AddAbilityConditionEffector(new PowerConditionEffector(25));
            factory.AddItem(鐵甲);
            Equipment 布衣 = new Equipment(37, "布衣", 16, EquipmentType.Body);
            布衣.AddEquipmentEffector(new PhysicalDefencePointEffector(3));
            布衣.AddEquipmentEffector(new MagicalDefencePointEffector(3));
            factory.AddItem(布衣);
            Equipment 時裝 = new Equipment(38, "時裝", 17, EquipmentType.Body);
            時裝.AddEquipmentEffector(new PhysicalAttackPointEffector(3));
            時裝.AddAbilityConditionEffector(new PowerConditionEffector(5));
            factory.AddItem(時裝);
            Equipment 基礎法袍 = new Equipment(39, "基礎法袍", 18, EquipmentType.Body);
            基礎法袍.AddEquipmentEffector(new PhysicalDefencePointEffector(3));
            基礎法袍.AddEquipmentEffector(new MagicalDefencePointEffector(25));
            基礎法袍.AddEquipmentEffector(new MagicalAttackPointEffector(4));
            基礎法袍.AddAbilityConditionEffector(new MagicConditionEffector(20));
            factory.AddItem(基礎法袍);

            Equipment 皮製戰靴 = new Equipment(40, "皮製戰靴", 19, EquipmentType.Foot);
            皮製戰靴.AddEquipmentEffector(new PhysicalDefencePointEffector(3));
            皮製戰靴.AddEquipmentEffector(new MagicalDefencePointEffector(3));
            皮製戰靴.AddAbilityConditionEffector(new PowerConditionEffector(1));
            factory.AddItem(皮製戰靴);
            Equipment 銅製戰靴 = new Equipment(41, "銅製戰靴", 20, EquipmentType.Foot);
            銅製戰靴.AddEquipmentEffector(new PhysicalDefencePointEffector(8));
            銅製戰靴.AddEquipmentEffector(new MagicalDefencePointEffector(5));
            銅製戰靴.AddEquipmentEffector(new SpeedPointEffector(-2));
            銅製戰靴.AddAbilityConditionEffector(new PowerConditionEffector(8));
            factory.AddItem(銅製戰靴);
            Equipment 鐵製戰靴 = new Equipment(42, "鐵製戰靴", 21, EquipmentType.Foot);
            鐵製戰靴.AddEquipmentEffector(new PhysicalDefencePointEffector(18));
            鐵製戰靴.AddEquipmentEffector(new MagicalDefencePointEffector(10));
            鐵製戰靴.AddEquipmentEffector(new SpeedPointEffector(-6));
            鐵製戰靴.AddAbilityConditionEffector(new PowerConditionEffector(18));
            factory.AddItem(鐵製戰靴);

            Equipment 草鞋 = new Equipment(43, "草鞋", 22, EquipmentType.Foot);
            草鞋.AddEquipmentEffector(new PhysicalDefencePointEffector(1));
            草鞋.AddEquipmentEffector(new MagicalDefencePointEffector(2));
            草鞋.AddEquipmentEffector(new SpeedPointEffector(2));
            factory.AddItem(草鞋);

            Equipment 布鞋 = new Equipment(44, "布鞋", 23, EquipmentType.Foot);
            草鞋.AddEquipmentEffector(new PhysicalDefencePointEffector(3));
            草鞋.AddEquipmentEffector(new MagicalDefencePointEffector(3));
            草鞋.AddEquipmentEffector(new SpeedPointEffector(5));
            factory.AddItem(布鞋);

            Equipment 基礎法鞋 = new Equipment(45, "基礎法鞋", 24, EquipmentType.Foot);
            基礎法鞋.AddEquipmentEffector(new PhysicalDefencePointEffector(3));
            基礎法鞋.AddEquipmentEffector(new MagicalDefencePointEffector(8));
            基礎法鞋.AddEquipmentEffector(new MagicalAttackPointEffector(2));
            基礎法鞋.AddAbilityConditionEffector(new MagicConditionEffector(10));
            factory.AddItem(基礎法鞋);

            factory.AddItem(new Equipment(46, "艾希恩的五芒星", 25, EquipmentType.Accessory));

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
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 1 }, new TradeItemInformation { itemID = 8, count = 50 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { itemID = 12, count = 1 } })
            );
            茶店.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 5 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { itemID = 13, count = 1 } })
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
                rewards: new List<TradeItemInformation> { new TradeItemInformation { itemID = 14, count = 1 } })
            );
            藥水店.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 6 }, new TradeItemInformation { itemID = 8, count = 50 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { itemID = 15, count = 1 } })
            );
            藥水店.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 12 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { itemID = 16, count = 1 } })
            );
            藥水店.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 24 }, new TradeItemInformation { itemID = 8, count = 50 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { itemID = 17, count = 1 } })
            );
            藥水店.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 5 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { itemID = 18, count = 1 } })
            );
            藥水店.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 10 }, new TradeItemInformation { itemID = 8, count = 50 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { itemID = 19, count = 1 } })
            );
            藥水店.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 20 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { itemID = 20, count = 1 } })
            );
            藥水店.AddTradeInformation(new TradeInformation(
                costs: new List<TradeItemInformation> { new TradeItemInformation { itemID = 7, count = 40 }, new TradeItemInformation { itemID = 8, count = 50 } },
                rewards: new List<TradeItemInformation> { new TradeItemInformation { itemID = 21, count = 1 } })
            );
            factory.AddStore(藥水店);

            StoreFactory.SaveFactory("StoreFactory", factory);
            Console.WriteLine("Store設置完成!");
        }
        private void SetupScene()
        {
            World world = new World("World");

            Scene 家裡 = new Scene(1, "家裡");
            家裡.AddAccessibleSceneID(2);
            world.AddScene(家裡);
            Scene 歐里 = new Scene(2, "歐里");
            歐里.AddAccessibleSceneID(1);
            歐里.AddAccessibleSceneID(3);
            歐里.AddAccessibleSceneID(4);
            歐里.AddAccessibleSceneID(8);
            歐里.AddAccessibleSceneID(10);
            歐里.AddAccessibleSceneID(11);
            歐里.AddNPC_ID(10);
            歐里.AddNPC_ID(11);
            world.AddScene(歐里);
            Scene 圖書館 = new Scene(3, "圖書館");
            圖書館.AddAccessibleSceneID(2);
            圖書館.AddNPC_ID(1);
            圖書館.AddNPC_ID(7);
            world.AddScene(圖書館);
            Scene 市集 = new Scene(4, "市集");
            市集.AddAccessibleSceneID(2);
            市集.AddAccessibleSceneID(15);
            市集.AddAccessibleSceneID(16);
            市集.AddAccessibleSceneID(17);
            市集.AddNPC_ID(4);
            市集.AddNPC_ID(5);
            市集.AddNPC_ID(12);
            world.AddScene(市集);
            Scene 看向北方的懸崖 = new Scene(5, "看向北方的懸崖");
            看向北方的懸崖.AddAccessibleSceneID(13);
            world.AddScene(看向北方的懸崖);
            Scene 農田 = new Scene(6, "農田");
            農田.AddAccessibleSceneID(7);
            農田.AddAccessibleSceneID(8);
            農田.AddAccessibleSceneID(13);
            農田.AddNPC_ID(13);
            world.AddScene(農田);
            Scene 城牆邊 = new Scene(7, "城牆邊");
            城牆邊.AddAccessibleSceneID(6);
            城牆邊.AddAccessibleSceneID(8);
            world.AddScene(城牆邊);
            Scene 城門口 = new Scene(8, "城門口");
            城門口.AddAccessibleSceneID(2);
            城門口.AddAccessibleSceneID(6);
            城門口.AddAccessibleSceneID(7);
            城門口.AddNPC_ID(14);
            world.AddScene(城門口);
            Scene 馬車上 = new Scene(9, "馬車上");
            world.AddScene(馬車上);
            Scene 茶館 = new Scene(10, "茶館");
            茶館.AddAccessibleSceneID(2);
            茶館.AddNPC_ID(2);
            茶館.AddNPC_ID(15);
            world.AddScene(茶館);
            Scene 教會 = new Scene(11, "教會");
            教會.AddAccessibleSceneID(2);
            教會.AddAccessibleSceneID(12);
            教會.AddNPC_ID(3);
            world.AddScene(教會);
            Scene 墓園 = new Scene(12, "墓園");
            墓園.AddAccessibleSceneID(11);
            墓園.AddNPC_ID(6);
            world.AddScene(墓園);
            Scene 歐里森林邊緣 = new Scene(13, "歐里森林邊緣");
            歐里森林邊緣.AddAccessibleSceneID(5);
            歐里森林邊緣.AddAccessibleSceneID(6);
            歐里森林邊緣.AddAccessibleSceneID(14);
            歐里森林邊緣.SetMonsterZone(new MonsterZone(new List<List<int>>
            {
                new List<int> { 1 },
                new List<int> { 1, 1 },
                new List<int> { 2 }
            }, new List<int> { 30, 15, 5 }));
            world.AddScene(歐里森林邊緣);
            Scene 歐里森林 = new Scene(14, "歐里森林");
            歐里森林.AddAccessibleSceneID(13);
            歐里森林.AddAccessibleSceneID(18);
            歐里森林.SetMonsterZone(new MonsterZone(new List<List<int>>
            {
                new List<int> { 1, 1 ,1 },
                new List<int> { 2, 2 },
                new List<int> { 3 }
            }, new List<int> { 45, 20, 5 }));
            world.AddScene(歐里森林);
            Scene 商人會館 = new Scene(15, "商人會館");
            商人會館.AddAccessibleSceneID(4);
            商人會館.AddNPC_ID(16);
            world.AddScene(商人會館);
            Scene 裝備行 = new Scene(16, "裝備行");
            裝備行.AddAccessibleSceneID(4);
            裝備行.AddNPC_ID(17);
            world.AddScene(裝備行);
            Scene 藥水店 = new Scene(17, "藥水店");
            藥水店.AddNPC_ID(18);
            藥水店.AddAccessibleSceneID(4);
            world.AddScene(藥水店);
            Scene 森林深處 = new Scene(18, "森林深處");
            森林深處.AddAccessibleSceneID(14);
            森林深處.AddAccessibleSceneID(19);
            森林深處.SetMonsterZone(new MonsterZone(new List<List<int>>
            {
                new List<int> { 3, 3 },
                new List<int> { 3, 3, 3 },
                new List<int> { 4, 3 },
                new List<int> { 4, 4 },
                new List<int> { 4, 4, 4 }
            }, new List<int> { 65, 30, 20, 10, 3 }));
            world.AddScene(森林深處);
            Scene 起點之湖 = new Scene(19, "起點之湖");
            起點之湖.AddAccessibleSceneID(18);
            起點之湖.AddAccessibleSceneID(20);
            起點之湖.SetMonsterZone(new MonsterZone(new List<List<int>>
            {
                new List<int> { 5 },
                new List<int> { 5, 5 },
                new List<int> { 5, 5, 6 },
                new List<int> { 6, 6 },
                new List<int> { 7 },
                new List<int> { 7, 7 },
                new List<int> { 5, 5, 5, 6, 6, 7 }
            }, new List<int> { 80, 55, 35, 20, 10, 3, 1 }));
            world.AddScene(起點之湖);
            Scene 黑暗之地 = new Scene(20, "黑暗之地");
            黑暗之地.AddAccessibleSceneID(19);
            黑暗之地.AddAccessibleSceneID(21);
            黑暗之地.SetMonsterZone(new MonsterZone(new List<List<int>>
            {
                new List<int> { 7 },
                new List<int> { 7, 7 },
                new List<int> { 8 },
                new List<int> { 8, 8 },
                new List<int> { 7, 7, 8 },
                new List<int> { 7, 7, 7, 8, 8 }
            }, new List<int> { 50, 40, 30, 20, 5, 1 }));
            world.AddScene(黑暗之地);
            Scene 光明之地 = new Scene(21, "光明之地");
            光明之地.AddAccessibleSceneID(20);
            光明之地.SetMonsterZone(new MonsterZone(new List<List<int>>
            {
                new List<int> { 7, 7, 8 },
                new List<int> { 7, 7, 7, 8, 8 },
                new List<int> { 9 },
                new List<int> { 7, 7, 8, 9 },
                new List<int> { 9, 9 },
                new List<int> { 9, 9, 9 },
                new List<int> { 9, 9, 9, 9 },
                new List<int> { 9, 9, 9, 9, 9 }
            }, new List<int> { 50, 30, 25, 15, 10, 5, 3, 1 }));
            world.AddScene(光明之地);

            World.SaveWorld("World", world);
            Console.WriteLine("Scene設置完成!");
        }
        private void SetupStory()
        {
            Console.WriteLine("Story不設置!");
        }
    }
}
