using common_mapping.Models;

namespace common_mapping
{
    internal class Items
    {
        Mapping _map;
        Dictionary<ConsoleKey, string> commands;
        public Items(Mapping map)
        {
            _map = map;
            commands = new Dictionary<ConsoleKey, string>();
            commands.Add(ConsoleKey.D1, "Insert new item");
            commands.Add(ConsoleKey.D2, "Delete item");
            commands.Add(ConsoleKey.D3, "Get items by value");
            commands.Add(ConsoleKey.D4, "Get items");
            commands.Add(ConsoleKey.Escape, "Back");
        }

        public void Main()
        {
            while (true)
            {
                try
                {
                    switch (Helper.ReadAction(commands, "\r\nChoose action with items: ", ConsoleColor.White))
                    {
                        case ConsoleKey.Escape: return;
                        case ConsoleKey.D1: InsertValue(); break;
                        case ConsoleKey.D2: Delete(); break;
                        case ConsoleKey.D3: GetByValue(); break;
                        case ConsoleKey.D4: Get(); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                }
            }
        }

        private void Delete()
        {
            int linkId = GetLinkId();
            string sourceValue = GetValue("source");
            _map.Items.Delete(linkId, sourceValue);
        }

        private void InsertValue()
        {
            int linkId = GetLinkId();
            string sourceValue = GetValue("source");
            string targetValue = GetValue("target");
            _map.Items.Insert(new MapItem(linkId, sourceValue, targetValue));
        }

        private void GetByValue()
        {
            int linkId = GetLinkId();
            string sourceValue = GetValue("source");
            var items = _map.Items.Get(linkId, sourceValue);
            Helper.PrintObjects(items, $"Items by link '{linkId}' value '{sourceValue}'", ConsoleColor.Green);
        }

        private void Get()
        {
            var linkId = GetLinkId();
            var items =_map.Items.Get(linkId);
            Helper.PrintObjects(items, $"Items by link '{linkId}'", ConsoleColor.Yellow);
        }

        private static int GetLinkId()
        {
            Console.Write("Enter link id: ");
            var linkId = Helper.GetId();
            return linkId;
        }

        private static string GetValue(string source)
        {
            Console.Write($"Enter {source} value: ");
            var sourceValue = Console.ReadLine();
            return sourceValue;
        }

    }
}
