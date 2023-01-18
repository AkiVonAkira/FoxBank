namespace FoxBank
{
    internal class Menu
    {
        private readonly string[] _menuItems;
        private int _selectedIndex;

        public Menu(string[] items)
        {
            _menuItems = items;
            _selectedIndex = 0;
        }

        public void PrintMenu()
        {
            Console.Clear();
            Console.WriteLine("Please select an option:");
            for (int i = 0; i < _menuItems.Length; i++)
            {
                Console.ForegroundColor = i == _selectedIndex ? ConsoleColor.Cyan : ConsoleColor.White;
                Console.WriteLine(i == _selectedIndex ? $"↪ {_menuItems[i]}" : $"  {_menuItems[i]}  ");
            }
            Console.ResetColor();
        }

        public int SelectIndex
        {
            get => _selectedIndex;
            set => _selectedIndex = (value % _menuItems.Length + _menuItems.Length) % _menuItems.Length;
        }

        public int UseMenu()
        {
            ConsoleKey userInput;
            do
            {
                userInput = Console.ReadKey(true).Key;
                switch (userInput)
                {
                    case ConsoleKey.UpArrow:
                        _selectedIndex--;
                        _selectedIndex = (_selectedIndex % _menuItems.Length + _menuItems.Length) % _menuItems.Length;
                        break;
                    case ConsoleKey.DownArrow:
                        _selectedIndex++;
                        _selectedIndex = (_selectedIndex % _menuItems.Length + _menuItems.Length) % _menuItems.Length;
                        break;
                    case ConsoleKey.Enter:
                    case ConsoleKey.Spacebar:
                        var index = _selectedIndex;
                        PrintMenu();
                        return index;
                }
                PrintMenu();
            } while (true);
        }
    }
}
