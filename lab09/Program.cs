//Przykład użycia ImageSharp do wczytania obrazu, operacji na poszczególnych pikselach oraz zapisania obrazu.

// using SixLabors.ImageSharp;
// using SixLabors.ImageSharp.Processing;

// using (Image<Rgb24> image = Image.Load<Rgb24>("lena.png")) 
// {
//     // klon obrazka - pracujemy teraz na kopii danych
//     Image<Rgb24>my_clone = image.Clone();
//     // pętla po wszystkich pikselach
//     for (int a = 0; a < image.Width; a++)
//         for (int b = 0; b < image.Height; b++)
//         {
//             // pobranie składników RGB 
//             byte R = image[a,b].R;
//             byte G = image[a,b].G;
//             byte B = image[a,b].B;
//             // zmiana RGB na BGR
//             my_clone[a,b] = new Rgb24(B, G, R);
//         }
//     //zapisanie obrazków
//     image.Save("test.png");
//     my_clone.Save("test2.png");           
// }

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

class Program
{
    static void Main(string[] args)
    {
        using (Image<Rgb24> image = Image.Load<Rgb24>("tree.jpg"))
        {
            GrayScale(image);
            MedianFilter(image, 3, 3);
        }
    }

    //1
    //Napisz metodę, która przekształci obraz wejściowy na obraz w odcieniach szarości. Aby dokonać konwersji obrazu na obraz w odcieniu szarości wykorzystaj "Average Method"
    public static void GrayScale(Image<Rgb24> image)
    {
        Image<Rgb24>my_clone = image.Clone();

        for (int a = 0; a < image.Width; a++)
            for (int b = 0; b < image.Height; b++)
            {
                byte R = image[a, b].R;
                byte G = image[a, b].G;
                byte B = image[a, b].B;
                byte gray = (byte)((R + G + B) / 3);
                my_clone[a, b] = new Rgb24(gray, gray, gray);
            }
        my_clone.Save("GrayScale.jpg");
    }

    //2 
    //Zaimplementuj filtr medianowy, parametry metody to obraz oraz rozmiar okna filtra medianowego (szerokość, wysokość). Szerokość i wysokość muszą być wartościami nieparzystymi.
    public static void MedianFilter(Image<Rgb24> image, int width, int height)
    {
        if (width % 2 == 0 || height % 2 == 0)
            {
                Console.WriteLine("Szerokość i wysokość muszą być wartościami nieparzystymi.");
                return;
            }

        Image<Rgb24>my_clone = image.Clone();

        for (int a = 0; a < image.Width - width; a++)
            for (int b = 0; b < image.Height - height; b++)
            {
                byte[] R = new byte[width * height];
                byte[] G = new byte[width * height];
                byte[] B = new byte[width * height];

                int i = 0;
                for (int x = a; x < a + width; x++)
                    for (int y = b; y < b + height; y++)
                    {
                        R[i] = image[x, y].R;
                        G[i] = image[x, y].G;
                        B[i] = image[x, y].B;
                        i++;
                    }
                Array.Sort(R);
                Array.Sort(G);
                Array.Sort(B);
                my_clone[a + width / 2, b + height / 2] = new Rgb24(R[width * height / 2], G[width * height / 2], B[width * height / 2]);
            }
        my_clone.Save("MedianFilter.jpg");
    }
}
