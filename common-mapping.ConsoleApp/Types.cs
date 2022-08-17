using common_mapping.Models;

namespace common_mapping
{
    public class Types
    {
        Mapping _map;
        Dictionary<ConsoleKey, string> commands;
        public Types(Mapping map)
        {
            _map = map;
            commands = new Dictionary<ConsoleKey, string>();
            commands.Add(ConsoleKey.D1, "Get types");
            commands.Add(ConsoleKey.D2, "Insert new type");
            commands.Add(ConsoleKey.D3, "Update type");
            commands.Add(ConsoleKey.D4, "Delete type");
            commands.Add(ConsoleKey.Escape, "Back");
        }

        public void Main()
        {
            while (true)
            {
                try
                {
                    switch (Helper.ReadAction(commands, "\r\nChoose action with types: ", ConsoleColor.DarkGreen))
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
                    Console.WriteLine($"{ex.Message} | {ex.InnerException?.Message}");
                    Console.ResetColor();
                }
            }
        }

        public void Get()
        {
            var types = _map.Types.Get();
            Helper.PrintObjects(types, $"Types ({types.Count()})", ConsoleColor.Yellow);
        }

        public void Insert()
        {
            MapType newType = new MapType();
            Console.Write("Enter name of new mapping type: ");
            newType.Name = Console.ReadLine();
            Console.Write($"Enter description of {newType.Name} type: ");
            newType.Description = Console.ReadLine();
            _map.Types.Insert(newType);
        }

        public void Update()
        {
            Console.Write($"nEnter id of mapping type: ");
            MapType type = new MapType();
            type.Id = Helper.GetId();            
            Console.Write("\r\nEnter new name: ");
            type.Name = Console.ReadLine();
            Console.Write($"Enter description: ");
            type.Description = Console.ReadLine();
            _map.Types.Update(type);
        }

        public void Delete()
        {
            Console.Write($"Enter id of mapping type: ");
            int id = Helper.GetId();
            _map.Types.Delete(id);
        }
    }
}
