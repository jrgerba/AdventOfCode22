using System.Diagnostics;

namespace AdventOfCode.Day2;

public static class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine($"My total score (part 1): {PartA()}");
        Console.WriteLine($"My total score (part 2): {PartB()}");
    }

    public static int PartA()
    {
        StreamReader sr = new(File.Open("input.txt", FileMode.Open, FileAccess.Read));

        int score = 0;

        while (!sr.EndOfStream)
        {
            string line = sr.ReadLine();
            Shape opponent = CharToShape(line[0]);
            Shape myShape = CharToShape(line[2]);

            score += ShapeValue(myShape) + OutcomeValue(GetOutcome(myShape, opponent));
        }

        sr.Close();

        return score;
    }

    public static int PartB()
    {
        StreamReader sr = new(File.Open("input.txt", FileMode.Open, FileAccess.Read));

        int score = 0;

        while (!sr.EndOfStream)
        {
            string line = sr.ReadLine();
            Shape opponent = CharToShape(line[0]);
            Outcome desiredOutcome = CharToOutcome(line[2]);

            score += OutcomeValue(desiredOutcome) + ShapeValue(GetRequiredShapeForOutcome(desiredOutcome, opponent));
        }

        sr.Close();

        return score;
    }

    public static Shape CharToShape(char c) => c switch
    {
        'A' or 'X' => Shape.Rock,
        'B' or 'Y' => Shape.Paper,
        'C' or 'Z' => Shape.Scissors,
        _ => throw new UnreachableException()
    };

    public static Outcome CharToOutcome(char c) => c switch
    {
        'X' => Outcome.Loss,
        'Y' => Outcome.Draw,
        'Z' => Outcome.Win,
        _ => throw new UnreachableException()
    };

    public static int ShapeValue(Shape shape) => shape switch
    {
        Shape.Rock => 1,
        Shape.Paper => 2,
        Shape.Scissors => 3,
        _ => throw new UnreachableException()
    };

    public static int OutcomeValue(Outcome outcome) => outcome switch
    {
        Outcome.Win => 6,
        Outcome.Loss => 0,
        Outcome.Draw => 3,
        _ => throw new UnreachableException()
    };

    public static Outcome GetOutcome(Shape player, Shape opponent) => player switch
    {
        Shape.Rock => opponent switch
        {
            Shape.Rock => Outcome.Draw,
            Shape.Paper => Outcome.Loss,
            Shape.Scissors => Outcome.Win,
            _ => throw new UnreachableException()
        },
        Shape.Paper => opponent switch
        {
            Shape.Rock => Outcome.Win,
            Shape.Paper => Outcome.Draw,
            Shape.Scissors => Outcome.Loss,
            _ => throw new UnreachableException()
        },
        Shape.Scissors => opponent switch
        {
            Shape.Rock => Outcome.Loss,
            Shape.Paper => Outcome.Win,
            Shape.Scissors => Outcome.Draw,
            _ => throw new UnreachableException()
        },
        _ => throw new UnreachableException()
    };


    public static Shape GetRequiredShapeForOutcome(Outcome outcome, Shape opponent) => outcome switch
    {
        Outcome.Win => (Shape)((((int)opponent << 1) | ((int)opponent >> 2)) & 0b111),
        Outcome.Draw => opponent,
        Outcome.Loss => (Shape)((((int)opponent >> 1) | ((int)opponent << 2)) & 0b111),
        _ => throw new UnreachableException()
    };
}