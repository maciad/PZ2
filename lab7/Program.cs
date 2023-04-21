using System.Numerics;
using System.Security.Cryptography;

class MainClass
{
    public static void Main(string[] args)
    {
        // zad1.ex(args);
        zad2.ex(args);
        // zad3.ex(args);
        // zad4.ex(args);
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
 
    public static void ex(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Nieprawidłowa liczba parametrów");
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
            if (args.Length != 3)
            {
                Console.WriteLine("Nieprawidłowa liczba parametrów");
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
            if (args.Length != 3)
            {
                Console.WriteLine("Nieprawidłowa liczba parametrów");
                return;
            }
            string privateKey = File.ReadAllText("privateKey.xml");
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privateKey);
            byte[] data = File.ReadAllBytes(args[1]);
            byte[] decryptedData = rsa.Decrypt(data, false);
            File.WriteAllBytes(args[2], decryptedData);
        }
        else
        {
            Console.WriteLine("Nieznany typ operacji");
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
    public static void ex(string[] args)
    {
        if (args.Length != 3)
        {
            Console.WriteLine("Nieprawidłowa liczba parametrów");
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

class zad3
{
    // Podpisywanie danych z pliku. Zakładamy, że mamy dwa pliki w których znajduje się
    // klucz prywatny i publiczny algorytmu RSA. Te pliki zostały utworzone np. programem
    // z punktu 1. Program pobiera nazwę dwóch plików (a) i (b). Program wczytuje plik 
    // (a). Jeśli plik (b) istnieje, program ma potraktować go jako podpis wygenerowany
    // z pliku (a) przy pomocy klucza prywatnego. Program ma zweryfikować, czy podpis
    // jest poprawny i wypisać wynik weryfikacji na ekran. Jeśli plik (b) nie istnieje,
    // program ma wygenerować podpis danych z pliku (a) używając klucza prywatnego 
    // i zapisać ten podpis do pliku (b).
    public static void ex(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Nieprawidłowa liczba parametrów");
            return;
        }
        string fileName = args[0];
        string signatureFileName = args[1];

        string privateKey = File.ReadAllText("privateKey.xml");
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(privateKey);

        if (File.Exists(signatureFileName))
        {
            byte[] signature = File.ReadAllBytes(signatureFileName);
            byte[] data = File.ReadAllBytes(fileName);
            if (rsa.VerifyData(data, new SHA256CryptoServiceProvider(), signature))
            {
                Console.WriteLine("Podpis jest poprawny");
            }
            else
            {
                Console.WriteLine("Podpis nie jest poprawny");
            }
        }
        else
        {
            byte[] data = File.ReadAllBytes(fileName);
            byte[] signature = rsa.SignData(data, new SHA256CryptoServiceProvider());
            File.WriteAllBytes(signatureFileName, signature);
        }

    }
}

class zad4
{
    // Zaszyfrowanie pliku algorytmem klucza symetrycznego przy użyciu hasła. 
    // Program ma przyjmować cztery parametry: pliki (a), (b), hasło, typ operacji.
    // Jeżeli typ operacji = 0 program ma zaszyfrować plik (a) algorytmem AES, którego
    // klucz ma zostać wygenerowany przy pomocy podanego hasła. Zaszyfrowane dane
    // mają być zapisane do pliku (b).
    // Jeżeli typ operacji = 1 program ma odszyfrować plik (a) algorytmem AES, którego
    // klucz ma zostać wygenerowany przy pomocy podanego hasła. Odszyfrowane dane mają 
    // być zapisane do pliku (b). Wszystkie dane wymagane do utworzenia klucza algorytmu
    // AES z wyjątkiem hasła mogą byc wpisane "na sztywno" do programu.
    public static void ex(string[] args)
    {
        if (args.Length != 4)
        {
            Console.WriteLine("Nieprawidłowa liczba parametrów");
            return;
        }

        string fileName = args[0];
        string encryptedFileName = args[1];
        string password = args[2];
        int typ = int.Parse(args[3]);

        Aes aes = Aes.Create();
        byte[] salt = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08 };
        Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(password, salt, 1000);
        aes.Key = rfc.GetBytes(32);
        aes.IV = rfc.GetBytes(16);
        aes.Mode = CipherMode.CFB;
        aes.Padding = PaddingMode.PKCS7;

        byte[] data = File.ReadAllBytes(fileName);

        if (typ == 0)
        {
            byte[] encryptedData = aes.CreateEncryptor().TransformFinalBlock(data, 0, data.Length);
            File.WriteAllBytes(encryptedFileName, encryptedData);
        }
        else if (typ == 1)
        {
            byte[] decryptedData = aes.CreateDecryptor().TransformFinalBlock(data, 0, data.Length);
            File.WriteAllBytes(encryptedFileName, decryptedData);
        }
        else
        {
            Console.WriteLine("Nieznany typ operacji");
        }
    }
}

