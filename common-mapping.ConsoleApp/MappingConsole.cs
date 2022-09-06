using common_mapping.Models;

namespace common_mapping
{
    internal class MappingConsole
    {
        Mapping _map;
        Dictionary<ConsoleKey, string> commands;
        public MappingConsole(Mapping map)
        {
            _map = map;
            commands = new Dictionary<ConsoleKey, string>();
            commands.Add(ConsoleKey.D1, "Insert new item");
            commands.Add(ConsoleKey.D2, "Delete item");
            commands.Add(ConsoleKey.D3, "Get items by value and link");
            commands.Add(ConsoleKey.D4, "Get items by link");
            commands.Add(ConsoleKey.D5, "Get items as like by link");
            commands.Add(ConsoleKey.D6, "Get links");
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
                        case ConsoleKey.D5: GetByLike(); break;
                        case ConsoleKey.D6: GetLinks(); break;
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

        private void GetLinks()
        {
            var links = _map.GetLinks();
            Helper.PrintObjects(links, "Links", ConsoleColor.Blue);
        }

        private void Delete()
        {
            var linkCode = Helper.GetValue("Enter link code: ");
            string sourceValue = Helper.GetValue("Enter source value: ");
            _map.Delete(linkCode, sourceValue);
        }

        private void InsertValue()
        {
            var linkCode = Helper.GetValue("Enter link code: ");
            string sourceValue = Helper.GetValue("Enter source value: ");
            string targetValue = Helper.GetValue("Enter target value: ");
            _map.Insert(new MapItem(linkCode, sourceValue, targetValue));
        }

        private void GetByValue()
        {
            var linkCode = Helper.GetValue("Enter link code: ");
            string sourceValue = Helper.GetValue("Enter source value: ");
            var items = _map.Get(linkCode, sourceValue);
            Helper.PrintObjects(items, $"Items by link '{linkCode}' value '{sourceValue}'", ConsoleColor.Green);
        }

        private void Get()
        {
            var linkCode = Helper.GetValue("Etner link code: ");
            var items =_map.Get(linkCode);
            Helper.PrintObjects(items, $"Items by link '{linkCode}'", ConsoleColor.Yellow);
        }

        private static int GetLinkId()
        {
            var linkId = Helper.GetId("Enter link id: ");
            return linkId;
        }        

        private void GetByLike()
        {
            var linkCode = Helper.GetValue("Enter link code: ");
            var searchText = Helper.GetValue("Enter searched value: ");
            var data = _map.GetByLike(linkCode, searchText);
            foreach (var item in data)
                Console.WriteLine($"Finded {item}");
        }

        
    }
}
