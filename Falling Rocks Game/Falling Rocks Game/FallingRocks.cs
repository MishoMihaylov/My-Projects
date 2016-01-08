using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;


class FallingRocks
{

    public static void PlaceUnit(Unit unit)
    {
        Console.SetCursorPosition(unit.x, unit.y);
        Console.ForegroundColor = unit.colour;
        Console.Write(unit.symbol);
    }

    public static void CreateField()
    {
         for (int i = 0; i < Console.WindowHeight; i++)
            {
                Console.SetCursorPosition(0, Console.WindowHeight - i - 1);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write('|');
            }

            for (int i = 0; i < Console.WindowHeight; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth - 10, Console.WindowHeight - i - 1);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write('|');
            }

            for (int i = 0; i < Console.WindowWidth - 10; i++)
            {
                Console.SetCursorPosition(i, Console.WindowHeight - 1);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write('*');
            }

            for (int i = 0; i < Console.WindowWidth - 10; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write('*');
            }
    }

    static void Main(string[] args)
    {

        bool hitted = false;

        // Console settings
        Console.CursorVisible = false;
        Console.BufferHeight = Console.WindowHeight = 20;
        Console.BufferWidth = Console.WindowWidth = 45;
        Random randomGenerator = new Random();

        //Creating types of rocks, the rocks and colours
        char[] typesOfRocks = { '^', '@', '*', '&', '+', '%', '$', '#', '!', '.', ';', '-' };
        List<Unit> Rock = new List<Unit>();
        ConsoleColor[] typesOfColours =
            {
            ConsoleColor.Blue,
            ConsoleColor.Cyan,
            ConsoleColor.DarkGreen,
            ConsoleColor.Yellow,
            ConsoleColor.Red,
            ConsoleColor.White
            };

        //Creating and positioning the dwarf
        Unit Dwarf = new Unit((Console.WindowWidth - 10) / 2, Console.WindowHeight - 2, ConsoleColor.Green, '0');
        PlaceUnit(Dwarf);


        while (!hitted)
        {
            Console.Clear();

            //Creating the game field
            CreateField();

            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo isPressed = Console.ReadKey();
                if (isPressed.Key == ConsoleKey.LeftArrow && Dwarf.x >= 2)
                {
                    Dwarf.x--;

                }
                if (isPressed.Key == ConsoleKey.RightArrow && Dwarf.x < Console.WindowWidth - 11)
                {
                    Dwarf.x++;
                }
            }

            //Creating new Rock and placing it
            if (randomGenerator.Next(0, 101) > 80)
            {
                Unit newRock = new Unit(
                    randomGenerator.Next(2, Console.WindowWidth - 10),
                    0,
                    typesOfColours[randomGenerator.Next(0, typesOfColours.Count())],
                    typesOfRocks[randomGenerator.Next(0, typesOfRocks.Count())]
                );
                Rock.Add(newRock);
            }

            for (int i = 0; i < Rock.Count(); i++)
            {
                if (!Rock.Any())
                {
                    break;
                }
                else
                {
                    Rock[i].y++;
                }
            }

            //Printing the objects and checking for hit

            PlaceUnit(Dwarf);

            foreach (var item in Rock)
            {
                PlaceUnit(item);

                if (item.x == Dwarf.x && item.y == Dwarf.y)
                {
                    Console.Clear();
                    Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop);
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine("Ops Stooone! Take break my dear Dwarf.");
                    Rock.Clear();
                    hitted = true;
                    break;
                }
                else if (item.y == Dwarf.y && item.x != Dwarf.x)
                {

                    item.y = 1;
                }
            }
            Thread.Sleep(150);
        }

    }
}

