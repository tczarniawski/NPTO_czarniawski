using System;
using System.Collections.Generic;



class Wiadomosc
{


    public string Nadawca { get; set; }
    public string Odbiorca { get; set;}
    public string Tresc { get; set; }
    public bool CzyPrzeczytana {get; set; }

    public Wiadomosc(string nadawca, string odbiorca, string tresc)
    {


        Nadawca = nadawca;
        Odbiorca = odbiorca;
        Tresc = tresc;
        CzyPrzeczytana = false;

    }


}



class Komunikator
{


    private List<string> uzytkownicy = new List<string>();
    private List<Wiadomosc> wiadomosci = new List<Wiadomosc>();
    private string zalogowanyUzytkownik = null;

    public void Rejestruj(string nazwa)
    {

        if (string.IsNullOrWhiteSpace(nazwa))
            throw new Exception(" nazwa uzytkownika nie może byc pusta");
        if (uzytkownicy.Contains(nazwa))
            throw new Exception("uzytkownik o takiej nazwie już istnieje");

        uzytkownicy.Add(nazwa);



    }

    public void Zaloguj(string nazwa)
    {


        if (zalogowanyUzytkownik != null)
            throw new Exception("inny uzytkownik jest już zalogowany");
        if (!uzytkownicy.Contains(nazwa))
            throw new Exception(" podany uzytkownik nie istnieje ");


        zalogowanyUzytkownik = nazwa;

    }

    public void Wyloguj()
    {

        if (zalogowanyUzytkownik == null)
            throw new Exception("zaden uzytkownik nie jest zalogowany");

        zalogowanyUzytkownik = null;


    }

    public void WyslijWiadomosc(string odbiorca, string tresc)
    {


        if (zalogowanyUzytkownik == null)
            throw new Exception(" musisz byc zalogowany aby wyslac wiadomosc ");

        if (!uzytkownicy.Contains(odbiorca))
            throw new Exception("odbiorca wiadomości nie istnieje");


        if (string.IsNullOrWhiteSpace(tresc))
            throw new Exception("tresc wiadomości nie może byc pusta");

        Wiadomosc nowaWiadomosc = new Wiadomosc(zalogowanyUzytkownik, odbiorca, tresc);
        wiadomosci.Add(nowaWiadomosc);


    }

    public List<Wiadomosc> OdbierzNieprzeczytane()
    {
        if (zalogowanyUzytkownik == null)
            throw new Exception("musisz być zalogowany aby odebrac wiadomosci");

        List<Wiadomosc> nieprzeczytane = new List<Wiadomosc>();

        foreach (Wiadomosc w in wiadomosci)
        {
            if (w.Odbiorca == zalogowanyUzytkownik && w.CzyPrzeczytana == false)
            {

                nieprzeczytane.Add(w);
                w.CzyPrzeczytana = true;
            }
        }

        return nieprzeczytane;
    }
}

class Program
{


    static void Main()
    {

        Komunikator komunikator = new Komunikator();

        try
        {
            Console.WriteLine("poprawne scenariusze : ");
            komunikator.Rejestruj("Anna");
            komunikator.Rejestruj("Piotr");
            


            komunikator.Zaloguj("Anna");
            komunikator.WyslijWiadomosc("Piotr", "czesc piotr");
            komunikator.WyslijWiadomosc("Piotr", "qwerty123");
            komunikator.Wyloguj();



            komunikator.Zaloguj("Piotr");
            List<Wiadomosc> skrzynkaPiotra = komunikator.OdbierzNieprzeczytane();
            


            Console.WriteLine("odebrane wiadomości dla Piotra :");
            foreach (Wiadomosc w in skrzynkaPiotra)
            {

                Console.WriteLine("Od: " + w.Nadawca + " | Treść: " + w.Tresc);
            }
            komunikator.Wyloguj();
        }
        catch (Exception ex)
        {
            Console.WriteLine("wystąpil blad: " + ex.Message);
        }

        Console.WriteLine("\n---BLEDNE_SCENARIUSZE----");

        try
        {
            komunikator.WyslijWiadomosc("Anna", " proba wysłania bez logowania ");
        }
        catch (Exception ex)
        {
            Console.WriteLine("błąd 1: " + ex.Message);
        }

        try
        {
            komunikator.Zaloguj("Anna");
            komunikator.WyslijWiadomosc("nieznany", "wiadomosc z nieistniejacego profilu");
        }
        catch (Exception ex)
        {
            Console.WriteLine("błąd 2: " + ex.Message);
        }

        try
        {
            komunikator.Rejestruj("Anna");
        }
        catch (Exception ex)
        {
            Console.WriteLine("błąd 3: " + ex.Message);
        }

        try
        {
            komunikator.Zaloguj("Piotr");
        }
        catch (Exception ex)
        {
            Console.WriteLine("błąd 4: " + ex.Message);
        }
    }
}

//komentarz