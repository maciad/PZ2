
lab4.Main.main(null);

namespace lab4
{
    class Reader<T>
    {
        public List<T> read(String path, Func<String[], T> parse)
        {
            List<T> list = new List<T>();
            using (StreamReader sr = new StreamReader(path))
            {
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    list.Add(parse(line[..].Split(',')));
                }
            }
            return list;
        }
    }

    class Territories
    {
        public String TerritoryID { get; set; }
        public String TerritoryDescription { get; set; }
        public String RegionID { get; set; }

        public Territories(String[] data)
        {
            TerritoryID = data[0];
            TerritoryDescription = data[1];
            RegionID = data[2];
        }

        public override string ToString()
        {
            return $"{TerritoryID} {TerritoryDescription} {RegionID}";
        }
    }

    class Employee_Territories
    {
        public String EmployeeID { get; set; }
        public String TerritoryID { get; set; }

        public Employee_Territories(String[] data)
        {
            EmployeeID = data[0];
            TerritoryID = data[1];
        }

        public override string ToString()
        {
            return $"{EmployeeID} {TerritoryID}";
        }
    }

    class Employees
    {
        public String EmployeeID { get; set; }
        public String LastName { get; set; }
        public String FirstName { get; set; }
        public String Title { get; set; }
        public String TitleOfCourtesy { get; set; }
        public String BirthDate { get; set; }
        public String HireDate { get; set; }
        public String Address { get; set; }
        public String City { get; set; }
        public String Region { get; set; }
        public String PostalCode { get; set; }
        public String Country { get; set; }
        public String HomePhone { get; set; }
        public String Extension { get; set; }
        public String Photo { get; set; }
        public String Notes { get; set; }
        public String ReportsTo { get; set; }
        public String PhotoPath { get; set; }

        public Employees(String[] data)
        {
            EmployeeID = data[0];
            LastName = data[1];
            FirstName = data[2];
            Title = data[3];
            TitleOfCourtesy = data[4];
            BirthDate = data[5];
            HireDate = data[6];
            Address = data[7];
            City = data[8];
            Region = data[9];
            PostalCode = data[10];
            Country = data[11];
            HomePhone = data[12];
            Extension = data[13];
            Photo = data[14];
            Notes = data[15];
            ReportsTo = data[16];
            PhotoPath = data[17];
        }

        public override string ToString()
        {
            return $"{EmployeeID} {LastName} {FirstName} {Title}";
        }
    }

    class Regions
    {
        public String RegionID { get; set; }
        public String RegionDescription { get; set; }

        public Regions(String[] data)
        {
            RegionID = data[0];
            RegionDescription = data[1];
        }

        public override string ToString()
        {
            return $"{RegionID} {RegionDescription}";
        }
    }

    class Main
    {
        public static void main(String [] args)
        {
            Reader<Territories> reader = new Reader<Territories>();
            List<Territories> territories = reader.read("territories.csv", (data) => new Territories(data));

            Reader<Employees> reader2 = new Reader<Employees>();
            List<Employees> employees = reader2.read("employees.csv", (data) => new Employees(data));

            Reader<Regions> reader3 = new Reader<Regions>();
            List<Regions> regions = reader3.read("regions.csv", (data) => new Regions(data));

            Reader<Employee_Territories> reader4 = new Reader<Employee_Territories>();
            List<Employee_Territories> employee_territories = reader4.read("employee_territories.csv", (data) => new Employee_Territories(data));

            // select last names of all employees
            var query1 = from e in employees
                         select e.TitleOfCourtesy;

            foreach (var item in query1)
            {
                Console.WriteLine(item);
            }


        }
     }

}

