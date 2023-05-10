public class main
{
    public static void Main()
    {
        OsobaFizyczna of1 = new OsobaFizyczna("Jan", null,"Kowalski", "11111111111", "1234");
        OsobaFizyczna of2 = new OsobaFizyczna("JAkub", "Krzysztof", "Nowak", "22222222222", "5678");
        OsobaPrawna op1 = new OsobaPrawna("Firma1", "Warszawa");
        RachunekBankowy r1 = new RachunekBankowy("1122", 1000, false, new List<PosiadaczRachunku> { of1 });
        r1 += of2;
        RachunekBankowy r2 = new RachunekBankowy("3344", 2000, true, new List<PosiadaczRachunku> { op1 });
        RachunekBankowy.DokonajTransakcji(r1, r2, 500, "Pozyczam ci 500");
        RachunekBankowy.DokonajTransakcji(r2, r1, 1000, "oddaje 1000");
        RachunekBankowy.DokonajTransakcji(r1, r2, 3000, "asdsad");
        //r1 -= of2;
        //r1 -= of2;
        Console.WriteLine(r1);
        Console.WriteLine(r2);
    }
}

public abstract class PosiadaczRachunku
{
    public override abstract string ToString();
}

public class OsobaFizyczna : PosiadaczRachunku
{
    private string imie;
    public string Imie
    {
        get { return imie; }
        set { imie = value; }
    }
    private string drugieImie;
    public string DrugieImie
    {
        get { return drugieImie; }
        set { drugieImie = value; }
    }
    private string nazwisko;
    public string Nazwisko
    {
        get { return nazwisko; }
        set { nazwisko = value; }
    }
    private string pesel;
    public string Pesel
    {
        get { return pesel; }
        set 
        { 
            if (value.Length != 11 || value == null)
                throw new Exception("Nieprawidłowy numer PESEL");
            pesel = value; 
        }
    }
    private string numerPaszportu;
    public string NumerPaszportu
    {
        get { return numerPaszportu; }
        set { numerPaszportu = value; }
    }

    public OsobaFizyczna(string imie, string drugieImie, string nazwisko, string pesel, string numerPaszportu)
    {

        this.imie = imie;
        this.drugieImie = drugieImie;
        this.nazwisko = nazwisko;
        if (pesel.Length != 11)
            throw new Exception("Nieprawidłowy numer PESEL");
        this.pesel = pesel;
        this.numerPaszportu = numerPaszportu;
        if (pesel == null && numerPaszportu == null)
            throw new Exception("Nie podano numeru PESEL ani numeru paszportu");
    }

    public override string ToString()
    {
        return "Osoba Fizyczna: " + imie + " " + drugieImie + " " + nazwisko;
    }
    
}

public class OsobaPrawna : PosiadaczRachunku
{
    private string nazwa;
    public string Nazwa
    {
        get { return nazwa; }
    }

    private string siedziba;
    public string Siedziba
    {
        get { return siedziba; }
    }

    public OsobaPrawna(string nazwa, string siedziba)
    {
        this.nazwa = nazwa;
        this.siedziba = siedziba;
    }

    public override string ToString()
    {
        return "Osoba Prawna: " + nazwa + " " + siedziba;
    }
}

public class RachunekBankowy
{
    private string numer;
    public string Numer
    {
        get { return numer; }
        set { numer = value; }
    }
    private Decimal stanRachunku;
    public Decimal StanRachunku
    {
        get { return stanRachunku; }
        set { stanRachunku = value; }
    }
    private bool czyDozwolonyDebet;
    public bool CzyDozwolonyDebet
    {
        get { return czyDozwolonyDebet; }
        set { czyDozwolonyDebet = value; }
    }
    private List<PosiadaczRachunku> posiadaczeRachunku = new List<PosiadaczRachunku>();
    public List<PosiadaczRachunku> PosiadaczeRachunku
    {
        get { return posiadaczeRachunku; }
        set { posiadaczeRachunku = value; }
    }
    private List<Transakcja> _Transakcje = new List<Transakcja>();
    public List<Transakcja> Transakcje
    {
        get { return _Transakcje; }
        set { _Transakcje = value; }
    }

    public RachunekBankowy(string numer, Decimal stanRachunku, bool czyDozwolonyDebet, List<PosiadaczRachunku> posiadaczeRachunku)
    {
        this.numer = numer;
        this.stanRachunku = stanRachunku;
        this.czyDozwolonyDebet = czyDozwolonyDebet;
        if (posiadaczeRachunku.Count == 0)
            throw new Exception("Nie podano posiadaczy rachunku");
        this.posiadaczeRachunku = posiadaczeRachunku;

    }

