﻿using System;

namespace TextAdventureGame.ConsoleEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            EditorControlHandler editorControlHandler = null;
            while(editorControlHandler == null)
            {
                Console.Clear();
                Console.WriteLine("請輸入要進入的編輯器：");
                Console.WriteLine("\tstory :故事編輯器");
                Console.WriteLine("\tworld :世界編輯器");
                Console.WriteLine("\titem :物品編輯器");
                Console.WriteLine("\tnpc :NPC編輯器");
                Console.WriteLine("\tstore :商店編輯器");
                Console.WriteLine("\tauto :自動設置所有遊戲資料");
                Console.WriteLine("\texit :關閉編輯器");
                switch (Console.ReadLine())
                {
                    case "story":
                        editorControlHandler = new StoryEditor();
                        break;
                    case "world":
                        editorControlHandler = new WorldEditor();
                        break;
                    case "item":
                        editorControlHandler = new ItemEditor();
                        break;
                    case "npc":
                        editorControlHandler = new NPC_Editor();
                        break;
                    case "store":
                        editorControlHandler = new StoreEditor();
                        break;
                    case "auto":
                        editorControlHandler = new AutoEditor();
                        break;
                    case "exit":
                        return;
                    default:
                        break;
                }
                if(editorControlHandler != null)
                {
                    int rollbackLayerCount = 0;
                    Console.WriteLine(editorControlHandler.ControlInformation);
                    while(rollbackLayerCount == 0)
                    {
                        editorControlHandler.HandleInput(Console.ReadLine(), out rollbackLayerCount);
                    }
                    editorControlHandler = null;
                    if (rollbackLayerCount - 1 > 0)
                    {
                        break;
                    }
                }
            }
            
        }
    }
}
