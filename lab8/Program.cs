using System.Globalization;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;




class Program
{
    public static void Main(string[] args)
    {
        CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

        (List<List<String>> data, List<String> headers) = readCSV("test.csv", ',');
        List<(string, string, bool)> types = checkColumns(data, headers);

        Console.WriteLine("Nazwa , typ, NULLABLE");
        foreach (var type in types)
        {
            Console.WriteLine(type);
        }

        string connectionString = "Data Source=database.sqlite";
        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            CreateTableFromData(types, "test", connection);
            
        }
        
    
    }


    public static void print(List<List<String>> data, List<String> headers)
    {
        foreach (var header in headers)
        {
            Console.Write(header + " ");
        }
        Console.WriteLine();
        foreach (var row in data)
        {
            foreach (var value in row)
            {
                if (value == "")
                {
                    Console.Write("NULL ");
                }
                else
                {
                    Console.Write(value + " ");
                }
            }
            Console.WriteLine();
        }
    }


    public static (List<List<String>>, List<String>) readCSV(String filename, char separator)
    {
        List<List<String>> result = new List<List<String>>();
        List<String> headers = null;
        using (StreamReader sr = new StreamReader(filename))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] values = line.Split(separator);
                if (headers == null)
                {
                    headers = new List<String>(values);
                }
                else
                {
                    result.Add(new List<String>(values));
                }
            }
        }
        return (result, headers);
    }


    public static List<(string, string, bool)> checkColumns(List<List<String>> data, List<String> headers)
    {
        var types = new List<(string, string, bool)>();
        for (int i = 0; i < headers.Count; i++)
        {
            string header = headers[i];
            bool canBeNull = false;
            String type = null;

            var column = data.Select(row => row[i]).ToList();

            if (column.Any(value => value == ""))
            {
                canBeNull = true;
            }
            if (column.All(value => int.TryParse(value, out _) || value == ""))
            {
                type = "INTEGER";
            }
            else if (column.All(value => double.TryParse(value, out _) || value == ""))
            {
                type = "REAL";
            }
            else
            {
                type = "TEXT";
            }

            types.Add((header, type.ToString(), canBeNull));
        }

        return types;
    }


    public static void CreateTableFromData(List<(string, string, bool)> types, string name, SqliteConnection connection)
    {
        string createTableQuery = "CREATE TABLE " + name + " (";
        foreach (var column in types)
        {
            createTableQuery += column.Item1 + " " + column.Item2;
            if (!column.Item3)
            {
                createTableQuery += " NOT NULL";
            }
            createTableQuery += ", ";
        }
        createTableQuery = createTableQuery.TrimEnd(',', ' ') + ");";
        
        using (SqliteCommand command = new SqliteCommand(createTableQuery, connection))
        {
            command.ExecuteNonQuery();
        }

    }
}