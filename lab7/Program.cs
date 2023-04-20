using System.Security.Cryptography;

class MainClass
{
    public static void Main(string[] args)
    {
        zad1.ex1(args);
    }
}


class zad1
{
    // Program szyfrujący przy pomocy kryptografii klucza asymetrycznego. 
    // Napisz program który jako parametr przyjmuje typ polecenia. W zależności 
    // od wybranego typu polecenia:
    // Jeżeli typ polecenia = 0 program ma wygenerować i zapisać do dwóch plików (o dowolnych nazwach, można je wpisać "na sztywno") klucz publiczny oraz klucz prywatny algorytmu RSA.
    // Jeżeli typ polecenia = 1, program dodatkowo pobiera nazwę dwóch plików (a), (b). Podany plik (a) ma zostać zaszyfrowany przy pomocy klucza publicznego odczytanego z pliku, który został stworzony przy pomocy tego programu kiedy typ polecenia = 0. Zaszyfrowane dane mają być zapisane w pliku (b).
    // Jeżeli typ polecenia = 2, program dodatkowo pobiera nazwę dwóch plików (a), (b). Podany plik (a) ma zostać odszyfrowany przy pomocy klucza prywatnego odczytanego z pliku, który został stworzony przy pomocy tego programu kiedy typ polecenia = 0. Odszyfrowane dane mają być zapisane w pliku (b).
 
    public static void ex1(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Brak parametru");
            return;
        }
        int typ = int.Parse(args[0]);
        if (typ == 0)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            string publicKey = rsa.ToXmlString(false);
            string privateKey = rsa.ToXmlString(true);
            File.WriteAllText("publicKey.xml", publicKey);
            File.WriteAllText("privateKey.xml", privateKey);
        }
        else if (typ == 1)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Brak parametru");
                return;
            }
            string publicKey = File.ReadAllText("publicKey.xml");
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publicKey);
            byte[] data = File.ReadAllBytes(args[1]);
            byte[] encryptedData = rsa.Encrypt(data, false);
            File.WriteAllBytes(args[2], encryptedData);
        }
        else if (typ == 2)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Brak parametru");
                return;
            }
            string privateKey = File.ReadAllText("privateKey.xml");
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privateKey);
            byte[] data = File.ReadAllBytes(args[1]);
            byte[] decryptedData = rsa.Decrypt(data, false);
            File.WriteAllBytes(args[2], decryptedData);
        }
    }
}

class zad2
{
    // Program liczący sumę kontrolną. Napisz program który jako parametry przyjmuje 
    // nazwę pliku (a), nazwę pliku zawierającego hash (b) oraz algorytm hashowania 
    // (SHA256, SHA512 lub MD5) (c). Jeżeli plik hash (b) nie istnieje, program ma policzyć
    // hash z pliku (a) i zapisać go pod nazwą (b). Jeżeli plik (b) istnieje, program ma
    // zweryfikować hash i wypisać do konsoli, czy hash jest zgodny.
    public static void ex2(string[] args)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Brak parametru");
            return;
        }
        string fileName = args[0];
        string hashFileName = args[1];
        string hashAlgorithm = args[2];
        if (!File.Exists(hashFileName))
        {
            byte[] hash = null;
            if (hashAlgorithm == "SHA256")
            {
                SHA256 sha256 = SHA256.Create();
                hash = sha256.ComputeHash(File.ReadAllBytes(fileName));
            }
            else if (hashAlgorithm == "SHA512")
            {
                SHA512 sha512 = SHA512.Create();
                hash = sha512.ComputeHash(File.ReadAllBytes(fileName));
            }
            else if (hashAlgorithm == "MD5")
            {
                MD5 md5 = MD5.Create();
                hash = md5.ComputeHash(File.ReadAllBytes(fileName));
            }
            else
            {
                Console.WriteLine("Nieznany algorytm");
                return;
            }
            File.WriteAllBytes(hashFileName, hash);
        }
        else
        {
            byte[] hash = File.ReadAllBytes(hashFileName);
            byte[] hash2 = null;
            if (hashAlgorithm == "SHA256")
            {
                SHA256 sha256 = SHA256.Create();
                hash2 = sha256.ComputeHash(File.ReadAllBytes(fileName));
            }
            else if (hashAlgorithm == "SHA512")
            {
                SHA512 sha512 = SHA512.Create();
                hash2 = sha512.ComputeHash(File.ReadAllBytes(fileName));
            }
            else if (hashAlgorithm == "MD5")
            {
                MD5 md5 = MD5.Create();
                hash2 = md5.ComputeHash(File.ReadAllBytes(fileName));
            }
            else
            {
                Console.WriteLine("Nieznany algorytm");
                return;
            }
            if (hash.SequenceEqual(hash2))
            {
                Console.WriteLine("Hash jest zgodny");
            }
            else
            {
                Console.WriteLine("Hash nie jest zgodny");
            }
        }
    }
}