    public static void DokonajTransakcji(RachunekBankowy rachunekŻródłowy, RachunekBankowy rachunekDocelowy, Decimal kwota, string opis)
    {
        if (kwota < 0)
            throw new Exception("Kwota nie może być ujemna");
        if (rachunekŻródłowy == null && rachunekDocelowy == null)
            throw new Exception("Nie podano rachunku źródłowego ani docelowego");


        if (rachunekŻródłowy == null)
        {
            rachunekDocelowy.StanRachunku += kwota;
            Transakcja transakcja = new Transakcja(rachunekŻródłowy, rachunekDocelowy, kwota, opis);
            rachunekDocelowy.Transakcje.Add(transakcja);
            return;
        }

        if (!rachunekŻródłowy.CzyDozwolonyDebet && rachunekŻródłowy.StanRachunku < kwota)
            throw new Exception("Brak środków na rachunku");

        if (rachunekDocelowy == null)
        {
            rachunekŻródłowy.StanRachunku -= kwota;
            Transakcja transakcja = new Transakcja(rachunekŻródłowy, rachunekDocelowy, kwota, opis);
            rachunekŻródłowy.Transakcje.Add(transakcja);
            return;
        }

        rachunekŻródłowy.StanRachunku -= kwota;
        rachunekDocelowy.StanRachunku += kwota;
        Transakcja transakcja1 = new Transakcja(rachunekŻródłowy, rachunekDocelowy, kwota, opis);
        rachunekŻródłowy.Transakcje.Add(transakcja1);
        rachunekDocelowy.Transakcje.Add(transakcja1);
    }

    public static RachunekBankowy operator+ (RachunekBankowy rachunek, PosiadaczRachunku posiadacz)
    {
        if (rachunek.PosiadaczeRachunku.Contains(posiadacz))
            throw new Exception("Posiadacz rachunku już istnieje");
        rachunek.PosiadaczeRachunku.Add(posiadacz);
        return rachunek;
    }

    public static RachunekBankowy operator- (RachunekBankowy rachunek, PosiadaczRachunku posiadacz)
    {
        if (!rachunek.PosiadaczeRachunku.Contains(posiadacz))
            throw new Exception("Posiadacz rachunku nie istnieje");
        if (rachunek.PosiadaczeRachunku.Count == 1)
            throw new Exception("Nie można usunąć ostatniego posiadacza rachunku");
        rachunek.PosiadaczeRachunku.Remove(posiadacz);
        return rachunek;
    }

    public override string ToString()
    {
        string posiadacze = "";
        foreach (PosiadaczRachunku posiadacz in posiadaczeRachunku)
        {
            posiadacze += posiadacz.ToString() + "\n";
        }

        string transakcje = "";
        foreach (Transakcja transakcja in Transakcje)
        {
            transakcje += transakcja.ToString() + "\n";
        }
        return "Rachunek nr: " + numer + "\nstan: " + stanRachunku + "\nposiadacze:\n" + posiadacze + "\nTransakcje\n" + transakcje;
    }

}

public class Transakcja
{
    private RachunekBankowy rachunekZrodlowy;
    public RachunekBankowy RachunekZrodlowy
    {
        get { return rachunekZrodlowy; }
        set { rachunekZrodlowy = value; }
    }
    private RachunekBankowy rachunekDocelowy;
    public RachunekBankowy RachunekDocelowy
    {
        get { return rachunekDocelowy; }
        set { rachunekDocelowy = value; }
    }
    private Decimal kwota;
    public Decimal Kwota
    {
        get { return kwota; }
        set { kwota = value; }
    }
    private string opis;
    public string Opis
    {
        get { return opis; }
        set { opis = value; }
    }

    public Transakcja(RachunekBankowy rachunekZrodlowy, RachunekBankowy rachunekDocelowy, Decimal kwota, string opis)
    {
        if (rachunekZrodlowy == null && rachunekDocelowy == null)
            throw new Exception("Nie podano rachunku źródłowego");
        this.rachunekZrodlowy = rachunekZrodlowy;
        this.rachunekDocelowy = rachunekDocelowy;
        this.kwota = kwota;
        this.opis = opis;
    }

    public override string ToString()
    {
        return "z: " + rachunekZrodlowy.Numer + "\ndo: " + rachunekDocelowy.Numer + "\nkwota: " + kwota + "\ntytuł: " + opis + "\n";
    }
}

