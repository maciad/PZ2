using System.Reactive.Linq;

class Program
{
    static void Main()
    {
        // Tworzenie źródeł
        var source1 = Observable.Interval(TimeSpan.FromSeconds(1))
            .Select(x => Math.Sin(x * 0.01));


        var source2 = Observable.Interval(TimeSpan.FromSeconds(1))
            .Select(_ => new Random().NextDouble() * 2 - 1);


        // Subskrypcja źródeł
        var subscription1 = source1
            .Where(x => x >= 0 && x <= 0.3)
            .Subscribe(value =>
            {
                Console.WriteLine("Wartość z pierwszego źródła: " + value);
            });

        var subscription2 = source2.Scan(double.MinValue, (max, value) => Math.Max(max, value))
            .Subscribe(value =>
            {
                Console.WriteLine("Maksymalna wartość z drugiego źródła: " + value);
            });

        var subscription3 = source1.Merge(source2)
            .Subscribe(value =>
            {
                Console.WriteLine("Wartość z połączonych strumieni: " + value);
            });


        //  20 sekund oczekiwania
        Thread.Sleep(TimeSpan.FromSeconds(20));


        // Zakończenie subskrypcji
        subscription1.Dispose();
        Console.WriteLine("Obserwacja pierwszego źródła zakończona.");

        subscription2.Dispose();
        Console.WriteLine("Obserwacja drugiego źródła zakończona.");

        subscription3.Dispose();
        Console.WriteLine("Obserwacja połączonych strumieni zakończona.");

    }
}
