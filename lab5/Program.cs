// class Data 
// {
//     public int ProducerId { get; set; }
    
//     public Data(int producerId)
//     {
//         ProducerId = producerId;
//     }
// }

// class Producer 
// {
//     private int id;
//     private int minSleep;
//     private int maxSleep;
//     private Random random = new Random();

//     public Producer(int id, int minSleep, int maxSleep)
//     {
//         this.id = id;
//         this.minSleep = minSleep;
//         this.maxSleep = maxSleep;
//     }

//     public void produce()
//     {
//         while (Program.isRunning)
//         {
//             int sleep = random.Next(minSleep, maxSleep);
//             Thread.Sleep(sleep);
            
//             Data data = new Data(id);
//             lock (Program.lockObject)
//             {
//                 Program.dataList.Add(data);
//             }
//         }
//     }



// }

// class Consumer 
// {
//     private int id;
//     private int minSleep;
//     private int maxSleep;
//     private List<Data> dataList;
//     private Dictionary<int, int> consumedData = new Dictionary<int, int>();
//     private Random random = new Random();

//     public Consumer(int id, int minSleep, int maxSleep, List<Data> dataList)
//     {
//         this.id = id;
//         this.minSleep = minSleep;
//         this.maxSleep = maxSleep;
//         this.dataList = dataList;
//     }

//     public void consume()
//     {
//         while (Program.isRunning)
//         {
//             int sleep = random.Next(minSleep, maxSleep);
//             Thread.Sleep(sleep);

//             lock (Program.lockObject)
//             {
//                 if (Program.dataList[0] != null)
//                 {            
//                     Data data = Program.dataList[0];
//                     Program.dataList.RemoveAt(0);
//                     this.dataList.Add(data);
//                     if (!consumedData.ContainsKey(data.ProducerId))
//                     {
//                         consumedData[data.ProducerId] = 0;
//                     }
//                     consumedData[data.ProducerId]++;
//                     Console.WriteLine("Consumer {0} consumed data from producer {1}", id, data.ProducerId);
//                 }
//             }
//         }
//         foreach (KeyValuePair<int, int> entry in consumedData)
//         {
//             Console.WriteLine("Consumer {0}: {1} data from producer {2}", id, entry.Value, entry.Key);
//         }
//     }
// }


// class Program
// {
//     public static List<Data> dataList = new List<Data>();
//     public static bool isRunning = true;
//     public static Object lockObject = new object();

//     public static void Main(string[] args)
//     {
//         int p = 10;
//         int c = 5;
//         Thread[] producers = new Thread[p];
//         Thread[] consumers = new Thread[c];

//         for (int i = 0; i < p; i++)
//         {
//             producers[i] = new Thread(new Producer(i, 500, 3000).produce);
//             producers[i].Start();
//         }

//         for (int i = 0; i < c; i++)
//         {
//             consumers[i] = new Thread(new Consumer(i, 500, 3000, new List<Data>()).consume);
//             consumers[i].Start();
//         }

//         while (isRunning)
//         {
//             if (Console.ReadKey().Key == ConsoleKey.Q)
//             {
//                 isRunning = false;
//             }
//         }
//         Console.WriteLine("==================================");
    
//         for (int i = 0; i < p; i++)
//         {
//             producers[i].Join();
//         }
//         for (int i = 0; i < c; i++)
//         {
//             consumers[i].Join();
//         }
//     }
// }


class Program2
{

    public static void Main(string[] args)
    {
        bool isRunning = true;
        List<String> files = Directory.GetFiles("./test").ToList();
        Thread thread = new Thread(() => monitor(files));
        thread.Start();
        while (isRunning)
        {
            if (Console.ReadKey().Key == ConsoleKey.Q)
            {

            }
        }
    }

    public static void monitor(List<String> files)
    {
        while (true)
        {
            List<String> newFiles = Directory.GetFiles("./test").ToList();
            foreach (String file in newFiles)
            {
                if (!files.Contains(file))
                {
                    Console.WriteLine("dodano plik {0}", file);
                }
            }
            foreach (String file in files)
            {
                if (!newFiles.Contains(file))
                {
                    Console.WriteLine("usunięto plik {0}", file);
                }
            }
            files = newFiles;
        }
    }
}
