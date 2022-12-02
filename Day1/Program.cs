namespace AdventOfCode.Day1;

public static class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine($"Highest total calories: {PartA()}");

        int track = 3;
        Console.WriteLine($"Top {track} highest total calorie total: {PartB(track)}");
    }

    private static int PartA()
    {
        StreamReader sr = new(File.Open("inputs.txt", FileMode.Open, FileAccess.Read));

        int cCount = 0;
        int cLargest = 0;

        while (!sr.EndOfStream)
        {
            string line = sr.ReadLine();

            if (line == "")
            {
                cLargest = cCount > cLargest ? cCount : cLargest;
                cCount = 0;
            }
            else
                cCount += int.Parse(line);
        }

        sr.Close();

        return cLargest;
    }

    private static int PartB(int cTrackCount)
    {
        StreamReader sr = new(File.Open("inputs.txt", FileMode.Open, FileAccess.Read));
        int[] cLargest = new int[cTrackCount];
        int cCount = 0;

        while (!sr.EndOfStream)
        {
            string line = sr.ReadLine();

            if (line == "")
            {
                for (int i = 0; i < cTrackCount; i++)
                {
                    if (cCount > cLargest[i])
                    {
                        int temp = cLargest[i];
                        cLargest[i] = cCount;
                        cCount = temp;
                    }
                }
                cCount = 0;
            }
            else
                cCount += int.Parse(line);
        }

        sr.Close();

        int total = 0;

        foreach (int i in cLargest)
            total += i;

        return total;
    }
}