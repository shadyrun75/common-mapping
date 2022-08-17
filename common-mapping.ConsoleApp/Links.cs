using common_mapping.Models;

namespace common_mapping
{
    public class Links
    {
        Mapping _map;
        Dictionary<ConsoleKey, string> commands;
        public Links(Mapping map)
        {
            _map = map;
            commands = new Dictionary<ConsoleKey, string>();
            commands.Add(ConsoleKey.D1, "Get links");
            commands.Add(ConsoleKey.D2, "Insert new link");
            commands.Add(ConsoleKey.D3, "Update link");
            commands.Add(ConsoleKey.D4, "Delete link");
            commands.Add(ConsoleKey.Escape, "Back");
        }

        public void Main()
        {
            while (true)
            {
                try
                {
                    switch (Helper.ReadAction(commands, "\r\nChoose action with links: ", ConsoleColor.Blue))
                    {
                        case ConsoleKey.Escape: return;
                        case ConsoleKey.D1: Get(); break;
                        case ConsoleKey.D2: Insert(); break;
                        case ConsoleKey.D3: Update(); break;
                        case ConsoleKey.D4: Delete(); break;
                        default: Console.WriteLine("Unkown command"); break;
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

        public void Get()
        {
            var links = _map.Links.Get();
            Helper.PrintObjects(links, $"Links ({links.Count()})", ConsoleColor.Cyan);
        }

        public void Insert()
        {
            MapLink newLink = new MapLink() { };
            Console.Write("Enter Source id type: ");
            newLink.SourceId = Helper.GetId();
            Console.Write("Enter Target id type: ");
            newLink.TargetId = Helper.GetId();
            _map.Links.Insert(newLink);
        }

        public void Update()
        {
            Console.Write($"Enter id of link: ");
            var link = new MapLink();
            link.Id = Helper.GetId();
            Console.Write($"Enter source id: ");
            link.SourceId = Helper.GetId();
            Console.Write($"Enter target id: ");
            link.TargetId = Helper.GetId();
            _map.Links.Update(link);
        }

        public void Delete()
        {
            Console.Write($"Enter id of link: ");
            int id= Helper.GetId();
            _map.Links.Delete(id);
        }
    }
}
