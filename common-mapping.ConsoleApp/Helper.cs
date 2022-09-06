namespace common_mapping
{
    public class Helper
    {
        public static void PrintObjects(IEnumerable<object> objs, string message = "", ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            foreach (var item in objs)
                Console.WriteLine(item);
            Console.ResetColor();
        }

        public static Int32 GetId(string message = "")
        {
            Int32 id = 0;
            Console.Write(message);
            string temp = Console.ReadLine();
            if (!Int32.TryParse(temp, out id))
                throw new Exception($"ERROR! '{temp}' is not integer value!");
            return id;
        }

        public static ConsoleKey ReadAction(Dictionary<ConsoleKey, string> commands, string message = "\r\nChoose action: ", ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            var keyColor = new ConsoleColor[] { ConsoleColor.Red, ConsoleColor.DarkRed}.Contains(color) ? ConsoleColor.Gray : ConsoleColor.Red;
            Console.Write(message);
            while (true)
            {
                foreach (var command in commands)
                {
                    Console.ForegroundColor = keyColor;
                    Console.Write($"[{command.Key}]");
                    Console.ForegroundColor = color;
                    Console.Write($" {command.Value} ");
                }
                Console.Write("\r\n> ");
                var key = Console.ReadKey().Key;
                Console.CursorLeft = 0; Console.Write(" "); Console.CursorLeft = 0;
                if (commands.Select(x => x.Key).Contains(key))
                {
                    Console.WriteLine(commands.First(x => x.Key == key).Value);
                    return key;
                }
                Console.WriteLine($"Unkown command '{key}'");
            }            
        }

        public static string GetValue(string source)
        {
            Console.Write(source);
            var sourceValue = Console.ReadLine();
            return sourceValue;
        }
    }
}
