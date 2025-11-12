using System.Globalization;
using System.Numerics;

namespace Evidencija_putovanja_console_app
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("APLIKACIJA ZA EVIDENCIJU GORIVA");
            //var users = new Dictionary<int, Tuple<string, string, DateTime, List<string>>>();
            //var trips = new Dictionary<int, Tuple<DateTime, double, double, double, double>>(); //brojevi tip?
            //var listOfTrips = new List<Dictionary<int, Tuple<DateTime, double, double, double, double>>>();
            //these are for later use
            static DateTime CheckDateInput(string input)
            {
                while (true)
                {
                    if (DateTime.TryParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                    {
                        if (date.Date > DateTime.Today)
                            Console.WriteLine("Neispravan unos, datum ne smije biti u budućnosti!\nUnesite datum: ");
                        else return date;
                    }
                    else Console.WriteLine("Neispravan format!\nUnesite datum: ");

                    input = Console.ReadLine();
                }
            }

            static int CheckIntInput(string input)
            {
                int number;
                while (true)
                {
                    if (int.TryParse(input, out number))
                    {
                        if (number > 0)
                            return number;
                        else Console.WriteLine("Neispravan unos!\nUnesite pozitivan broj: ");
                    }
                    else Console.WriteLine("Neispravan unos!\nUnesite cijeli broj: ");

                    input = Console.ReadLine();
                }
            }

            static double CheckDoubleInput(string input)
            {
                double number;
                while (true)
                {
                    if (double.TryParse(input, out number))
                    {
                        if (number > 0)
                            return number;
                        else Console.WriteLine("Neispravan unos!\nUnesite pozitivan broj: ");
                    }
                    else Console.WriteLine("Neispravan unos!\nUnesite broj: ");

                    input = Console.ReadLine();
                }

            }

            static string CheckStringInput(string input)
            {
                while (true)
                {
                    if (input=="")
                        Console.WriteLine("Neispravan unos!\nPokušajte ponovno: ");
                    else return input;

                    input = Console.ReadLine();
                }
            }

            static void InputUser()
            {
                Console.WriteLine("Unesite id korisnika: ");
                int idKorisnik = CheckIntInput(Console.ReadLine()); //.
                Console.WriteLine("Unesite ime: ");
                string name = CheckStringInput(Console.ReadLine());
                Console.WriteLine("Unesite prezime: ");
                string surname = CheckStringInput(Console.ReadLine());
                Console.WriteLine("Unesite datum rođenja: ");
                DateTime dateOfBirth = CheckDateInput(Console.ReadLine());
                //missing list of travels
                var userAttributes = (Id: idKorisnik, Name: name, Surname: surname, DateOfBirth: dateOfBirth);
                Console.WriteLine(userAttributes.ToString());
            }

            static void InputTrip()
            {
                //odaberi korisnika

                Console.WriteLine("Unesite kilometražu: ");
                Console.WriteLine("Unesite potrošeno gorivo: ");
                Console.WriteLine("Unesite cijenu po litri: ");
                Console.WriteLine("Putovanje uspješno dodano!");
            }

            var menuChoice = "";
            //string? menuChoice = null;
            do
            {
                Console.WriteLine("Izbornik:\n1 - Korisnici\n2 - Putovanja\n0 - Izlaz iz aplikacije");
                menuChoice = Console.ReadLine();
                Console.WriteLine("Odabir: {0}", menuChoice);
                switch (menuChoice)
                {
                    case "1":
                        var usersMenuChoice = "";
                        do
                        {
                            Console.WriteLine("Korisnici:\n1 - Unos novog korisnika\n2 - Brisanje korisnika\n3 - Uređivanje korisnika\n4 - Pregled svih korisnika\n0 - Povratak na glavni izbornik\n");
                            usersMenuChoice = Console.ReadLine();
                            Console.WriteLine("Odabir: {0}", usersMenuChoice);
                            switch (usersMenuChoice)
                            {
                                case "1":
                                    Console.WriteLine("Unos novog korisnika");
                                    InputUser();
                                    break;
                                case "2":
                                    Console.WriteLine("Brisanje korisnika");
                                    break;
                                case "3":
                                    Console.WriteLine("Uređivanje korisnika");
                                    break;
                                case "4":
                                    Console.WriteLine("Pregled svih korisnika");
                                    break;
                                default: //odradi i kad upise 0
                                    Console.WriteLine("Pogrešan unos!");
                                    break;
                            }
                        }
                        while (usersMenuChoice != "0");
                        Console.WriteLine("Povratak na glavni izbornik");
                        break;
                    case "2":
                        var travelsMenuChoice = "";
                        do
                        {
                            Console.WriteLine("Putovanja:\n1 - Unos novog putovanja\n2 - Brisanje putovanja\n3 - Uređivanje postojećeg putovanja\n4 - Pregled svih putovanja\n5 - Izvještaji i analize\n0 - Povratak na glavni izbornik\n");
                            travelsMenuChoice = Console.ReadLine();
                            Console.WriteLine("Odabir: {0}", travelsMenuChoice);
                            switch (travelsMenuChoice)
                            {
                                case "1":
                                    Console.WriteLine("Unos novog putovanja");
                                    break;
                                case "2":
                                    Console.WriteLine("Brisanje putovanja");
                                    break;
                                case "3":
                                    Console.WriteLine("Uređivanje postojećeg putovanja");
                                    break;
                                case "4":
                                    Console.WriteLine("Pregled svih putovanja");
                                    break;
                                case "5":
                                    Console.WriteLine("Izvještaji i analize");
                                    break;
                                default:
                                    Console.WriteLine("Pogrešan unos!");
                                    break;
                            }
                        }
                        while (travelsMenuChoice != "0");
                        Console.WriteLine("Povratak na glavni izbornik");
                        break;
                    default:
                        Console.WriteLine("Pogrešan unos!");
                        break;

                }
            }
            while (menuChoice != "0");
            Console.WriteLine("Izlaz iz aplikacije");
        }
    }
}
