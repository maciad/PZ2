using System.Globalization;

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
                line = sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    list.Add(parse(line.Split(',')));
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

    class Orders
    {
        public String OrderID { get; set; }
        public String CustomerID { get; set; }
        public String EmployeeID { get; set; }
        public String OrderDate { get; set; }
        public String RequiredDate { get; set; }
        public String ShippedDate { get; set; }
        public String ShipVia { get; set; }
        public String Freight { get; set; }
        public String ShipName { get; set; }
        public String ShipAddress { get; set; }
        public String ShipCity { get; set; }
        public String ShipRegion { get; set; }
        public String ShipPostalCode { get; set; }
        public String ShipCountry { get; set; }

        public Orders(String[] data)
        {
            OrderID = data[0];
            CustomerID = data[1];
            EmployeeID = data[2];
            OrderDate = data[3];
            RequiredDate = data[4];
            ShippedDate = data[5];
            ShipVia = data[6];
            Freight = data[7];
            ShipName = data[8];
            ShipAddress = data[9];
            ShipCity = data[10];
            ShipRegion = data[11];
            ShipPostalCode = data[12];
            ShipCountry = data[13];
        }

        public override string ToString()
        {
            return $"{OrderID} {CustomerID} {EmployeeID} {OrderDate}";
        }
    }

    class Order_Details
    {
        public String OrderID { get; set; }
        public String ProductID { get; set; }
        public String UnitPrice { get; set; }
        public String Quantity { get; set; }
        public String Discount { get; set; }

        public Order_Details(String[] data)
        {
            OrderID = data[0];
            ProductID = data[1];
            UnitPrice = data[2];
            Quantity = data[3];
            Discount = data[4];
        }

        public override string ToString()
        {
            return $"{OrderID} {ProductID} {UnitPrice} {Quantity}";
        }
    }

    class Main
    {
        public static void main(String [] args)
        {
            // ZAD 1
            Reader<Territories> reader = new Reader<Territories>();
            List<Territories> territories = reader.read("territories.csv", (data) => new Territories(data));

            Reader<Employees> reader2 = new Reader<Employees>();
            List<Employees> employees = reader2.read("employees.csv", (data) => new Employees(data));

            Reader<Regions> reader3 = new Reader<Regions>();
            List<Regions> regions = reader3.read("regions.csv", (data) => new Regions(data));

            Reader<Employee_Territories> reader4 = new Reader<Employee_Territories>();
            List<Employee_Territories> employee_territories = reader4.read("employee_territories.csv", (data) => new Employee_Territories(data));

            // ZAD 2
            var query1 = from e in employees
                         select e.LastName;
            foreach (var item in query1)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("\n### ZAD 3 #############################################################\n");
            
            // ZAD 3
            var query2 = from e in employees
                         join et in employee_territories on e.EmployeeID equals et.EmployeeID
                         join t in territories on et.TerritoryID equals t.TerritoryID
                         join r in regions on t.RegionID equals r.RegionID
                         select new { e.LastName, t.TerritoryDescription, r.RegionDescription };
            foreach (var item in query2)
            {
                Console.WriteLine(item.LastName + " " + item.TerritoryDescription + " " + item.RegionDescription);
            }
            Console.WriteLine("\n### ZAD 4 #############################################################\n");
            
            // ZAD 4
            var query3 = from r in regions
                         join t in territories on r.RegionID equals t.RegionID
                         join et in employee_territories on t.TerritoryID equals et.TerritoryID
                         join e in employees on et.EmployeeID equals e.EmployeeID
                         group e by r.RegionDescription into g
                         select new {Region = g.Key, Employees = g };
            foreach (var item in query3)
            {
                Console.WriteLine("REGION: " + item.Region);
                foreach (var item2 in item.Employees.Distinct())
                {
                    Console.WriteLine('\t' + item2.FirstName + " " + item2.LastName);
                }
            }
            Console.WriteLine("\n### ZAD 5 #############################################################\n");
            
            // ZAD 5
            var query4 = from r in regions
                         join t in territories on r.RegionID equals t.RegionID
                         join et in employee_territories on t.TerritoryID equals et.TerritoryID
                         join e in employees on et.EmployeeID equals e.EmployeeID
                         group e by r.RegionDescription into g
                         select new {Region = g.Key, Employees = g};
            foreach (var item in query4)
            {
                Console.WriteLine(item.Region + " : " + item.Employees.Distinct().Count());
            }
            Console.WriteLine("\n### ZAD 6 #############################################################\n");

            // ZAD 6
            Reader<Orders> reader5 = new Reader<Orders>();
            List<Orders> orders = reader5.read("orders.csv", (data) => new Orders(data));

            Reader<Order_Details> reader6 = new Reader<Order_Details>();
            List<Order_Details> order_details = reader6.read("orders_details.csv", (data) => new Order_Details(data));

            var query5 = from e in employees
            join o in orders on e.EmployeeID equals o.EmployeeID
            join od in order_details on o.OrderID equals od.OrderID
            group od by e.LastName into g
            select new { LastName = g.Key, Order_Details = g };
            foreach (var item in query5)
            {
                Console.WriteLine(item.LastName + "\n\tcount: " + item.Order_Details.Count()
                    + "\n\tmean: " + item.Order_Details.Average(x => Decimal.Parse(x.UnitPrice, CultureInfo.InvariantCulture) * (1 - Decimal.Parse(x.Discount, CultureInfo.InvariantCulture)) * Decimal.Parse(x.Quantity, CultureInfo.InvariantCulture))
                    + "\n\tmax: " + item.Order_Details.Max(x => Decimal.Parse(x.UnitPrice, CultureInfo.InvariantCulture) * (1 - Decimal.Parse(x.Discount, CultureInfo.InvariantCulture)) * Decimal.Parse(x.Quantity, CultureInfo.InvariantCulture)));
            }
            
        }
     }

}

