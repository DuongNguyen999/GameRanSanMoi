using System;
using System.Threading;

internal class Program
{
    const int width = 20;
    const int height = 20;
    static char[,] screen = new char[height, width];
    static int[] x = new int[50];
    static int[] y = new int[50];
    static int fruitX, fruitY;
    static int tail;
    static bool gameOver;
    static int score;
    static ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();

    static void Setup()
    {
        gameOver = false;
        Random rand = new Random();
        fruitX = rand.Next(1, width);
        fruitY = rand.Next(1, height);
        x[0] = width / 2;
        y[0] = height / 2;
        tail = 0;
        score = 0;
    }

    static void Draw()
    {
        Console.Clear();

        // Vẽ màn hình game
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (i == 0 || i == height - 1 || j == 0 || j == width - 1)
                {
                    screen[i, j] = '#';
                }
                else
                {
                    screen[i, j] = ' ';
                }
            }
        }

        screen[y[0], x[0]] = 'O';
        screen[fruitY, fruitX] = 'F';
        for (int k = 1; k < tail; k++)
        {
            screen[y[k], x[k]] = 'O';
        }

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Console.Write(screen[i, j]);
            }
            Console.WriteLine();
        }

        Console.WriteLine("Score: " + score);
        Console.WriteLine("Press X to quit the game.");
    }

    static void Input()
    {
        if (Console.KeyAvailable)
            keyInfo = Console.ReadKey();
    }

    static void Logic()
    {
        Thread.Sleep(500);

        if (keyInfo.Key == ConsoleKey.RightArrow)
            x[0]++;
        if (keyInfo.Key == ConsoleKey.LeftArrow)
            x[0]--;
        if (keyInfo.Key == ConsoleKey.UpArrow)
            y[0]--;
        if (keyInfo.Key == ConsoleKey.DownArrow)
            y[0]++;
        if (keyInfo.Key == ConsoleKey.X)
            gameOver = true;
            Console.WriteLine(" ");

        if (x[0] == fruitX && y[0] == fruitY)
        {
            Random rand = new Random();
            fruitX = rand.Next(1, width);
            fruitY = rand.Next(1, height);
            tail++;
            score++;
        }
        
        if (tail == 3)
        {
          Setup();
        }
        
        if (x[0] >= width || x[0] < 0 || y[0] >= height || y[0] < 0)
            gameOver = true;

        for (int i = 1; i < tail; i++)
        {
            if (x[i] == x[0] && y[i] == y[0])
                gameOver = true;
        }
    }

    static void Main(string[] args)
    {
        Setup();
        while (!gameOver)
        {
            Draw();
            Input();
            Logic();
        }
        Console.WriteLine("Game over!");
        Console.WriteLine("Your score is " + score);
    }
}
