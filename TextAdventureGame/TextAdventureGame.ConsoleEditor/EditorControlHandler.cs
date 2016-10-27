using System;

namespace TextAdventureGame.ConsoleEditor
{
    public abstract class EditorControlHandler
    {
        protected EditorControlHandler editorControlHandler;

        public abstract string ControlInformation { get; }
        protected virtual bool HandleCommand(string command, out int rollbackLayerCount)
        {
            bool canHandle = true;
            rollbackLayerCount = 0;
            switch (command)
            {
                case "help":
                    HelpCommandTask();
                    break;
                case "clear":
                    ClearCommandTask();
                    break;
                default:
                    canHandle = false;
                    break;
            }
            return canHandle;
        }
        public void HandleInput(string inputCommand, out int rollbackLayerCount)
        {
            if(!HandleCommand(inputCommand, out rollbackLayerCount))
            {
                Console.WriteLine("無效的指令");
            }

            if (editorControlHandler != null)
            {
                int editorRollbackLayerCount = 0;
                Console.WriteLine(editorControlHandler.ControlInformation);
                while (editorRollbackLayerCount == 0)
                {
                    editorControlHandler.HandleInput(Console.ReadLine(), out editorRollbackLayerCount);
                }
                editorControlHandler = null;
                if (editorRollbackLayerCount - 1 > 0)
                {
                    rollbackLayerCount = editorRollbackLayerCount - 1;
                }
                else
                {
                    Console.WriteLine(ControlInformation);
                }
            }
        }
        protected virtual void HelpCommandTask()
        {
            Console.WriteLine("\t輸入clear清空畫面");
        }
        protected void ClearCommandTask()
        {
            Console.Clear();
            Console.WriteLine(ControlInformation);
        }
    }
}
