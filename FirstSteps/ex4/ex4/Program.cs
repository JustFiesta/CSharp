using System;

class Program
{
    static void Main()
    {
        Console.Write("Insert height of tree: ");
        if (int.TryParse(Console.ReadLine(), out int height) && height > 0)
        {
            DrawTree(height);
        }
        else
        {
            Console.WriteLine("Wrong value! Please give an integer.");
        }
    }

    static void DrawTree(int height)
    {
        for (int i = 0; i < height; i++)
        {
            // insert spaces
            for (int j = 0; j < height - i - 1; j++)
            {
                Console.Write(" ");
            }

            // draw tree
            for (int k = 0; k < 2 * i + 1; k++)
            {
                Console.Write("*");
            }

            Console.WriteLine(); // go to new line after one is done
        }

        // draw tree trunk
        for (int i = 0; i < height - 1; i++)
        {
            Console.Write(" ");
        }
        Console.WriteLine("|");
    }
}
