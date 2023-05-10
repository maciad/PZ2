using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        string imageName = "tree.jpg";

        int[,] kernel1 = {{0, -1, 0},
                          {-1, 4, -1},
                          {0, -1, 0}};
        int[,] kernel2 = {{-1, -1, -1},
                          {-1, 8, -1},
                          {-1, -1, -1}};
        int[,] kernel3 = {{-2, -1, 0},
                          {-1, 1, 1},
                          {0, 1, 2}};
        int[,] kernel4 = {{0, -1, 0},
                          {-1, 5, -1},
                          {0, -1, 0}};

        using (Image<Rgb24> image = Image.Load<Rgb24>(imageName))
        {
            GrayScale(image);
            MedianFilter(image, 5, 5);
            MaxFilter(image, 5, 5);
            MinFilter(image, 5, 5);
            MeanFilter(image, 5, 5);
            ConvolutionFilter(image, kernel1);
        }
    }

    //1
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
    public static void MedianFilter(Image<Rgb24> image, int width, int height)
    {
        if (width % 2 == 0 || height % 2 == 0)
        {
            Console.WriteLine("Szerokość i wysokość muszą być wartościami nieparzystymi.");
            return;
        }

        Image<Rgb24>my_clone = image.Clone();
        byte[] R = new byte[width * height];
        byte[] G = new byte[width * height];
        byte[] B = new byte[width * height];

        for (int a = width / 2; a < image.Width - width / 2; a++)
            for (int b = height / 2; b < image.Height - height / 2; b++)
            {
                int i = 0;
                for (int x = a - width / 2; x <= a + width / 2; x++)
                    for (int y = b - height / 2; y <= b + height / 2; y++)
                    {
                        R[i] = image[x, y].R;
                        G[i] = image[x, y].G;
                        B[i] = image[x, y].B;
                        i++;
                    }
                Array.Sort(R);
                Array.Sort(G);
                Array.Sort(B);
                my_clone[a, b] = new Rgb24(R[width * height / 2], G[width * height / 2], B[width * height / 2]);
            }
        my_clone.Save("MedianFilter.jpg");
    }

    //3
    public static void MaxFilter(Image<Rgb24> image, int width, int height)
    {
        if (width % 2 == 0 || height % 2 == 0)
        {
            Console.WriteLine("Szerokość i wysokość muszą być wartościami nieparzystymi.");
            return;
        }

        Image<Rgb24>my_clone = image.Clone();
        byte[] R = new byte[width * height];
        byte[] G = new byte[width * height];
        byte[] B = new byte[width * height];

        for (int a = width / 2; a < image.Width - width / 2; a++)
            for (int b = height / 2; b < image.Height - height / 2; b++)
            {
                int i = 0;
                for (int x = a - width / 2; x <= a + width / 2; x++)
                    for (int y = b - height / 2; y <= b + height / 2; y++)
                    {
                        R[i] = image[x, y].R;
                        G[i] = image[x, y].G;
                        B[i] = image[x, y].B;
                        i++;
                    }
                Array.Sort(R);
                Array.Sort(G);
                Array.Sort(B);
                my_clone[a, b] = new Rgb24(R[width * height - 1], G[width * height - 1], B[width * height - 1]);
            }
        my_clone.Save("MaxFilter.jpg");
    }

    public static void MinFilter(Image<Rgb24> image, int width, int height)
    {
        if (width % 2 == 0 || height % 2 == 0)
        {
            Console.WriteLine("Szerokość i wysokość muszą być wartościami nieparzystymi.");
            return;
        }

        Image<Rgb24>my_clone = image.Clone();
        byte[] R = new byte[width * height];
        byte[] G = new byte[width * height];
        byte[] B = new byte[width * height];

        for (int a = width / 2; a < image.Width - width / 2; a++)
            for (int b = height / 2; b < image.Height - height / 2; b++)
            {
                int i = 0;
                for (int x = a - width / 2; x <= a + width / 2; x++)
                    for (int y = b - height / 2; y <= b + height / 2; y++)
                    {
                        R[i] = image[x, y].R;
                        G[i] = image[x, y].G;
                        B[i] = image[x, y].B;
                        i++;
                    }
                Array.Sort(R);
                Array.Sort(G);
                Array.Sort(B);
                my_clone[a, b] = new Rgb24(R[0], G[0], B[0]);
            }
        my_clone.Save("MinFilter.jpg");
    }

    public static void MeanFilter(Image<Rgb24> image, int width, int height)
    {
        if (width % 2 == 0 || height % 2 == 0)
        {
            Console.WriteLine("Szerokość i wysokość muszą być wartościami nieparzystymi.");
            return;
        }

        Image<Rgb24>my_clone = image.Clone();
        byte[] R = new byte[width * height];
        byte[] G = new byte[width * height];
        byte[] B = new byte[width * height];

        for (int a = width / 2; a < image.Width - width / 2; a++)
            for (int b = height / 2; b < image.Height - height / 2; b++)
            {
                int i = 0;
                for (int x = a - width / 2; x <= a + width / 2; x++)
                    for (int y = b - height / 2; y <= b + height / 2; y++)
                    {
                        R[i] = image[x, y].R;
                        G[i] = image[x, y].G;
                        B[i] = image[x, y].B;
                        i++;
                    }
                int sumR = R.Sum(b => (int)b);
                int sumG = G.Sum(b => (int)b);
                int sumB = B.Sum(b => (int)b);
                byte meanR = (byte)(sumR / (width * height));
                byte meanG = (byte)(sumG / (width * height));
                byte meanB = (byte)(sumB / (width * height));
                my_clone[a, b] = new Rgb24(meanR, meanG, meanB);
            }
        my_clone.Save("MeanFilter.jpg");
    }

    //4
    public static void ConvolutionFilter(Image<Rgb24> image, int[,] kernel)
    {
        int width = kernel.GetLength(0);
        int height = kernel.GetLength(1);
        if (width % 2 == 0 || height % 2 == 0)
        {
            Console.WriteLine("Szerokość i wysokość muszą być wartościami nieparzystymi.");
            return;
        }

        Image<Rgb24> my_clone = image.Clone();
        int offsetX = width / 2;
        int offsetY = height / 2;

        for (int a = 0; a < image.Width; a++)
            for (int b = 0; b < image.Height; b++)
            {
                int sumR = 0;
                int sumG = 0;
                int sumB = 0;

                for (int x = -offsetX; x <= offsetX; x++)
                    for (int y = -offsetY; y <= offsetY; y++)
                    {
                        int pixelX = a + x;
                        int pixelY = b + y;
                        int kernelX = x + offsetX;
                        int kernelY = y + offsetY;

                        if (pixelX < 0 || pixelX >= image.Width || pixelY < 0 || pixelY >= image.Height)
                        {
                            pixelX = GetNearestPixel(pixelX, 0, image.Width - 1);
                            pixelY = GetNearestPixel(pixelY, 0, image.Height - 1);
                        }

                        sumR += image[pixelX, pixelY].R * kernel[kernelX, kernelY];
                        sumG += image[pixelX, pixelY].G * kernel[kernelX, kernelY];
                        sumB += image[pixelX, pixelY].B * kernel[kernelX, kernelY];
                    }

                byte meanR = ClampByte(sumR);
                byte meanG = ClampByte(sumG);
                byte meanB = ClampByte(sumB);
                my_clone[a, b] = new Rgb24(meanR, meanG, meanB);
            }

        my_clone.Save("ConvolutionFilter.jpg");
    }

    private static byte ClampByte(int value)
    {
        return (byte)Math.Max(0, Math.Min(255, value));
    }

    private static int GetNearestPixel(int value, int minValue, int maxValue)
    {
        if (value < minValue)
            return minValue;
        if (value > maxValue)
            return maxValue;
        return value;
    }

}