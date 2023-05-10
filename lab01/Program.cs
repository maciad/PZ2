public class main
{
    public static void Main(string [] args)
    {
        // zad2.koniec();
        // zad3.znajdz();
        // zad4.liczby(args[0], int.Parse(args[1]), int.Parse(args[2]), int.Parse(args[3]), int.Parse(args[4]), bool.Parse(args[5]));
        // zad5.liczby();
    }
}

class zad2
{
    public static void koniec()
    {
        StreamWriter sw = new StreamWriter("NazwaPliku.txt", append:false);
        string s = Console.ReadLine();
        sw.WriteLine(s);
        String zmienna = s;
        while(true)
        {
            s = Console.ReadLine();
            sw.WriteLine(s);
            if (s.CompareTo(zmienna) > 0)
            {
                zmienna = s;
            }
            if (s == "koniec!")
            {
                break;
            }
        }
        sw.Close();
        
    }
}

class zad3
{
    public static void znajdz()
    {
        string filename = Console.ReadLine();
        string szukana = Console.ReadLine();
        StreamReader sr = new StreamReader(filename);
        int lineNumber = 1;
        while(!sr.EndOfStream)
        {
            string line = sr.ReadLine();
            int index = line.IndexOf(szukana);
            if (line.Contains(szukana))
            {
                Console.WriteLine("linijka: " + lineNumber + " pozycja: " + index);
            }
            lineNumber++;
        }
        sr.Close();
    }

}

class zad4
{

    public static void liczby(string filename, int n, int minVal, int maxVal, int seed, bool isDouble)
    {
        StreamWriter sw = new StreamWriter(filename, append:true);
        Random rnd = new Random(seed);
        for (int i = 0; i < n; i++)
        {
            if (isDouble)
            {
                sw.WriteLine((rnd.NextDouble() * (maxVal - minVal) + minVal));
            }
            else
            {
                sw.WriteLine(rnd.Next(minVal, maxVal));
            }
        }
        sw.Close();

    }

}

class zad5
{
    public static void liczby()
    {
        StreamReader sr = new StreamReader("random.txt");
        int lines = 0;
        int characters = 0;
        int maxVal = int.MinValue;
        int minVal = int.MaxValue;
        int sum = 0;

        while (!sr.EndOfStream)
        {
            string line = sr.ReadLine();
            lines++;
            characters += line.Length;
            int val = int.Parse(line);
            if (val > maxVal)
            {
                maxVal = val;
            }
            if (val < minVal)
            {
                minVal = val;
            }
            sum += val;
        }
        Console.WriteLine("Liczba linijek: " + lines);
        Console.WriteLine("Liczba znakow: " + characters);
        Console.WriteLine("Najwieksza wartosc: " + maxVal);
        Console.WriteLine("Najmniejsza wartosc: " + minVal);
        Console.WriteLine("Srednia: " + (double)sum / lines);
        sr.Close();
    }
}