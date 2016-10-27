using System;

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
                Console.WriteLine("\texit :關閉編輯器");
                switch (Console.ReadLine())
                {
                    case "story":
                        editorControlHandler = new StoryEditor();
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
